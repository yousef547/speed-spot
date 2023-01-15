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
    public class BookTender : EntityBase
    {
        public int OpportunityId { get; set; }
        public Opportunity Opportunity { get; set; }
        public string AssignedToId { get; set; }
        public ApplicationUser AssignedTo { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Fees { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal VAT { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public int? ReceiptAttachmentId { get; set; }
        public Attachment ReceiptAttachment { get; set; }
        public string Description { get; set; }
        //public int? TodoTaskId { get; set; }
        //public ToDoTask TodoTask { get; set; }
        public virtual BookTenderRequest Request { get; set; }
    }
}
