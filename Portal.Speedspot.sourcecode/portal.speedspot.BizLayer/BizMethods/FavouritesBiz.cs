using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class FavouritesBiz : BizBaseT<Favourite>
    {
        public FavouritesBiz(IFavouritesRepository repository) : base(repository)
        {

        }

        public async Task<IList<Favourite>> GetAllFavouriteAsync(string userId, int? enumValue = null)
        {
            return (await Repository.GetAllAsync(t => t.UserId == userId && (enumValue == null || t.TypeId == enumValue),x=>x.Type)).ToList();
        }

        //public async Task<IList<NotificationUser>> GetUserNotificationsAsync(string userId, bool? isRead = null)
        //{
        //    return (await _repository.GetAllAsync(t => t.SendToId == userId && (isRead == null || t.IsRead == isRead) &&
        //    !(t.IsRead && t.Notification.CreatedDate < DateTime.UtcNow.AddDays(-7)),
        //        x => x.Notification.CreatedBy)).OrderByDescending(o => o.Id).ToList();
        //}

        public async Task<bool> IsFavourite(int typeId, string userId, int? itemId)
        {
            var item = await Repository.GetFirstOrDefaultAsync(f => f.TypeId == typeId && f.UserId == userId && f.ItemId == itemId);
            if (item != null)
                return true;

            return false;
        }

        public async Task<bool> DeleteFavourite(int typeId, string userId, int? itemId)
        {
            return await Repository.DeleteAsync(await Repository.GetFirstAsync(f => f.TypeId == typeId && f.UserId == userId && f.ItemId == itemId));
        }
    }
}
