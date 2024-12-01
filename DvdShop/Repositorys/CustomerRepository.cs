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

        public async Task<Customer?> GetCustomerById(Guid id)
        {
            try
            {
                var customer= await _dbcontext.Customers
                                     .Include(c => c.Address)
                                     .Include(c => c.Rentals)    // Include Rentals if needed
                                     .Include(c => c.Payments)   // Include Payments if needed
                                     .FirstOrDefaultAsync(c => c.Id == id);

                if (customer == null)
                {
                    throw new KeyNotFoundException($"Customer with ID '{id}' not found.");
                }
                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching customer from database.", ex);
            }
        }

        public async Task<bool> UpdateCustomer(Customer customer)
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


        public async Task<bool> DeleteCustomer(Guid customerId)
        {
            try
            {
                // Fetch the customer with related data
                var customer = await _dbcontext.Customers
                                              .Include(c => c.Rentals)
                                              .Include(c => c.Address)
                                              .FirstOrDefaultAsync(c => c.Id == customerId);

                if (customer == null)
                {
                    throw new KeyNotFoundException("Customer not found.");
                }

                // Prevent deletion if the customer has rentals
                if (customer.Rentals != null && customer.Rentals.Any())
                {
                    throw new InvalidOperationException("Cannot delete customer with active rentals.");
                }

                // Delete Address if exists
                if (customer.Address != null)
                {
                    _dbcontext.Addresses.Remove(customer.Address);
                }

                // Delete UserRole associated with the customer
                var userRole = await _dbcontext.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == customerId);
                if (userRole != null)
                {
                    _dbcontext.UserRoles.Remove(userRole);
                }

                // Delete User associated with the customer
                var user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Id == customerId);
                if (user != null)
                {
                    _dbcontext.Users.Remove(user);
                }

                // Delete the customer
                _dbcontext.Customers.Remove(customer);

                // Save changes
                var changes = await _dbcontext.SaveChangesAsync();
                return changes > 0;
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting customer from database.", ex);
            }
        }

        public async Task<bool> DVDExistsAsync(Guid dvdId)
        {
            return await _dbcontext.DVDs.AnyAsync(d => d.Id == dvdId);
        }

        public async Task<bool> CustomerExistsAsync(Guid customerId)
        {
            return await _dbcontext.Customers.AnyAsync(c => c.Id == customerId);
        }

        public async Task<bool> AddReviewAsync(Review review)
        {
            try
            {
                await _dbcontext.Reviews.AddAsync(review);
                var result = await _dbcontext.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding review to the database.", ex);
            }
        }

    }
}
