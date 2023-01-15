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
    public class PerformanceLG : LetterOfGuarantee
    {
        public virtual PerformanceLGRequest Request { get; set; }

        public int QuotationId { get; set; }
        public virtual Quotation Quotation { get; set; }

        public AdvancePaymentType? AdvanceType { get; set; }

        public int? IssueBankId { get; set; }
        public Bank IssueBank { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? AdvanceAmount { get; set; }
        public string AdvanceNo { get; set; }
        public DateTime? AdvanceDate { get; set; }

        public int? ReceiveBankId { get; set; }
        public Bank ReceiveBank { get; set; }

        public string AdvanceAttachmentTitle { get; set; }
        public string AdvanceAttachmentPath { get; set; }
    }
}
