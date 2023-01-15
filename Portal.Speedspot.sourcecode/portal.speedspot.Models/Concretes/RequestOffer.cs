using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class RequestOffer : EntityBase
    {
        public int OpportunityId { get; set; }
        public Opportunity Opportunity { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        //public string Description { get; set; }
        public bool IsEmailSent { get; set; }
        //public int RequestOfferAttachmentId { get; set; }
        //public Attachment RequestOfferAttachment { get; set; }
        public virtual IList<RequestOfferProduct> Products { get; set; }
    }
}
