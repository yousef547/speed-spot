using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class SupplierContact : EntityBase
    {
        [Required]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        [Required]
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}
