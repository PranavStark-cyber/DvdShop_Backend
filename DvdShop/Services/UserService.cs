using DvdShop.DTOs.Requests;
using DvdShop.Entity;
using DvdShop.Interface.IRepositorys;
using DvdShop.Interface.IServices;

namespace DvdShop.Services
{

    //public class UserService : IUserService
    //{
    //    private readonly IUserRepository _userRepository;
    //    private readonly IEmailService _emailService;

    //    public UserService(IUserRepository userRepository, IEmailService emailService)
    //    {
    //        _userRepository = userRepository;
    //        _emailService = emailService;
    //    }

    //    public async Task<string> RegisterUserAsync(RegisterDTO registerDTO)
    //    {
    //        var user = await _userRepository.RegisterUserAsync(registerDTO);
    //        var otp = GenerateOTP();
    //        await _userRepository.AddVerificationOTPAsync(registerDTO.Email, otp);

    //        var body = $"Dear {registerDTO.FirstName},\n\nYour email verification OTP is {otp}.\n\nBest Regards,\nYourAppName";
    //        await _emailService.SendEmailAsync(registerDTO.Email, "Email Verification", body);

    //        return "Registration successful. Please verify your email.";
    //    }

    //    public async Task<string> VerifyEmailAsync(VerificationDTO verificationDTO)
    //    {
    //        var isValid = await _userRepository.ValidateOTPAsync(verificationDTO.Email, verificationDTO.OTP);
    //        if (!isValid) return "Invalid or expired OTP.";

    //        var user = await _userRepository.GetUserByEmailAsync(verificationDTO.Email);
    //        user.IsConfirmed = true;
    //        //await _userRepository.UpdateUserAsync(user);

    //        return "Email successfully verified.";
    //    }

    //    private string GenerateOTP()
    //    {
    //        return new Random().Next(100000, 999999).ToString();
    //    }
    //}

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IEmailService _emailService;

        public UserService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _emailService = emailService;
        }

        public async Task<string> RegisterUserAsync(RegisterDTO registerDTO)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = registerDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password), 
                IsConfirmed = false
            };

            var registeredUser = await _userRepository.RegisterUserAsync(user);

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Nic = registerDTO.Nic,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                PhoneNumber = registerDTO.PhoneNumber,
                JoinDate = DateTime.Now,
            };

            await _userRepository.AddCustomerAsync(customer);

            var customerRole = await _roleRepository.GetRoleByNameAsync("Customer");
            if (customerRole == null)
            {
                customerRole = new Role { Name = "Customer" };
                await _roleRepository.AddRoleAsync(customerRole);
            }

            var userRole = new UserRole
            {
                UserId = registeredUser.Id,
                RoleId = customerRole.Id
            };
            await _userRepository.AddUserRoleAsync(userRole);

            var otp = GenerateOTP();
            await _userRepository.AddVerificationOTPAsync(registeredUser.Email, otp);

            var body = $"Dear {registerDTO.FirstName},\n\nYour email verification OTP is {otp}.\n\nBest Regards,\nYourAppName";
            await _emailService.SendEmailAsync(registeredUser.Email, "Email Verification", body);

            return "Registration successful. Please verify your email.";
        }

        public async Task<string> VerifyEmailAsync(VerificationDTO verificationDTO)
        {
            var isValid = await _userRepository.ValidateOTPAsync(verificationDTO.Email, verificationDTO.OTP);
            if (!isValid) return "Invalid or expired OTP.";

            var user = await _userRepository.GetUserByEmailAsync(verificationDTO.Email);
            user.IsConfirmed = true;
            await _userRepository.UpdateUserAsync(user);

            return "Email successfully verified.";
        }
        private string GenerateOTP()
        {
            Random rand = new Random();
            return rand.Next(100000, 999999).ToString();
        }

        //public async Task<string> LoginAsync(LoginDTO loginDTO)
        //{
        //    var user = await _userRepository.GetUserByEmailAsync(loginDTO.Email);
        //    if (user == null)
        //    {
        //        throw new UnauthorizedAccessException("Invalid email or password.");
        //    }

        //    if (!BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password))
        //    {
        //        throw new UnauthorizedAccessException("Invalid email or password.");
        //    }

        //    var userRoles = await _roleRepository.GetUserRolesAsync(user.Id);

        //    var token = _jwtTokenService.GenerateToken(user, userRoles);

        //    return token;
        //}
    }



}
