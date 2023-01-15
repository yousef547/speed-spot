using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class Product : EntityBase
    {
        [Required]
        public int CategoryId { get; set; }
        public virtual ProductCategory Category { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string AutoCode { get; set; }

        public IList<SupplierProduct> Suppliers { get; set; }
    }
}
