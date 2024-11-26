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
        public async Task<IActionResult> AddDvd( CreateDvdDto createDvdDto)
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

        [HttpPut("UpdateDvd/{id}")]
        public async Task<IActionResult> UpdateDvd(Guid id, [FromBody] CreateDvdDto updateDvdDto)
        {
            if (updateDvdDto == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                var updatedDvd = await _managerService.UpdateDvdAsync(id, updateDvdDto);
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
                var result = await _managerService.DeleteDvdAsync(dvdId, quantityToDelete);
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
            var dvds = await _managerService.GetAllDvdsAsync();
            return Ok(dvds);
        }

        [HttpGet("GetAllGenare")]
        public async Task<ActionResult<List<Genre>>> GetGenare()
        {
            var genres = await _managerService.GetGenareAsync();
            return Ok(genres);
        }
        [HttpGet("GetAllDirector")]
        public async Task<ActionResult<List<Director>>> GetDirector()
        {
            var directors = await _managerService.GetDirectorAsync();
            return Ok(directors);
        }
    }
}
