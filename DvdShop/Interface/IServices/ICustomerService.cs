using DvdShop.DTOs.Requests.Customers;
using DvdShop.DTOs.Responses.Customers;
using DvdShop.Entity;

namespace DvdShop.Interface.IServices
{
    public interface ICustomerService
    {
        Task<Customer> UpdateCustomer(UpdateCustomerDTO customerDto);
        Task<ICollection<Customer>> GetCustomers();
        Task<CustomerResponseDTO?> GetCustomerById(Guid customerId);
        Task<bool> DeleteCustomerAsync(Guid customerId);
        Task<bool> AddReviewAsync(AddReviewDTO reviewDto);
    }
}
