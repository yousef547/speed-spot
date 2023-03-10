using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class ClassificationsBiz : BizBaseT<Classification>
    {
        public ClassificationsBiz(IClassificationsRepository repository) : base(repository)
        {

        }
    }
}
