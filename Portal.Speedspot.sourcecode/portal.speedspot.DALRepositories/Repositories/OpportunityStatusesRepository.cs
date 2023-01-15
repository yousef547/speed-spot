using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class OpportunityStatusesRepository : RepositoryBase<OpportunityStatus>, IOpportunityStatusesRepository
    {
        public OpportunityStatusesRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
