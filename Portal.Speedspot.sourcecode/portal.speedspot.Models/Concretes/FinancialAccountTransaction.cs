using portal.speedspot.Models.BasesAndAbstracts;

namespace portal.speedspot.Models.Concretes
{
    public class FinancialAccountTransaction : Transaction
    {
        public int FinancialAccountId { get; set; }
        public FinancialAccount FinancialAccount { get; set; }
    }
}
