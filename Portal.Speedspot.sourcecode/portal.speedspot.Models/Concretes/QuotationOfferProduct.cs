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
    public class QuotationOfferProduct : EntityBase
    {
        public int OfferId { get; set; }
        public QuotationOfferVersion Offer { get; set; }

        public int ProductId { get; set; }
        public SupplierOfferProduct Product { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxPercentage { get; set; }

        [Required]
        public string OfferDescription { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? SupplierPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? SupplierRate { get; set; }

        public bool IsIncluded { get; set; }

        public IList<QuotationOfferProductOrigin> SelectedOrigins { get; set; }
    }
}
