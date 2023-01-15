using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class UserActivitiesBiz : BizBaseT<UserActivity>
    {
        public UserActivitiesBiz(IUserActivitiesRepository repository) : base(repository)
        {

        }

        public override async Task<IList<UserActivity>> GetAllAsync()
        {
            return await Repository.GetAllAsync(x => x.User.Department);
        }

        public async Task<IList<UserActivity>> GetByUserAsync(string userId)
        {
            return (await Repository.GetAllAsync(a => a.UserId == userId && a.CreatedDateTime >= DateTime.UtcNow.AddDays(-30),
                x => x.User.Department)).OrderByDescending(o => o.CreatedDateTime).ToList();
        }
    }
}
