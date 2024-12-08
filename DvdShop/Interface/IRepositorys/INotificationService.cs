using DvdShop.DTOs.Responses.Customers;
using DvdShop.Entity;

namespace DvdShop.Interface.IRepositorys
{
    public interface INotificationService
    {
        Task SendNotificationAsync(Guid receiverId, string title, string message, string type = "Info");
        Task SendNotification(Guid receiverId, string title, string message, string type = "Warning");
        Task<List<Notification>> GetNotificationsAsync(Guid userId);
        Task<List<NotificationResponse>> GetNotificationsByUserId(Guid userId);
    }
}
