using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class PerformanceLGRequestsRepository : RepositoryBase<PerformanceLGRequest>, IPerformanceLGRequestsRepository
    {
        public PerformanceLGRequestsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
