using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class PerformanceLGsRepository : RepositoryBase<PerformanceLG>, IPerformanceLGsRepository
    {
        public PerformanceLGsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
