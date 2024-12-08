using DvdShop.Database;
using DvdShop.Entity;
using DvdShop.Interface.IRepositorys;
using Microsoft.EntityFrameworkCore;

namespace DvdShop.Repositorys
{
    public class NotificationRepository:INotificationRepository
    {
        private readonly DvdStoreContext _Notificationcontext;

        public NotificationRepository(DvdStoreContext Notificationcontext)
        {
            _Notificationcontext = Notificationcontext;
        }
        public async Task<List<Notification>> GetNotificationsByUserId(Guid userId)
        {
            return await _Notificationcontext.Notifications
                                 .Where(n => n.ReceiverId == userId)
                                 .OrderByDescending(n => n.Date)
                                 .ToListAsync();
        }

        public async Task<Notification?> GetNotificationById(Guid notificationId)
        {
            return await _Notificationcontext.Notifications
                                 .FirstOrDefaultAsync(n => n.Id == notificationId);
        }

        public async Task<bool> AddNotification(Notification notification)
        {
            await _Notificationcontext.Notifications.AddAsync(notification);
            return await _Notificationcontext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateNotification(Notification notification)
        {
            _Notificationcontext.Notifications.Update(notification);
            return await _Notificationcontext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteNotification(Guid notificationId)
        {
            var notification = await _Notificationcontext.Notifications.FindAsync(notificationId);
            if (notification != null)
            {
                _Notificationcontext.Notifications.Remove(notification);
                return await _Notificationcontext.SaveChangesAsync() > 0;
            }
            return false;
        }


        

    }
}
