using DvdShop.Database;
using DvdShop.DTOs.Responses.Customers;
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

        public async Task<List<NotificationResponse>> GetNotificationsByUserId(Guid userId)
        {
            try
            {
                // Fetch notifications for the given user
                var notifications = await _notificationRepository.GetNotificationsByUserId(userId);

                // Map the notifications to NotificationResponse DTO
                var notificationResponses = notifications.Select(notification => new NotificationResponse
                {
                    Id = notification.Id,
                    Title = notification.Title,
                    Message = notification.Message,
                    ViewStatus = notification.ViewStatus,
                    Type = notification.Type,
                    Date = notification.Date
                }).ToList();

                return notificationResponses;
            }
            catch (Exception ex)
            {
                // Log the exception (you can use your logging framework here)
                Console.Error.WriteLine($"Error occurred while fetching notifications: {ex.Message}");

                // Optionally, you can return an empty list or rethrow the exception based on your requirements
                throw new ApplicationException("An error occurred while retrieving notifications.");
            }
        }

    }
}
