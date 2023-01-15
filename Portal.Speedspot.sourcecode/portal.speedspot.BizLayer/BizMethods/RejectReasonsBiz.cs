using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class RejectReasonsBiz : BizBaseT<RejectReason>
    {
        public RejectReasonsBiz(IRejectReasonsRepository repository) : base(repository)
        {

        }
    }
}
