using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class PaymentRequestDirection : EntityBase
    {
        public int PaymentRequestId { get; set; }
        public PaymentRequest PaymentRequest { get; set; }

        public int AccountId { get; set; }
        public FinancialAccount Account { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public string Description { get; set; }
    }
}
