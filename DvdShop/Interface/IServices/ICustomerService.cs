using DvdShop.DTOs.Requests.Customers;
using DvdShop.Entity;

namespace DvdShop.Interface.IServices
{
    public interface ICustomerService
    {
        Task<Customer> UpdateCustomerAsync(UpdateCustomerDTO customerDto);
        Task<ICollection<Customer>> GetCustomers();
    }
}
