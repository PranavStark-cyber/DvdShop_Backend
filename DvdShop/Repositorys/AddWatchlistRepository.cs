using DvdShop.Database;
using DvdShop.Entity;
using DvdShop.Interface.IRepositorys;
using Microsoft.EntityFrameworkCore;

namespace DvdShop.Repositorys
{
    public class AddWatchlistRepository : IAddWatchlistRepository
    {
        private readonly DvdStoreContext _context;

        public AddWatchlistRepository(DvdStoreContext context)
        {
            _context = context;
        }

        public async Task<AddWatchlist> AddToWatchlistAsync(AddWatchlist watchlist)
        {
            await _context.AddWatchlists.AddAsync(watchlist);
            await _context.SaveChangesAsync();
            return watchlist;
        }

        public async Task<IEnumerable<AddWatchlist>> GetWatchlistByCustomerIdAsync(Guid customerId)
        {
            return await _context.AddWatchlists
                .Where(w => w.CustomerId == customerId)
                .Include(w => w.Customer)
                .Include(w => w.DVD)
                .ToListAsync();
        }

        public async Task<IEnumerable<AddWatchlist>> GetWatchlistByDvdIdAsync(Guid dvdId)
        {
            return await _context.AddWatchlists
                .Where(w => w.DVDId == dvdId)
                .Include(w => w.Customer)
                .Include(w => w.DVD)
                .ToListAsync();
        }

        public async Task<IEnumerable<AddWatchlist>> GetAllWatchlistsAsync()
        {
            return await _context.AddWatchlists
                .Include(w => w.Customer)
                .Include(w => w.DVD)
                .ToListAsync();
        }
    }

}
