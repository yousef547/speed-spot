using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class SupplierProduct : EntityBase
    {
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
