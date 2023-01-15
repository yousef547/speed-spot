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
    public class SupplierOffer : EntityBase
    {
        public int OpportunityId { get; set; }
        public Opportunity Opportunity { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ExchangeRate { get; set; }
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
        public int DeliveryTypeId { get; set; }
        public DeliveryType DeliveryType { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CIFCost { get; set; }
        //public DateTime? DeliveryDate { get; set; }
        public int DeliveryRangeFrom { get; set; }
        public int DeliveryRangeTo { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AdditionalCost { get; set; }

        public bool IsAccepted { get; set; }

        public IList<SupplierOfferProduct> Products { get; set; }
        public IList<SupplierOfferAttachment> Attachments { get; set; }
    }
}
