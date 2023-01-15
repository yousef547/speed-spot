using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class SupplierNegotiation : QuotationConversation
    {
        public bool IsSupplier { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
