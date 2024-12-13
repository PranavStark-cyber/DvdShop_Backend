using DvdShop.Entity;

namespace DvdShop.Interface.IRepositorys
{
    public interface IAddWatchlistRepository
    {
        Task<AddWatchlist> AddToWatchlistAsync(AddWatchlist watchlist);
        Task<IEnumerable<AddWatchlist>> GetWatchlistByCustomerIdAsync(Guid customerId);
        Task<IEnumerable<AddWatchlist>> GetWatchlistByDvdIdAsync(Guid dvdId);
        Task<IEnumerable<AddWatchlist>> GetAllWatchlistsAsync();
    }
}
