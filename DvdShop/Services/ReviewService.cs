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

        public async Task<Review> AddReviewAsync(Review review)
        {
            return await _reviewRepository.AddReviewAsync(review);
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
