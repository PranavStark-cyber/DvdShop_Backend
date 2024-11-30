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

        [HttpPost("GetAllCustomer")]
        public async Task<IActionResult> GetAllCustomer()
        {
            var data = await _customerService.GetCustomers();
            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, UpdateCustomerDTO customerDto)
        {
            if (id != customerDto.Id)
            {
                return BadRequest("Customer ID mismatch.");
            }

            try
            {
                var updatedCustomer = await _customerService.UpdateCustomerAsync(customerDto);
                return Ok(updatedCustomer);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
