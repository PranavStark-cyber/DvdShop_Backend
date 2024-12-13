using DvdShop.Entity;
using DvdShop.Interface.IRepositorys;
using DvdShop.Interface.IServices;

namespace DvdShop.Services
{
    public class AddWatchlistService : IAddWatchlistService
    {
        private readonly IAddWatchlistRepository _watchlistRepository;

        public AddWatchlistService(IAddWatchlistRepository watchlistRepository)
        {
            _watchlistRepository = watchlistRepository;
        }

        public async Task<AddWatchlist> AddToWatchlistAsync(AddWatchlist watchlist)
        {
            return await _watchlistRepository.AddToWatchlistAsync(watchlist);
        }

        public async Task<IEnumerable<AddWatchlist>> GetWatchlistByCustomerIdAsync(Guid customerId)
        {
            return await _watchlistRepository.GetWatchlistByCustomerIdAsync(customerId);
        }

        public async Task<IEnumerable<AddWatchlist>> GetWatchlistByDvdIdAsync(Guid dvdId)
        {
            return await _watchlistRepository.GetWatchlistByDvdIdAsync(dvdId);
        }

        public async Task<IEnumerable<AddWatchlist>> GetAllWatchlistsAsync()
        {
            return await _watchlistRepository.GetAllWatchlistsAsync();
        }
    }

}
