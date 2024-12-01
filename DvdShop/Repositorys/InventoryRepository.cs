using DvdShop.Database;
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
    }
}
