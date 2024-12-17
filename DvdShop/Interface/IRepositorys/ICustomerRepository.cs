using DvdShop.DTOs.Requests.Customers;
using DvdShop.Entity;

namespace DvdShop.Interface.IRepositorys
{
    public interface ICustomerRepository
    {
        Task<ICollection<Customer>> GetCustomers( );
        Task<bool> UpdateCustomer(Customer customer);
        Task<Customer> GetCustomerById(Guid id);
        Task<bool> DeleteCustomer(Guid customerId);
        Task<bool> DVDExistsAsync(Guid dvdId);
        Task<bool> CustomerExistsAsync(Guid customerId);
        Task<User> GetUserById(Guid UserId);
        Task<bool> AddReviewAsync(Review review);



    }
}
