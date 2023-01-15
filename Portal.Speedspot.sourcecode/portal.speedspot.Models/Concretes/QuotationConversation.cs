using portal.speedspot.Models.BasesAndAbstracts;
using portal.speedspot.Models.Concretes.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class QuotationConversation : EntityBase
    {
        public int QuotationId { get; set; }
        public Quotation Quotation { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        [Required]
        public string Message { get; set; }

        public int? AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
    }
}
