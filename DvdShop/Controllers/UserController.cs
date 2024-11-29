using DvdShop.DTOs.Requests;
using DvdShop.DTOs.Requests.Customers;
using DvdShop.DTOs.Requests.Manager;
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
            try
            {
                var result = await _userService.RegisterUserAsync(registerDTO);
                return Ok(new { message = result }); 
            }
            catch (InvalidOperationException ex)
            {
              
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPost("registerStaff")]
        public async Task<IActionResult> RegisterStaff([FromBody] RegisterStaff registerDTO)
        {
            var result = await _userService.RegisterManagerAsync(registerDTO);
            return Ok(new { message = result });
        }


        [HttpPost("verify")]
        public async Task<IActionResult> Verify([FromBody] VerificationDTO verificationDTO)
        {
            if (verificationDTO == null)
            {
                return BadRequest(new { message = "Invalid request body." });
            }

            try
            {
                var result = await _userService.VerifyEmailAsync(verificationDTO);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., log to a file, database, or monitoring system)
                // Example: _logger.LogError(ex, "Error during email verification.");
                return StatusCode(500, new { message = "An internal error occurred. Please try again later." });
            }
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
