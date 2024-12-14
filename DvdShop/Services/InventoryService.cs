using DvdShop.DTOs;
using DvdShop.Interface.IRepositorys;
using DvdShop.Interface.IServices;

namespace DvdShop.Services
{
    public class InventoryService: IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<IEnumerable<InventoryDto>> GetAvailableAndTotalCopiesAsync()
        {
            var inventories = await _inventoryRepository.GetAllInventoriesAsync();

            return inventories.Select(i => new InventoryDto
            {
                DvdId = i.DvdId,
                DvdTitle = i.Dvd.Title,  // Assuming DVD has a Title property
                TotalCopies = i.TotalCopies,
                AvailableCopies = i.AvailableCopies
            }).ToList();
        }
    }
}
