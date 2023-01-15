using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class PurchaseOrderSupplierOffer : EntityBase
    {
        public int PurchaseOrderId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }

        public int SupplierOfferId { get; set; }
        public SupplierOffer SupplierOffer { get; set; }
    }
}
