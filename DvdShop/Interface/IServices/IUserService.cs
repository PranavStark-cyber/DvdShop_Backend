using DvdShop.DTOs.Requests;
using DvdShop.DTOs.Requests.Customers;

namespace DvdShop.Interface.IServices
{
    public interface IUserService
    {
        Task<string> RegisterUserAsync(RegisterDTO registerDTO);
        Task<string> VerifyEmailAsync(VerificationDTO verificationDTO);
        Task<string> Login(LoginRequestDTO loginDTO);
    }
}
