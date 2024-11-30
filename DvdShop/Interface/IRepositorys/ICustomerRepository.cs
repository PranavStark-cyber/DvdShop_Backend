using DvdShop.DTOs.Requests.Customers;
using DvdShop.Entity;

namespace DvdShop.Interface.IRepositorys
{
    public interface ICustomerRepository
    {
        Task<ICollection<Customer>> GetCustomers( );
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<Customer?> GetCustomerByIdAsync(Guid id);

    }
}
