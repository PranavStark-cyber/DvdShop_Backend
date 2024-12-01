using DvdShop.DTOs.Requests.Manager;
using DvdShop.Entity;

namespace DvdShop.Interface.IServices
{
    public interface IManagerService
    {
        Task<DVD> AddDvd(CreateDvdDto createDvdDto);
        Task<DVD> GetDvdById(Guid id);
        Task<DVD> UpdateDvd(Guid id, CreateDvdDto updateDvdDto);
        Task<string> DeleteDvd(Guid id, int quantityToDelete);
        Task<IEnumerable<DVD>> GetAllDvds();
        Task<List<Director>> GetDirector();
        Task<List<Genre>> GetGenare();
        Task<List<Inventory>> GetAllInventory();
        Task<List<Inventory>> GetWeeklyInventoryReport();
        Task<List<Inventory>> GetMonthlyInventoryReport();
        Task SendInventoryReportNotification(string type);
    }
}
