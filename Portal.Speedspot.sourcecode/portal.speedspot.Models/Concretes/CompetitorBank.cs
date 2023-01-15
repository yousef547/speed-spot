using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class CompetitorBank : EntityBase
    {
        public int CompetitorId { get; set; }
        public Competitor Competitor { get; set; }
        public int BranchId { get; set; }
        public BankBranch Branch { get; set; }
        public IList<CompetitorBankCurrency> Currencies { get; set; }
    }
}
