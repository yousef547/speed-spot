using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class PaymentTermsRepository : RepositoryBase<PaymentTerm>, IPaymentTermsRepository
    {
        public PaymentTermsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
