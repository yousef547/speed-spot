using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class FinancialAccountsRepository : RepositoryBase<FinancialAccount>, IFinancialAccountsRepository
    {
        public FinancialAccountsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
