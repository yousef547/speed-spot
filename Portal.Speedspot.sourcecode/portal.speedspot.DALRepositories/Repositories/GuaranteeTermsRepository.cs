using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class GuaranteeTermsRepository : RepositoryBase<GuaranteeTerm>, IGuaranteeTermsRepository
    {
        public GuaranteeTermsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
