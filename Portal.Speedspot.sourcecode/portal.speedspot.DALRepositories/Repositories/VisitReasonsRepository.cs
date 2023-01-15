using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class VisitReasonsRepository : RepositoryBase<VisitReason>, IVisitReasonsRepository
    {
        public VisitReasonsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
