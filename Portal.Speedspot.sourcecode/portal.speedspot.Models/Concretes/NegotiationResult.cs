using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class NegotiationResult : EntityBase
    {
        public int QuotationId { get; set; }
        public Quotation Quotation { get; set; }

        public int? SelectedVersionId { get; set; }
        public QuotationOfferVersion SelectedVersion { get; set; }

        public int? SelectedAlternateId { get; set; }
        public AlternateVersion SelectedAlternate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OfferValue { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DownPaymentPercentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UponDeliveryPercentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AfterInstallationPercentage { get; set; }

        public int DeliveryFromDays { get; set; }
        public int DeliveryToDays { get; set; }
    }
}
