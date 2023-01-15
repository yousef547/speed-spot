using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class OpportunityAttachment : EntityBase
    {
        public int OpportunityId { get; set; }
        public Opportunity Opportunity { get; set; }
        public int AttachmentId { get; set; }
        public Attachment Attachment { get; set; }
    }
}
