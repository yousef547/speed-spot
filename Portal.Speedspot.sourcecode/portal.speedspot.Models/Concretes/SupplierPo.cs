using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.Concretes
{
    public class SupplierPo : EntityBase
    {
        public int PurchaseOrderId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        public string SupplierPONumber { get; set; }
        public DateTime? StartDate { get; set; }
        public SupplierPoType Type { get; set; }
        public int? DeliveryPeriod { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public IList<SupplierPoOffer> Offers { get; set; }
    }
}
