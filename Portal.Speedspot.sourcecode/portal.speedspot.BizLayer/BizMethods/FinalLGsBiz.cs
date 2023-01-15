using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class FinalLGsBiz : BizBaseT<FinalLG>
    {
        public FinalLGsBiz(IFinalLGsRepository repository) : base(repository)
        {

        }
    }
}
