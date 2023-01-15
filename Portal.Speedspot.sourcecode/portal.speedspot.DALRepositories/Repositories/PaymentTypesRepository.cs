using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class PaymentTypesRepository : RepositoryBase<PaymentType>, IPaymentTypesRepository
    {
        public PaymentTypesRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
