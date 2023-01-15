using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.Concretes
{
    public class Treasury : EntityBase
    {
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public TreasuryType Type { get; set; }

        public string Name { get; set; }

        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OpeningBalance { get; set; }

        public DateTime DateCreated { get; set; }

        public int? BankId { get; set; }
        public Bank Bank { get; set; }

        public string AccountNo { get; set; }

        public virtual IList<TreasuryTransaction> Transactions { get; set; }
    }
}
