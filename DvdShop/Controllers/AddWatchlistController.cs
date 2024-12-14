using DvdShop.DTOs.Requests;
using DvdShop.Entity;
using DvdShop.Interface.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DvdShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddWatchlistController : ControllerBase
    {
        private readonly IAddWatchlistService _watchlistService;

        public AddWatchlistController(IAddWatchlistService watchlistService)
        {
            _watchlistService = watchlistService;
        }

        // POST: api/AddWatchlist
        [HttpPost]
        public async Task<IActionResult> AddToWatchlist(AddwatchlistDTO watchlist)
        {
            if (watchlist == null)
            {
                return BadRequest("Watchlist cannot be null.");
            }

            var addedWatchlist = await _watchlistService.AddToWatchlistAsync(watchlist);
            return CreatedAtAction(nameof(GetWatchlistById), new { id = addedWatchlist.Id }, addedWatchlist);
        }

        // GET: api/AddWatchlist
        [HttpGet]
        public async Task<IActionResult> GetAllWatchlists()
        {
            var watchlists = await _watchlistService.GetAllWatchlistsAsync();
            return Ok(watchlists);
        }

        // GET: api/AddWatchlist/by-customer/{customerId}
        [HttpGet("by-customer/{customerId}")]
        public async Task<IActionResult> GetWatchlistByCustomerId(Guid customerId)
        {
            var watchlists = await _watchlistService.GetWatchlistByCustomerIdAsync(customerId);
            return Ok(watchlists);
        }

        // GET: api/AddWatchlist/by-dvd/{dvdId}
        [HttpGet("by-dvd/{dvdId}")]
        public async Task<IActionResult> GetWatchlistByDvdId(Guid dvdId)
        {
            var watchlists = await _watchlistService.GetWatchlistByDvdIdAsync(dvdId);
            return Ok(watchlists);
        }

        // GET: api/AddWatchlist/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWatchlistById(Guid id)
        {
            var watchlist = await _watchlistService.GetWatchlistByCustomerIdAsync(id); // This can be changed to find by Id if needed.
            if (watchlist == null)
            {
                return NotFound();
            }
            return Ok(watchlist);
        }
    }
}
