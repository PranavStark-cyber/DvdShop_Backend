using DvdShop.DTOs.Requests.Manager;
using DvdShop.Entity;

namespace DvdShop.Interface.IServices
{
    public interface IManagerService
    {
        Task<DVD> AddDvdAsync(CreateDvdDto createDvdDto);
        Task<DVD> GetDvdByIdAsync(Guid id);
    }
}
