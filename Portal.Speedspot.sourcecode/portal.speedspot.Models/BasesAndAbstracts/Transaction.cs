using System;
using System.ComponentModel.DataAnnotations.Schema;

using portal.speedspot.Models.Concretes;

using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.BasesAndAbstracts
{
    public class Transaction : EntityBase
    {
        public TransactionType Type { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public string Description { get; set; }
        public string DescriptionAr { get; set; }

        public DateTime DateCreated { get; set; }

        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ExchangeRate { get; set; }
    }
}
