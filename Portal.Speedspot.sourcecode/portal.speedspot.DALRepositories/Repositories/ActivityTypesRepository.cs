using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class ActivityTypesRepository : RepositoryBase<ActivityType>, IActivityTypesRepository
    {
        public ActivityTypesRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
