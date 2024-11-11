using DvdShop.Database;
using DvdShop.DTOs.Requests.Customers;
using DvdShop.Entity;
using DvdShop.Interface.IRepositorys;
using Microsoft.EntityFrameworkCore;

namespace DvdShop.Repositorys
{
    public class CustomerRepository: ICustomerRepository
    {
        private readonly DvdDbcontext _dbcontext;

        public CustomerRepository(DvdDbcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        public async Task<Customer>RegisterCustomer(Customer customer)
        {

            if (customer == null)
            {
                throw new Exception("Please enter the deatails");
            }

            var resiter = await _dbcontext.Customers.AddAsync(customer);
            await _dbcontext.SaveChangesAsync();

            return customer;
        }
        public async Task<Customer> GetUserByNic(string Nic)
        {
            var user = await _dbcontext.Customers.SingleOrDefaultAsync(u => u.Nic == Nic);
            return user!;
        }

        public async Task<Customer> Login(LoginRequestDTO request)
        {
            var user = await GetUserByNic(request.Nic);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var isLogin = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            if (isLogin)
            {
                return user;
            }
            else
            {
                throw new Exception("Invalid password");
            }
        }
    }
}
