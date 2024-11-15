using DvdShop.Entity;
using System.Threading.Tasks;

namespace DvdShop.Interface.IRepositorys
{
    public interface IManagerRepository
    {
        Task<DVD> AddDvdAsync(DVD dvd);
        Task<Genre> GetGenreByIdAsync(int genreId);
        Task<Director> GetDirectorByIdAsync(int directorId);
        Task<Inventory> AddInventoryAsync(Inventory inventory);
        Task<DVD> GetDvdByIdAsync(Guid id);
        Task<Genre> GetOrCreateGenreAsync(int genreId, string genreName);
        Task<Director> GetOrCreateDirectorAsync(int directorId, string directorName, string directordescription);
        Task RemoveInventory(Inventory inventory);
        Task UpdateInventoryAsync(Inventory inventory);
        Task<IEnumerable<DVD>> GetAllDvdsAsync();
        Task<string> DeleteDvdAsync(Guid id, int quantityToDelete);
        Task<DVD> UpdateDvdAsync(DVD dvd);
        Task<Inventory> GetInventoryByDvdIdAsync(Guid dvdId);


    }
}
