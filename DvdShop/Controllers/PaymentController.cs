using DvdShop.Interface.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DvdShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // API for Total Amount
        [HttpGet("total/{customerId}")]
        public async Task<IActionResult> GetTotalAmountByCustomerId(Guid customerId)
        {
            try
            {
                var totalAmount = await _paymentService.GetTotalAmountByCustomerId(customerId);
                return Ok(totalAmount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        // API for Overdue Amount
        [HttpGet("overdue/{customerId}")]
        public async Task<IActionResult> GetOverdueAmountByCustomerId(Guid customerId)
        {
            try
            {
                var overdueAmount = await _paymentService.GetOverdueAmountByCustomerId(customerId);
                return Ok(overdueAmount);
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }
    }
}
