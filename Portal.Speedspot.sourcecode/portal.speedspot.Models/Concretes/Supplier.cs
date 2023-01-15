using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class Supplier : Partner
    {
        [Range(10, 100)]
        [Required]
        public int Rank { get; set; }
        public Supplier Parent { get; set; }

        public bool IsAgency { get; set; }
        public int? CompetitorId { get; set; }
        public Competitor Competitor { get; set; }

        public virtual IList<Supplier> Childs { get; set; }
        public virtual IList<SupplierDepartment> Departments { get; set; }
        public virtual IList<SupplierClassification> Classifications { get; set; }
        public virtual IList<SupplierContact> Contacts { get; set; }
        public virtual IList<SupplierFile> Files { get; set; }
        public virtual IList<SupplierEmployee> Employees { get; set; }
        public virtual IList<SupplierCertificate> Certificates { get; set; }
        public virtual IList<SupplierProduct> Products { get; set; }
        public virtual IList<SupplierBank> Banks { get; set; }
    }
}
