using DvdShop.DTOs.Requests.Manager;
using DvdShop.Entity;

namespace DvdShop.Interface.IServices
{
    public interface IManagerService
    {
        Task<DVD> AddDvdAsync(CreateDvdDto createDvdDto);
        Task<DVD> GetDvdByIdAsync(Guid id);
        Task<DVD> UpdateDvdAsync(Guid id, CreateDvdDto updateDvdDto);
        Task<string> DeleteDvdAsync(Guid id, int quantityToDelete);
        Task<IEnumerable<DVD>> GetAllDvdsAsync();
        Task<List<Director>> GetDirectorAsync();
        Task<List<Genre>> GetGenareAsync();
    }
}
