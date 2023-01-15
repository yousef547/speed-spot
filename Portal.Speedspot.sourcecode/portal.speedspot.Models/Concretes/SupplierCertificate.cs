using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class SupplierCertificate : EntityBase
    {
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public int CertificateId { get; set; }
        public Certificate Certificate { get; set; }
    }
}
