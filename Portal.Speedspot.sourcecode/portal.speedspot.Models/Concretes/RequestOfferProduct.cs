using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class RequestOfferProduct:EntityBase
    {
        public int RequestOfferId { get; set; }
        public RequestOffer RequestOffer { get; set; }
        public int ProductId { get; set; }
        public TechnicalSpecificationProduct Product { get; set; }
    }
}
