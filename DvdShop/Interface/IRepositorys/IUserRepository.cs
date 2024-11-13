using DvdShop.DTOs.Requests;
using DvdShop.Entity;

namespace DvdShop.Interface.IRepositorys
{
    public interface IUserRepository
    {
        Task<User> RegisterUserAsync(RegisterDTO registerDTO);
        Task<User> GetUserByEmailAsync(string email);
        Task AddVerificationOTPAsync(string email, string otp);
        Task<bool> ValidateOTPAsync(string email, string otp);
    }
}
