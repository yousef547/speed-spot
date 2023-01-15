using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class VisitsRepository : RepositoryBase<Visit>, IVisitsRepository
    {
        public VisitsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
