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
    public class QuotationOfferVersion : EntityBase
    {
        public int OfferId { get; set; }
        public QuotationOffer Offer { get; set; }
        public int Version { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string AttentionTo { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }

        public string AttentionTo2 { get; set; }
        public string Phone2 { get; set; }
        public string Email2 { get; set; }

        [Required]
        public string CoverLetter { get; set; }

        [Required]
        public string TechnicalSpecifications { get; set; }

        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public int ValidityId { get; set; }
        public OfferValidity Validity { get; set; }
        public int DeliveryFromDays { get; set; }
        public int DeliveryToDays { get; set; }
        public string DeliveryNotes { get; set; }

        public int DeliveryPlaceId { get; set; }
        public DeliveryPlace DeliveryPlace { get; set; }

        public int? OriginDocumentId { get; set; }
        public OriginDocument OriginDocument { get; set; }

        public int? GuaranteeTermId { get; set; }
        public GuaranteeTerm GuaranteeTerm { get; set; }

        public int? DeliveryGuaranteeMonths { get; set; }
        public int? CommissionGuaranteeMonths { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DownPaymentPercentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UponDeliveryPercentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AfterInstallationPercentage { get; set; }

        public string Notes { get; set; }
        public string TechnicalNotes { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Factor { get; set; }

        public bool IsSelected { get; set; }

        [Required]
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public IList<QuotationOfferCertificate> Certificates { get; set; }
        public IList<QuotationOfferProduct> Products { get; set; }
        public IList<AlternateVersion> AlternateVersions { get; set; }
    }
}
