using DvdShop.DTOs.Requests.Manager;
using DvdShop.Entity;
using DvdShop.Interface.IServices;
using DvdShop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DvdShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;
        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }



        [HttpPost("AddDvd")]
        public async Task<IActionResult> AddDvd(CreateDvdDto createDvdDto)
        {

            if (string.IsNullOrWhiteSpace(createDvdDto.ImageUrl))
            {
                return BadRequest("ImageUrl is required and cannot be null or empty.");
            }

            if (createDvdDto == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                var dvd = await _managerService.AddDvd(createDvdDto);

                return CreatedAtAction(nameof(GetDvdById), new { id = dvd.Id }, dvd);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetDvdById{id}")]
        public async Task<ActionResult<DVD>> GetDvdById(Guid id)
        {
            var dvd = await _managerService.GetDvdById(id);
            if (dvd == null)
            {
                return NotFound();
            }

            return Ok(dvd);
        }

        [HttpPut("UpdateDvd/{id}")]
        public async Task<IActionResult> UpdateDvd(Guid id, CreateDvdDto updateDvdDto)
        {
            if (updateDvdDto == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                var updatedDvd = await _managerService.UpdateDvd(id, updateDvdDto);
                return Ok(updatedDvd);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("delete-dvd/{dvdId}")]
        public async Task<IActionResult> DeleteDvd(Guid dvdId, [FromBody] int quantityToDelete)
        {
            try
            {
                var result = await _managerService.DeleteDvd(dvdId, quantityToDelete);
                return Ok(result); // Success message
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetAllDvds")]
        public async Task<ActionResult<IEnumerable<DVD>>> GetAllDvds()
        {
            var dvds = await _managerService.GetAllDvds();
            return Ok(dvds);
        }

        [HttpGet("GetAllGenare")]
        public async Task<ActionResult<List<Genre>>> GetGenare()
        {
            var genres = await _managerService.GetGenare();
            return Ok(genres);
        }
        [HttpGet("GetAllDirector")]
        public async Task<ActionResult<List<Director>>> GetDirector()
        {
            var directors = await _managerService.GetDirector();
            return Ok(directors);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllInventory()
        {
            try
            {
                var inventories = await _managerService.GetAllInventory();
                return Ok(inventories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("weekly-report")]
        public async Task<IActionResult> GetWeeklyReport()
        {
            try
            {
                var report = await _managerService.GetWeeklyInventoryReport();
                return Ok(report);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("monthly-report")]
        public async Task<IActionResult> GetMonthlyReport()
        {
            try
            {
                var report = await _managerService.GetMonthlyInventoryReport();
                return Ok(report);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("send-report")]
        public async Task<IActionResult> SendReport([FromQuery] string type)
        {
            try
            {
                await _managerService.SendInventoryReportNotification(type);
                return Ok($"Inventory {type} report sent to managers.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaffById(Guid id)
        {
            try
            {
                var staff = await _managerService.GetStaffById(id);
                if (staff == null)
                {
                    return NotFound($"Staff with ID '{id}' not found.");
                }

                return Ok(staff);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}