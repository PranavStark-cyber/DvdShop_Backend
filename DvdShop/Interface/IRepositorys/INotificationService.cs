using DvdShop.Entity;

namespace DvdShop.Interface.IRepositorys
{
    public interface INotificationService
    {
        Task SendNotificationAsync(Guid receiverId, string title, string message, string type = "Info");
        Task<List<Notification>> GetNotificationsAsync(Guid userId);
    }
}
