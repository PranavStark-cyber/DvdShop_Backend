using DvdShop.DTOs.Requests;
using DvdShop.Interface.IRepositorys;
using DvdShop.Interface.IServices;

namespace DvdShop.Services
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public UserService(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task<string> RegisterUserAsync(RegisterDTO registerDTO)
        {
            var user = await _userRepository.RegisterUserAsync(registerDTO);
            var otp = GenerateOTP();
            await _userRepository.AddVerificationOTPAsync(registerDTO.Email, otp);

            var body = $"Dear {registerDTO.FirstName},\n\nYour email verification OTP is {otp}.\n\nBest Regards,\nYourAppName";
            await _emailService.SendEmailAsync(registerDTO.Email, "Email Verification", body);

            return "Registration successful. Please verify your email.";
        }

        public async Task<string> VerifyEmailAsync(VerificationDTO verificationDTO)
        {
            var isValid = await _userRepository.ValidateOTPAsync(verificationDTO.Email, verificationDTO.OTP);
            if (!isValid) return "Invalid or expired OTP.";

            var user = await _userRepository.GetUserByEmailAsync(verificationDTO.Email);
            user.IsConfirmed = true;
            //await _userRepository.UpdateUserAsync(user);

            return "Email successfully verified.";
        }

        private string GenerateOTP()
        {
            return new Random().Next(100000, 999999).ToString();
        }
    }

}
