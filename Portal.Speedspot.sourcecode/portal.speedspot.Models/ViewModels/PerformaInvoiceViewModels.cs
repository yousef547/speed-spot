using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class PerformaInvoiceViewModel
    {
        [Required]
        public int PurchaseOrderId { get; set; }

        public string DepartmentName { get; set; }

        public string CustomerName { get; set; }

        public string VATNo { get; set; }

        [Required]
        public string InvoiceNo { get; set; }

        [Required]
        public int CurrencyId { get; set; }

        [Required]
        public decimal ExchangeRate { get; set; }

        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; }

        [Required]
        public int PaymentTermId { get; set; }
        public string PaymentTermName { get; set; }
        public int PaymentTermDaysNo { get; set; }

        public DateTime DueDate { get; set; }

        public string Message { get; set; }

        public string GeneratedCode { get; set; }

        public bool IsTaxIncluded { get; set; }
    }
}
