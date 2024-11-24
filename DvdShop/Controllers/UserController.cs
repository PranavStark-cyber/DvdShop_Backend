using DvdShop.DTOs.Requests;
using DvdShop.DTOs.Requests.Customers;
using DvdShop.Interface.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace DvdShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var result = await _userService.RegisterUserAsync(registerDTO);
            return Ok(new { message = result });
        }

        [HttpPost("verify")]
        public async Task<IActionResult> Verify([FromBody] VerificationDTO verificationDTO)
        {
            var result = await _userService.VerifyEmailAsync(verificationDTO);
            return Ok(new { message = result });
        }

        [HttpPost("Login")]
         
        public async Task<IActionResult> Login (LoginRequestDTO loginRequest)
        {
            try
            {
                var userdetails = await _userService.Login(loginRequest);
                return Ok(userdetails);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
