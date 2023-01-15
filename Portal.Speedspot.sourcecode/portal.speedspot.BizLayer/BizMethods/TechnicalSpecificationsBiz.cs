using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class TechnicalSpecificationsBiz : BizBaseT<TechnicalSpecification>
    {
        public TechnicalSpecificationsBiz(ITechnicalSpecificationsRepository repository) : base(repository)
        {

        }
    }
}
