using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class VATValuesBiz : BizBaseT<VATValue>
    {
        public VATValuesBiz(IVATValuesRepository repository) : base(repository)
        {

        }
    }
}
