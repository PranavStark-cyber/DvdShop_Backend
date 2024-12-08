using DvdShop.Interface.IRepositorys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DvdShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetNotificationsByUserId(Guid userId)
        {
            try
            {
                // Fetch notifications by userId from the service layer
                var notifications = await _notificationService.GetNotificationsByUserId(userId);

                // Return the notifications as a response
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                // Handle any errors and return a proper response
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }
    }
}
