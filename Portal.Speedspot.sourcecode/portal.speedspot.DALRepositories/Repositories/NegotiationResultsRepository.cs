using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class NegotiationResultsRepository : RepositoryBase<NegotiationResult>, INegotiationResultsRepository
    {
        public NegotiationResultsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
