using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class DeliveryTypesBiz : BizBaseT<DeliveryType>
    {
        public DeliveryTypesBiz(IDeliveryTypesRepository repository) : base(repository)
        {

        }
    }
}
