using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class NotificationUsersBiz : BizBaseT<NotificationUser>
    {
        private static INotificationUsersRepository _repository;
        public NotificationUsersBiz(INotificationUsersRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IList<NotificationUser>> GetUserNotificationsAsync(string userId, bool? isRead = null)
        {
            return (await _repository.GetAllAsync(t => t.SendToId == userId && (isRead == null || t.IsRead == isRead)  &&
            !(t.IsRead  && t.Notification.CreatedDate<DateTime.UtcNow.AddDays(-7)),
                x => x.Notification.CreatedBy)).OrderByDescending(o => o.Id).ToList();
        }


        public async override Task<NotificationUser> GetByIdAsync(int id)
        {
            return await _repository.GetFirstOrDefaultAsync(x => x.Id == id,y => y.Notification.CreatedBy);
        }
        public async Task<bool> UpdateIsRead(int notificationId, bool isRead)
        {
            var updateNotification = await _repository.FindByIdAsync(notificationId);
            updateNotification.IsRead = isRead;
            return await _repository.DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAllIsDelivered(string userId)
        {
            var notificationUsers = Repository.DbContext.NotificationUsers.Where(nu => nu.SendToId == userId).ToList();
            notificationUsers.ForEach(nu => nu.IsDelivered = true);
            return await Repository.DbContext.SaveChangesAsync() > 0;
        }

        public async Task<IList<NotificationUser>> GetUserNotificationsNotDeliveredAsync(string userId)
        {
            return (await _repository.GetAllAsync(t => t.SendToId == userId &&  t.IsDelivered == false)).ToList();
        }
    }
}
