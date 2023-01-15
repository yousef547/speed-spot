using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class SupplierFile : EntityBase
    {
        [Required]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        [Required]
        public int AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
    }
}
