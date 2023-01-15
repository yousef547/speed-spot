using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class TechnicalSpecification : EntityBase
    {
        public int OpportunityId { get; set; }
        public Opportunity Opportunity { get; set; }
        public string Specifications { get; set; }
        public virtual List<TechnicalSpecificationAttachment> Attachments { get; set; }
        public virtual IList<TechnicalSpecificationProduct> Products { get; set; }
    }
}
