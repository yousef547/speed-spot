using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class CompetitorBankCurrency : EntityBase
    {
        public int CompetitorBankId { get; set; }
        public CompetitorBank CompetitorBank { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public string AccountNo { get; set; }
        public string IBAN { get; set; }
    }
}
