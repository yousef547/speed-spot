using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class CurrenciesRepository : RepositoryBase<Currency>, ICurrenciesRepository
    {
        public CurrenciesRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
