using DvdShop.DTOs.Requests;
using DvdShop.Entity;

namespace DvdShop.Interface.IServices
{
    public interface IReviewService
    {
        Task<ReviewDTO> CreateReviewAsync(ReviewDTO reviewDTO);
        Task<IEnumerable<Review>> GetReviewsByCustomerIdAsync(Guid customerId);
        Task<IEnumerable<Review>> GetReviewsByDvdIdAsync(Guid dvdId);
        Task<IEnumerable<Review>> GetAllReviewsAsync();
    }
}
