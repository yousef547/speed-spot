using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class ActivityTypesBiz : BizBaseT<ActivityType>
    {
        public ActivityTypesBiz(IActivityTypesRepository repository) : base(repository)
        {

        }
    }
}
