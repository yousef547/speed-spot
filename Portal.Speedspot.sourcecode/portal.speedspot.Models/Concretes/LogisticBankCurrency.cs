using portal.speedspot.Models.BasesAndAbstracts;

namespace portal.speedspot.Models.Concretes
{
    public class LogisticBankCurrency : EntityBase
    {
        public int LogisticBankId { get; set; }
        public LogisticBank LogisticBank { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public string AccountNo { get; set; }
        public string IBAN { get; set; }
    }
}