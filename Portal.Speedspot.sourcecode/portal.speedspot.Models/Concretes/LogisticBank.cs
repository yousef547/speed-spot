using portal.speedspot.Models.BasesAndAbstracts;
using System.Collections.Generic;

namespace portal.speedspot.Models.Concretes
{
    public class LogisticBank : EntityBase
    {
        public int LogisticId { get; set; }
        public Logistic Logistic { get; set; }
        public int BranchId { get; set; }
        public BankBranch Branch { get; set; }

        public IList<LogisticBankCurrency> Currencies { get; set; }
    }
}