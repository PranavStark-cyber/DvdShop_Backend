using DvdShop.DTOs.Requests.Customers;
using DvdShop.Entity;

namespace DvdShop.Interface.IServices
{
    public interface ICustomerService
    {
        Task<Customer> RegisterCustomer(RegisterDTO registerDTO);
        Task<String> Login(LoginRequestDTO request);

    }
}
