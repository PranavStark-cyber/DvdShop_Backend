using DvdShop.DTOs.Requests;
using DvdShop.Entity;
using DvdShop.Interface.IRepositorys;
using DvdShop.Interface.IServices;

namespace DvdShop.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;


        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<ReviewDTO> CreateReviewAsync(ReviewDTO reviewDTO)
        {
            // Check if the review already exists for the customer and DVD
            var existingReview = await _reviewRepository.GetReviewByCustomerAndDvdAsync(reviewDTO.CustomerId, reviewDTO.DvdId);
            if (existingReview != null)
            {
                throw new InvalidOperationException("Review already exists for this customer and DVD.");
            }

            // Manual mapping from ReviewDTO to Review entity
            var review = new Review
            {
                Id = Guid.NewGuid(), // Assuming you want a new GUID for the review
                CustomerId = reviewDTO.CustomerId,
                DvdId = reviewDTO.DvdId,
                Comment = reviewDTO.Comment,
                Rating = reviewDTO.Rating,
                ReviewDate = DateTime.UtcNow
            };

            // Add the review to the database
            var createdReview = await _reviewRepository.AddReviewAsync(review);

            // Manual mapping from Review entity to ReviewDTO to return the response
            var reviewResponseDTO = new ReviewDTO
            {
                CustomerId = createdReview.CustomerId,
                DvdId = createdReview.DvdId,
                Comment = createdReview.Comment,
                Rating = createdReview.Rating
            };

            return reviewResponseDTO;
        }
        public async Task<IEnumerable<Review>> GetReviewsByCustomerIdAsync(Guid customerId)
        {
            return await _reviewRepository.GetReviewsByCustomerIdAsync(customerId);
        }

        public async Task<IEnumerable<Review>> GetReviewsByDvdIdAsync(Guid dvdId)
        {
            return await _reviewRepository.GetReviewsByDvdIdAsync(dvdId);
        }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            return await _reviewRepository.GetAllReviewsAsync();
        }
    }

}
