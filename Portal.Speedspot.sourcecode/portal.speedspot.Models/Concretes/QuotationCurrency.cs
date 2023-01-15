using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class QuotationCurrency : EntityBase
    {
        public int QuotationId { get; set; }
        public Quotation Quotation { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ExchangeRate { get; set; }
    }
}
