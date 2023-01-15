using portal.speedspot.Models.BasesAndAbstracts;
using portal.speedspot.Models.Concretes.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class BidBond : EntityBase
    {
        public int OpportunityId { get; set; }
        public Opportunity Opportunity { get; set; }
        public string AssignedToId { get; set; }
        public ApplicationUser AssignedTo { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Amount { get; set; }
        public string Description { get; set; }
        public bool CashType { get; set; }
        public int? BidBondAttachmentId { get; set; }
        public Attachment BidBondAttachment { get; set; }
        public string BidBondOff_ReceiptNo { get; set; }
        public DateTime? BidBondOff_PaymentDate { get; set; }
        public DateTime? BidBondOn_IssueDate { get; set; }
        public bool? BidBondOn_IsCredit { get; set; }
        public int? BidBondOn_NoOfPeriods { get; set; }
        public int? BidBondOn_BankId { get; set; }
        public Bank BidBondOn_Bank { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? BidBondOn_HoldAmount { get; set; }
        public string BidBondOn_BankBranch { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? BidBondOn_Percentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? BidBondOn_Fees { get; set; }
        public string BidBondOn_BidBondNo { get; set; }
        public DateTime? BidBondOn_ExpiryDate { get; set; }
        //public int? TodoTaskId { get; set; }
        //public ToDoTask TodoTask { get; set; }
        public virtual BidBondRequest Request { get; set; }
    }
}
