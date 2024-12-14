using DvdShop.DTOs;

namespace DvdShop.Interface.IServices
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryDto>> GetAvailableAndTotalCopiesAsync();
    }
}
