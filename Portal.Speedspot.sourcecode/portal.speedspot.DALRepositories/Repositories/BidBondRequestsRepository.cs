using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class BidBondRequestsRepository : RepositoryBase<BidBondRequest>, IBidBondRequestsRepository
    {
        public BidBondRequestsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
