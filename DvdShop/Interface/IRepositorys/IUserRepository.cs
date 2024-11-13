using DvdShop.DTOs.Requests;
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
        Task AddUserRoleAsync(UserRole userRole);
        Task UpdateUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> ValidateOTPAsync(string email, string otp);
        Task AddVerificationOTPAsync(string email, string otp);
        Task<Customer> GetCustomerByEmailAsync(string email);
        Task UpdateCustomerAsync(Customer customer);
    }

}
