using DvdShop.Database;
using DvdShop.Entity;
using DvdShop.Interface.IRepositorys;
using System;

namespace DvdShop.Repositorys
{
    public class NotificationService:INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task SendNotificationAsync(Guid receiverId, string title, string message, string type = "Info")
        {
            try
            {
                var notification = new Notification
                {
                    Id = Guid.NewGuid(),
                    ReceiverId = receiverId,
                    Title = title,
                    Message = message,
                    Type = type,
                    ViewStatus = "Unread", // Default status
                    Date = DateTime.Now
                };

                await _notificationRepository.AddNotification(notification);
            }
            catch (Exception ex)
            {
                throw new Exception("Error sending notification.", ex);
            }
        }

        public async Task SendNotification(Guid receiverId, string title, string message, string type = "Warning")
        {
            try
            {
                var notification = new Notification
                {
                    Id = Guid.NewGuid(),
                    ReceiverId = receiverId,
                    Title = title,
                    Message = message,
                    Type = type,
                    ViewStatus = "Unread", // Default status
                    Date = DateTime.Now
                };

                await _notificationRepository.AddNotification(notification);
            }
            catch (Exception ex)
            {
                throw new Exception("Error sending notification.", ex);
            }
        }

        public async Task<List<Notification>> GetNotificationsAsync(Guid userId)
        {
            return await _notificationRepository.GetNotificationsByUserId(userId);
        }
    }
}
