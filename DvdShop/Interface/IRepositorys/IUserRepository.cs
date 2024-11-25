using DvdShop.DTOs.Requests;
using DvdShop.DTOs.Requests.Customers;
using DvdShop.DTOs.Responses.Customers;
using DvdShop.Entity;

namespace DvdShop.Interface.IRepositorys
{
    //public interface IUserRepository
    //{
    //    Task<User> RegisterUserAsync(RegisterDTO registerDTO);
    //    Task<User> GetUserByEmailAsync(string email);
    //    Task AddVerificationOTPAsync(string email, string otp);
    //    Task<bool> ValidateOTPAsync(string email, string otp);
    //}

    public interface IUserRepository
    {
        Task<User> RegisterUserAsync(User user);
        Task AddCustomerAsync(Customer customer);
        Task AddStaffAsync(Staff staff);
        Task AddUserRoleAsync(UserRole userRole);
        Task UpdateUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> ValidateOTPAsync(string email, string otp);
        Task AddVerificationOTPAsync(string email, string otp);
        Task<Customer> GetCustomerByEmailAsync(string email);
        Task UpdateCustomerAsync(Customer customer);
        Task<Role> GetRoleById(Guid userId);
        Task<UserRole> GetUserRoleByUserId(Guid userId);
        Task<Customer> GetCustomerByIdAsync(Guid CustomerId);
    }

}
