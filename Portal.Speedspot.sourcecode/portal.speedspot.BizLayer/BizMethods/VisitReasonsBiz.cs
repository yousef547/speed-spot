using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class VisitReasonsBiz : BizBaseT<VisitReason>
    {
        public VisitReasonsBiz(IVisitReasonsRepository repository) : base(repository)
        {

        }
    }
}
