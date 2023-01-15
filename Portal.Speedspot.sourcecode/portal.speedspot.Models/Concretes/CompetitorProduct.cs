using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class CompetitorProduct : EntityBase
    {
        public int CompetitorId { get; set; }
        public Competitor Competitor { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
