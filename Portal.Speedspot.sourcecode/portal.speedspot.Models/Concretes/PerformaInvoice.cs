
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using portal.speedspot.Models.BasesAndAbstracts;

namespace portal.speedspot.Models.Concretes
{
    public class PerformaInvoice : EntityBase
    {
        public int PurchaseOrderId { get; set; }

        [Required]
        public string InvoiceNo { get; set; }

        [Required]
        public int CurrencyId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ExchangeRate { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        [Required]
        public int PaymentTermId { get; set; }

        public string Message { get; set; }

        [Required]
        public string GeneratedCode { get; set; }

        [Required]
        public bool IsTaxIncluded { get; set; }
    }
}
