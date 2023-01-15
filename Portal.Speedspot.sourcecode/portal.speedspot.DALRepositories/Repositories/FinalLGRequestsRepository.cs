using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class FinalLGRequestsRepository : RepositoryBase<FinalLGRequest>, IFinalLGRequestsRepository
    {
        public FinalLGRequestsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
