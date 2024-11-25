using DvdShop.DTOs.Requests;
using DvdShop.DTOs.Requests.Customers;
using DvdShop.DTOs.Requests.Manager;

namespace DvdShop.Interface.IServices
{
    public interface IUserService
    {
        Task<string> RegisterUserAsync(RegisterDTO registerDTO);
        Task<string> VerifyEmailAsync(VerificationDTO verificationDTO);
        Task<string> Login(LoginRequestDTO loginDTO);
        Task<string> RegisterManagerAsync(RegisterStaff registerDTO);
    }
}
