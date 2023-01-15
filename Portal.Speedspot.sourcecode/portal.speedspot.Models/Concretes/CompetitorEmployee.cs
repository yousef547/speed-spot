using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class CompetitorEmployee : EntityBase
    {
        public int CompetitorId { get; set; }
        public Competitor Competitor { get; set; }
        public int EmployeeId { get; set; }
        public PartnerEmployee Employee { get; set; }
    }
}
