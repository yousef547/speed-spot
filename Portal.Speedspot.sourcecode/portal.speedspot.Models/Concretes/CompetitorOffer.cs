using portal.speedspot.Models.BasesAndAbstracts;
using portal.speedspot.Models.Concretes.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class CompetitorOffer : EntityBase
    {
        public int CompetitorId { get; set; }
        public Competitor Competitor { get; set; }

        public int QuotationId { get; set; }
        public Quotation Quotation { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DownPaymentPercentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UponDeliveryPercentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AfterInstallationPercentage { get; set; }

        public int ValidityId { get; set; }
        public OfferValidity Validity { get; set; }
        public int? OriginDocumentId { get; set; }
        public OriginDocument OriginDocument { get; set; }
        public int GuaranteeTermId { get; set; }
        public GuaranteeTerm GuaranteeTerm { get; set; }

        public int DeliveryPlaceId { get; set; }
        public DeliveryPlace DeliveryPlace { get; set; }
        public int DeliveryFromDays { get; set; }
        public int DeliveryToDays { get; set; }

        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ExchangeRate { get; set; }

        public bool IsVATIncluded { get; set; }

        public string Notes { get; set; }

        public string Supplier { get; set; }

        [Required]
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public IList<CompetitorOfferCertificate> Certificates { get; set; }
        public IList<CompetitorOfferProduct> Products { get; set; }
    }
}
