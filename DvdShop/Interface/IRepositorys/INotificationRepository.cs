using DvdShop.Entity;

namespace DvdShop.Interface.IRepositorys
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetNotificationsByUserId(Guid userId);
        Task<Notification?> GetNotificationById(Guid notificationId);
        Task<bool> AddNotification(Notification notification);
        Task<bool> UpdateNotification(Notification notification);
        Task<bool> DeleteNotification(Guid notificationId);
    }
}
