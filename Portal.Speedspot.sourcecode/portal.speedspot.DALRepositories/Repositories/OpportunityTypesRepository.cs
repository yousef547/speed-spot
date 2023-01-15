using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class OpportunityTypesRepository : RepositoryBase<OpportunityType>, IOpportunityTypesRepository
    {
        public OpportunityTypesRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
