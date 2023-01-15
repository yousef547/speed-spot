using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class AlternateVersion : EntityBase
    {
        public int QuotationOfferVersionId { get; set; }

        public QuotationOfferVersion QuotationOfferVersion { get; set; }

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

        public int? DeliveryGuaranteeMonths { get; set; }
        public int? CommissionGuaranteeMonths { get; set; }

        public int? GuaranteeTermId { get; set; }
        public GuaranteeTerm GuaranteeTerm { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal DownPaymentPercentage { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal UponDeliveryPercentage { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal AfterInstallationPercentage { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Factor { get; set; }

        public int? SerialNo { get; set; }

        public IList<AlternateVersionCertificate> Certificates { get; set; }

        public IList<AlternateVersionProduct> Products { get; set; }
    }
}
