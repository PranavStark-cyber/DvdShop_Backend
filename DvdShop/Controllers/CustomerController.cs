using DvdShop.DTOs.Requests.Customers;
using DvdShop.Interface.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DvdShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterCustomer(RegisterDTO registerDTO)
        {
          var data =  await _customerService.RegisterCustomer(registerDTO);

            return Ok(data);
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login(LoginRequestDTO request)
        {
            try
            {
                var userDetails = await _customerService.Login(request);
                return Ok(userDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
