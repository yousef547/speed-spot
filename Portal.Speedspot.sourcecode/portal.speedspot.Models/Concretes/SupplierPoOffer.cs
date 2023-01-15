using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class SupplierPoOffer : EntityBase
    {
        public int SupplierOfferId { get; set; }
        public SupplierOffer SupplierOffer { get; set; }
        public int SupplierPoId { get; set; }
        public SupplierPo SupplierPo { get; set; }
    }
}
