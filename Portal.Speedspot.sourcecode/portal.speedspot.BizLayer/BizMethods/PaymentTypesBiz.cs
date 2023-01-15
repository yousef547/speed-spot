using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class PaymentTypesBiz : BizBaseT<PaymentType>
    {
        public PaymentTypesBiz(IPaymentTypesRepository repository) : base(repository)
        {

        }
    }
}
