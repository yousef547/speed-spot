using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class QuotationCompetitorsRepository : RepositoryBase<QuotationCompetitor>, IQuotationCompetitorsRepository
    {
        public QuotationCompetitorsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
