using DvdShop.DTOs;
using DvdShop.DTOs.Requests;
using DvdShop.DTOs.Requests.Customers;
using DvdShop.DTOs.Requests.Manager;
using DvdShop.Entity;
using DvdShop.Interface.IRepositorys;
using DvdShop.Interface.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        private readonly IConfiguration _configuration;
        private readonly IManagerRepository _managerRepository;
        public UserService(
            IUserRepository userRepository,
            IManagerRepository managerRepository,
            IRoleRepository roleRepository,
            IEmailService emailService,
             IConfiguration configuration)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _emailService = emailService;
            _configuration = configuration;
            _managerRepository = managerRepository;


        }

        public async Task<string> RegisterUserAsync(RegisterDTO registerDTO)
        {
            var email = await _userRepository.GetUserByEmailAsync(registerDTO.Email);
            if (email != null)
            {
                return "Email Already Used.";
            }


            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = registerDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password), 
                IsConfirmed = false
            };

            var registeredUser = await _userRepository.RegisterUserAsync(user);
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

            var customer = new Customer
            {
                Id = registeredUser.Id,
                Nic = registerDTO.Nic,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                JoinDate = DateTime.Now,
            };

            await _userRepository.AddCustomerAsync(customer);

           

            var otp = GenerateOTP();
            await _userRepository.AddVerificationOTPAsync(registeredUser.Email, otp);

            var body = $"Dear {registerDTO.FirstName},\n\nYour email verification OTP is {otp}.\n\nBest Regards,\nYourAppName";
            await _emailService.SendEmailAsync(registeredUser.Email, "Email Verification", body);

            return "Registration successful. Please verify your email.";
        }
        public async Task<string> RegisterManagerAsync(RegisterStaff registerDTO)
        {
            var email = await _userRepository.GetUserByEmailAsync(registerDTO.Email);
            if (email != null)
            {
                return "Email Already Used.";
            }


            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = registerDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password),
                IsConfirmed = false
            };

            var registeredUser = await _userRepository.RegisterUserAsync(user);
            var managerRole = await _roleRepository.GetRoleByNameAsync("Manager");
            if (managerRole == null)
            {
                managerRole = new Role { Name = "Manager" };
                await _roleRepository.AddRoleAsync(managerRole);
            }

            var userRole = new UserRole
            {
                UserId = registeredUser.Id,
                RoleId = managerRole.Id
            };
            await _userRepository.AddUserRoleAsync(userRole);

            var staff = new Staff
            {
                Id = registeredUser.Id,
                NIC = registerDTO.Nic,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,

            };

            await _userRepository.AddStaffAsync(staff);



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

        public async Task<string> Login(LoginRequestDTO loginDTO)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginDTO.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            if (!BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var userRoles = await _userRepository.GetUserRoleByUserId(user.Id);
            var roledata = await _userRepository.GetRoleById(userRoles.RoleId);

            if (roledata == null)
            {
                throw new Exception("role not found");
            }

            if(roledata.Name == "Customer")
            {
                var customerdata = await _userRepository.GetCustomerByIdAsync(user.Id);
                var tokenREquest = new TokenRequestDTO
                {
                    Id = customerdata.Id,
                    Name =customerdata.FirstName,
                    Email = user.Email,
                    Role = roledata.Name

                };

                return CreateToken(tokenREquest);
            }
            else
            {
                if (roledata.Name == "Admin" || roledata.Name == "Staff")
                {
                    var admindata = await _managerRepository.GetStaffById(user.Id);
                    var tokenRequest = new TokenRequestDTO
                    {
                        Id = admindata.Id,
                        Name = admindata.FirstName,
                        Email = user.Email,
                        Role = roledata.Name
                    };
                    return CreateToken(tokenRequest);
                }
            }


            return null;
        }


        public string CreateToken(TokenRequestDTO requestDTO)
        {
            var claimList = new List<Claim>();
            claimList.Add(new Claim("Id", requestDTO.Id.ToString()));
            claimList.Add(new Claim("Name", requestDTO.Name));
            claimList.Add(new Claim("Email", requestDTO.Email));
            claimList.Add(new Claim("Role", requestDTO.Role.ToString()));



            var key =new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!));
            var credintial = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claimList,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credintial
            );

         return new JwtSecurityTokenHandler().WriteToken(token);
          
        }
    }



}
