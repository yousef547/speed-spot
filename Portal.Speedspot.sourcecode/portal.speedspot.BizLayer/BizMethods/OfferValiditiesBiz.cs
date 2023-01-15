using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class OfferValiditiesBiz : BizBaseT<OfferValidity>
    {
        public OfferValiditiesBiz(IOfferValiditiesRepository repository) : base(repository)
        {

        }
    }
}
