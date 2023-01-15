using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
   public class CustomerPo : EntityBase
    {
        public int PurchaseOrderId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        public string CustomerPONumber { get; set; }
        public DateTime? StartDate { get; set; }
        public int? DeliveryPeriod { get; set; }
        public string Code { get; set; }
        public IList<CustomerPoAttachment> Attachments { get; set; }
    }
}
