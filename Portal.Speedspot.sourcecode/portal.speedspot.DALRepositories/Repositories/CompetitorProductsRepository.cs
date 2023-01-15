using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class CompetitorProductsRepository : RepositoryBase<CompetitorProduct>, ICompetitorProductsRepository
    {
        public CompetitorProductsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
