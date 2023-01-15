using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using portal.speedspot.Models.Concretes;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.ViewModels
{
    public class TreasuryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(DepartmentId))]
        public int DepartmentId { get; set; }

        public TreasuryType Type { get; set; }

        public string Name { get; set; }

        public int CurrencyId { get; set; }
        public CurrencyViewModel CurrencyVM { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OpeningBalance { get; set; }

        public DateTime DateCreated { get; set; }

        public int? BankId { get; set; }
        public BankViewModel BankVM { get; set; }

        public string AccountNo { get; set; }

        public bool IsArchived { get; set; }

        public string Description
        {
            get
            {
                return BankId is not null ? $"{BankVM?.Name} - #{AccountNo}" : Name;
            }
        }

        public string DescriptionAr
        {
            get
            {
                return BankId is not null ? $"{BankVM?.NameAr} - #{AccountNo}" : Name;
            }
        }

        public IList<TreasuryTransactionViewModel> TransactionVMs { get; set; }

        public decimal Balance
        {
            get
            {
                decimal balance = OpeningBalance;
                if (TransactionVMs != null)
                {
                    balance += TransactionVMs.Where(t => t.Type == TransactionType.Debit).Sum(t => t.Amount * t.ExchangeRate);
                    balance -= TransactionVMs.Where(t => t.Type == TransactionType.Credit).Sum(t => t.Amount * t.ExchangeRate);
                }
                return balance;
            }
        }
    }

    public class TreasuryTransactionViewModel
    {
        public int Id { get; set; }

        public int TreasuryId { get; set; }

        public TransactionType Type { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }
        public string DescriptionAr { get; set; }

        public DateTime DateCreated { get; set; }

        public int CurrencyId { get; set; }

        public decimal ExchangeRate { get; set; }
    }
}
