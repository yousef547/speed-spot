using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class GuaranteeTermsBiz : BizBaseT<GuaranteeTerm>
    {
        public GuaranteeTermsBiz(IGuaranteeTermsRepository repository) : base(repository)
        {

        }
    }
}
