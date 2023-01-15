using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class OtherNegotiationResult : EntityBase
    {
        public int QuotationId { get; set; }
        public Quotation Quotation { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string ValueBefore { get; set; }
        [Required]
        public string ValueAfter { get; set; }
    }
}
