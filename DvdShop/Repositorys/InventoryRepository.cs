using DvdShop.Database;
using DvdShop.Entity;
using DvdShop.Interface.IRepositorys;
using Microsoft.EntityFrameworkCore;

namespace DvdShop.Repositorys
{
    public class InventoryRepository: IInventoryRepository
    {
        private readonly DvdStoreContext _inventorycontext;

        public InventoryRepository(DvdStoreContext inventorycontext)
        {
            _inventorycontext = inventorycontext;
        }

        public async Task UpdateInventory(Guid dvdId, int quantity)
        {
            var inventory = await _inventorycontext.Inventories.FirstOrDefaultAsync(i => i.DvdId == dvdId);
            if (inventory != null)
            {
                inventory.AvailableCopies += quantity; // Decrease by quantity for rentals
                await _inventorycontext.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Inventory>> GetAllInventoriesAsync()
        {
            return await _inventorycontext.Inventories
                .Include(i => i.Dvd)  // Include related DVD data
                .ToListAsync();
        }
    }
}
