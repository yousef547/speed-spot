using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class DeliveryPlacesBiz : BizBaseT<DeliveryPlace>
    {
        public DeliveryPlacesBiz(IDeliveryPlacesRepository repository) : base(repository)
        {

        }
    }
}
