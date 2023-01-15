using portal.speedspot.Models.BasesAndAbstracts;
using portal.speedspot.Models.Concretes.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.Concretes
{
    public class PaymentRequest : EntityBase
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public DateTime Deadline { get; set; }

        public int? ProjectId { get; set; }
        public Project Project { get; set; }

        public DateTime DateCreated { get; set; }

        public int SerialNo { get; set; }

        public RequestStatusEnum Status { get; set; }

        public string RejectReason { get; set; }

        [Required]
        public string CreatedByUserId { get; set; }
        public ApplicationUser CreatedByUser { get; set; }

        public int? ReceiptAttachmentId { get; set; }
        public Attachment ReceiptAttachment { get; set; }

        public int? CashAttachmentId { get; set; }
        public Attachment CashAttachment { get; set; }

        public int? TransferAttachmentId { get; set; }
        public Attachment TransferAttachment { get; set; }

        public virtual IList<PaymentRequestDirection> Directions { get; set; }
        public virtual PaymentDetails PaymentDetails { get; set; }
    }
}
