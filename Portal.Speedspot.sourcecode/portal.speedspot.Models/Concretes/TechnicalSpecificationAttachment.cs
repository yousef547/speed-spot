using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class TechnicalSpecificationAttachment : EntityBase
    {
        public int TechnicalSpecificationId { get; set; }
        public TechnicalSpecification TechnicalSpecification { get; set; }
        public int AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
    }
}
