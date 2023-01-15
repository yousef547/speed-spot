using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class SupplierOfferProduct : EntityBase
    {
        public int OfferId { get; set; }
        public SupplierOffer Offer { get; set; }
        public int TechnicalSpecificationProductId { get; set; }
        public TechnicalSpecificationProduct TechnicalSpecificationProduct { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int? AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
    }
}
