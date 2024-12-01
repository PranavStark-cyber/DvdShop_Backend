using DvdShop.Entity;
using System.Threading.Tasks;

namespace DvdShop.Interface.IRepositorys
{
    public interface IManagerRepository
    {
        Task<Staff> GetStaffById(Guid StaffId);
        Task<DVD> AddDvd(DVD dvd);
        Task<List<Director>> GetDirector();
        Task<List<Genre>> GetGenre();
        Task<Genre> GetGenreById(int genreId);
        Task<Director> GetDirectorById(int directorId);
        Task<Inventory> AddInventory(Inventory inventory);
        Task<DVD> GetDvdById(Guid id);
        Task<Genre> GetOrCreateGenre(int genreId, string genreName);
        Task<Director> GetOrCreateDirector(int directorId, string directorName, string directordescription);
        Task RemoveInventory(Inventory inventory);
        Task UpdateInventory(Inventory inventory);
        Task<IEnumerable<DVD>> GetAllDvds();
        Task<string> DeleteDvd(Guid id, int quantityToDelete);
        Task<DVD> UpdateDvd(DVD dvd);
        Task<Inventory> GetInventoryByDvdId(Guid dvdId);


        Task<List<Inventory>> GetWeeklyInventoryReport();
        Task<List<Inventory>> GetMonthlyInventoryReport();
        Task SendInventoryReportNotification(string title, string message);
        Task<List<Inventory>> GetAllInventory();


    }
}
