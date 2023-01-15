using portal.speedspot.Models.BasesAndAbstracts;
using portal.speedspot.Models.Concretes.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class QuotationOffer : EntityBase
    {
        public int QuotationId { get; set; }
        public Quotation Quotation { get; set; }

        public bool IsTwoEnvelopes { get; set; }

        [Required]
        public string Code { get; set; }

        public IList<QuotationOfferVersion> Versions { get; set; }
    }
}
