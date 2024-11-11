using DvdShop.DTOs.Requests.Customers;
using DvdShop.Entity;

namespace DvdShop.Interface.IRepositorys
{
    public interface ICustomerRepository
    {
        Task<Customer> RegisterCustomer(Customer customer);
        Task<Customer> Login(LoginRequestDTO request);
        Task<Customer> GetUserByNic(string Nic);

    }
}
