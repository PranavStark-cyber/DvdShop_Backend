using DvdShop.DTOs;
using DvdShop.Interface.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DvdShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // Get all inventories with available and total copies
        [HttpGet("available-and-total")]
        public async Task<ActionResult<IEnumerable<InventoryDto>>> GetAvailableAndTotalCopies()
        {
            var result = await _inventoryService.GetAvailableAndTotalCopiesAsync();
            return Ok(result);
        }
    }
}
