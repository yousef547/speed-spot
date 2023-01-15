using portal.speedspot.Models.BasesAndAbstracts;
using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.Concretes.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.BasesAndAbstracts
{
    public class LetterOfGuarantee : EntityBase
    {
        public string AssignedToId { get; set; }
        public ApplicationUser AssignedTo { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Amount { get; set; }
        public string Description { get; set; }
        public bool CashType { get; set; }
        public string AttachmentTitle { get; set; }
        public string AttachmentPath { get; set; }
        public DateTime? IssueDate { get; set; }
        public bool? IsCredit { get; set; }
        public int? NoOfPeriods { get; set; }
        public int? BankId { get; set; }
        public Bank Bank { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? PaidAmount { get; set; }
        public int? BankBranchId { get; set; }
        public BankBranch BankBranch { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Percentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Fees { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string LetterOfGuaranteeNo { get; set; }

        public DateTime? ReceivingDate { get; set; }
        public string EmployeeName { get; set; }
        public string ReceiptAttachmentPath { get; set; }
        public string ReceiptAttachmentTitle { get; set; }
    }
}
