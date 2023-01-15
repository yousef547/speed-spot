using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class PaymentTermsBiz : BizBaseT<PaymentTerm>
    {
        public PaymentTermsBiz(IPaymentTermsRepository repository) : base(repository)
        {

        }
    }
}
