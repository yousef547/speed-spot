using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class TechnicalSpecificationProduct : EntityBase
    {
        public int Index { get; set; }
        public int TechnicalSpecificationId { get; set; }
        public TechnicalSpecification TechnicalSpecification { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int? ProductOriginId { get; set; }
        public ProductOrigin ProductOrigin { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Quantity { get; set; }
        [Required]
        public string Description { get; set; }
        public int? AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
        public virtual IList<TechnicalSpecificationProductOrigin> RequestedOrigins { get; set; }
    }
}
