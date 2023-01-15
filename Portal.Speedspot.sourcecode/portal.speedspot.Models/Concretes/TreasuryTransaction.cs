using portal.speedspot.Models.BasesAndAbstracts;

namespace portal.speedspot.Models.Concretes
{
    public class TreasuryTransaction : Transaction
    {
        public int TreasuryId { get; set; }
        public Treasury Treasury { get; set; }
    }
}
