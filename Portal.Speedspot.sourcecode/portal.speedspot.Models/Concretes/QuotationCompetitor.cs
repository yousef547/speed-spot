using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class QuotationCompetitor : EntityBase
    {
        public int QuotationId { get; set; }
        public Quotation Quotation { get; set; }

        public int CompetitorId { get; set; }
        public Competitor Competitor { get; set; }

        public bool IsTechnicalAccepted { get; set; }
    }
}
