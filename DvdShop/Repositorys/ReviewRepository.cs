using DvdShop.Database;
using DvdShop.Entity;
using DvdShop.Interface.IRepositorys;
using Microsoft.EntityFrameworkCore;

namespace DvdShop.Repositorys
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DvdStoreContext _context;

        public ReviewRepository(DvdStoreContext context)
        {
            _context = context;
        }

        public async Task<Review> AddReviewAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<IEnumerable<Review>> GetReviewsByCustomerIdAsync(Guid customerId)
        {
            return await _context.Reviews
                .Where(r => r.CustomerId == customerId)
                .Include(r => r.Customer)
                .Include(r => r.DVD)
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsByDvdIdAsync(Guid dvdId)
        {
            return await _context.Reviews
                .Where(r => r.DvdId == dvdId)
                .Include(r => r.Customer)
                .Include(r => r.DVD)
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            return await _context.Reviews
                .Include(r => r.Customer)
                .Include(r => r.DVD)
                .ToListAsync();
        }
    }

}
