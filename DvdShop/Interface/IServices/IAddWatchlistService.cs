using DvdShop.DTOs.Requests;
using DvdShop.Entity;

namespace DvdShop.Interface.IServices
{
    public interface IAddWatchlistService
    {
        Task<AddWatchlist> AddToWatchlistAsync(AddwatchlistDTO watchlist);
        Task<IEnumerable<AddWatchlist>> GetWatchlistByCustomerIdAsync(Guid customerId);
        Task<IEnumerable<AddWatchlist>> GetWatchlistByDvdIdAsync(Guid dvdId);
        Task<IEnumerable<AddWatchlist>> GetAllWatchlistsAsync();
    }
}
