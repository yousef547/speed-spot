using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class SupplierClassification : EntityBase
    {
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public int ClassificationId { get; set; }
        public Classification Classification { get; set; }
    }
}
