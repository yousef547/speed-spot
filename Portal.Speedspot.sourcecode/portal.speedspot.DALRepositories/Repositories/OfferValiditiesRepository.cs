using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class OfferValiditiesRepository : RepositoryBase<OfferValidity>, IOfferValiditiesRepository
    {
        public OfferValiditiesRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
