using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class BidBondsRepository : RepositoryBase<BidBond>, IBidBondsRepository
    {
        public BidBondsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
