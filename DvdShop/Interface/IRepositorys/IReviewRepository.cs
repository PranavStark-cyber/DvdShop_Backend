using DvdShop.Entity;

namespace DvdShop.Interface.IRepositorys
{
    public interface IReviewRepository
    {
 
        Task<IEnumerable<Review>> GetReviewsByCustomerIdAsync(Guid customerId);
        Task<IEnumerable<Review>> GetReviewsByDvdIdAsync(Guid dvdId);
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review> AddReviewAsync(Review review);
        Task<Review> GetReviewByCustomerAndDvdAsync(Guid customerId, Guid dvdId);
    }
}
