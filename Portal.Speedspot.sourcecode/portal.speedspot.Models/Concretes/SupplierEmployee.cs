using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class SupplierEmployee : EntityBase
    {
        [Required]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        public PartnerEmployee Employee { get; set; }
    }
}
