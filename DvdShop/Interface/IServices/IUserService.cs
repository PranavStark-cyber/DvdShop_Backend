using DvdShop.DTOs.Requests;

namespace DvdShop.Interface.IServices
{
    public interface IUserService
    {
        Task<string> RegisterUserAsync(RegisterDTO registerDTO);
        Task<string> VerifyEmailAsync(VerificationDTO verificationDTO);
    }
}
