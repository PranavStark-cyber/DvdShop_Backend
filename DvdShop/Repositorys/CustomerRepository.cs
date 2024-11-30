using DvdShop.Database;
using DvdShop.DTOs.Requests.Customers;
using DvdShop.Entity;
using DvdShop.Interface.IRepositorys;
using Microsoft.EntityFrameworkCore;

namespace DvdShop.Repositorys
{
    public class CustomerRepository: ICustomerRepository
    {
        private readonly DvdStoreContext _dbcontext;

        public CustomerRepository(DvdStoreContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        public async Task<ICollection<Customer>> GetCustomers( )
        {
          return  await _dbcontext.Customers.Include(ad=>ad.Address).Include(re=>re.Rentals).Include(re => re.Reviews).Include(re => re.Payments).Include(re => re.Notifications).ToListAsync();
           
        }

        public async Task<Customer?> GetCustomerByIdAsync(Guid id)
        {
            try
            {
                return await _dbcontext.Customers
                                     .Include(c => c.Address)
                                     .FirstOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching customer from database.", ex);
            }
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            try
            {
                // Check if the customer exists in the database
                var existingCustomer = await _dbcontext.Customers.AsNoTracking()
                                                               .FirstOrDefaultAsync(c => c.Id == customer.Id);

                if (existingCustomer == null)
                {
                    throw new KeyNotFoundException("Customer not found for update.");
                }

                // If address exists, ensure it is tracked or added
                if (customer.Address != null)
                {
                    if (existingCustomer.Address == null)
                    {
                        // Add new address if none exists
                        customer.Address.Id = Guid.NewGuid();
                        _dbcontext.Addresses.Add(customer.Address);
                    }
                    else
                    {
                        // Attach the existing address to ensure tracking
                        _dbcontext.Entry(customer.Address).State = EntityState.Modified;
                    }
                }

                // Attach the customer and mark it as modified
                _dbcontext.Entry(customer).State = EntityState.Modified;

                // Save changes and return success
                var changes = await _dbcontext.SaveChangesAsync();
                return changes > 0;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception("Concurrency conflict occurred during update.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating customer in database.", ex);
            }
        }
    }
}
