using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class RejectReasonsRepository : RepositoryBase<RejectReason>, IRejectReasonsRepository
    {
        public RejectReasonsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
