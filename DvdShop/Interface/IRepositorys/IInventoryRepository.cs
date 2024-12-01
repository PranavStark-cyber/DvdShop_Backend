namespace DvdShop.Interface.IRepositorys
{
    public interface IInventoryRepository
    {
          Task UpdateInventory(Guid dvdId, int quantity);
    }
}
