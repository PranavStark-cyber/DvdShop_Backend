using DvdShop.DTOs.Requests.Manager;
using DvdShop.Entity;
using DvdShop.Interface.IServices;
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
        public async Task<IActionResult> AddDvd([FromBody] CreateDvdDto createDvdDto)
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
                var dvd = await _managerService.AddDvdAsync(createDvdDto);

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
            var dvd = await _managerService.GetDvdByIdAsync(id);
            if (dvd == null)
            {
                return NotFound();
            }

            return Ok(dvd);
        }
    }
}
