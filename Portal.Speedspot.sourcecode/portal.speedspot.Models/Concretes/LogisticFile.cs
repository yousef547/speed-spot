using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class LogisticFile : EntityBase
    {
        [Required]
        public int LogisticId { get; set; }
        public Logistic Logistic { get; set; }
        [Required]
        public int AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
    }
}
