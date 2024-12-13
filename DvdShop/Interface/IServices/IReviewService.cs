using DvdShop.Entity;

namespace DvdShop.Interface.IServices
{
    public interface IReviewService
    {
        Task<Review> AddReviewAsync(Review review);
        Task<IEnumerable<Review>> GetReviewsByCustomerIdAsync(Guid customerId);
        Task<IEnumerable<Review>> GetReviewsByDvdIdAsync(Guid dvdId);
        Task<IEnumerable<Review>> GetAllReviewsAsync();
    }
}
