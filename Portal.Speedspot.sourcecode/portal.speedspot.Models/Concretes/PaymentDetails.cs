using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.Concretes
{
    public class PaymentDetails : EntityBase
    {
        public int PaymentRequestId { get; set; }
        public PaymentRequest PaymentRequest { get; set; }
        public int TreasuryId { get; set; }
        public Treasury Treasury { get; set; }
        public string Description { get; set; }
        public PaymentTypeEnum Type { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ExchangeRate { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime? IssueDate { get; set; }
        public int? ReceiveBankId { get; set; }
        public Bank ReceiveBank { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
