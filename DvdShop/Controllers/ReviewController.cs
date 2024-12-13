using DvdShop.Entity;
using DvdShop.Interface.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DvdShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // POST: api/review
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] Review review)
        {
            if (review == null)
            {
                return BadRequest("Review cannot be null.");
            }

            var addedReview = await _reviewService.AddReviewAsync(review);
            return CreatedAtAction(nameof(GetReviewById), new { id = addedReview.Id }, addedReview);
        }

        // GET: api/review
        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            return Ok(reviews);
        }

        // GET: api/review/by-customer/{customerId}
        [HttpGet("by-customer/{customerId}")]
        public async Task<IActionResult> GetReviewsByCustomerId(Guid customerId)
        {
            var reviews = await _reviewService.GetReviewsByCustomerIdAsync(customerId);
            return Ok(reviews);
        }

        // GET: api/review/by-dvd/{dvdId}
        [HttpGet("by-dvd/{dvdId}")]
        public async Task<IActionResult> GetReviewsByDvdId(Guid dvdId)
        {
            var reviews = await _reviewService.GetReviewsByDvdIdAsync(dvdId);
            return Ok(reviews);
        }

        // GET: api/review/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(Guid id)
        {
            var review = await _reviewService.GetReviewsByCustomerIdAsync(id); // Change to find review by Id.
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }
    }
}
