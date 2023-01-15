using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.ViewModels
{
    public class FinancialAccountViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(Name))]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(NameAr))]
        public string NameAr { get; set; }
        public int? ParentId { get; set; }
        public FinancialAccountViewModel ParentVM { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(Code))]
        public string Code { get; set; }

        public string AutoCode { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentCode { get; set; }
        public decimal? Price { get; }
        public bool IsArchived { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(Type))]
        public FinancialAccountType Type { get; set; }

        [Display(Name = "GeneratedCode")]
        public string GeneratedCode
        {
            get
            {
                return (ParentVM != null ? ParentVM.GeneratedCode : DepartmentCode) + AutoCode;
            }
        }

        public string Description
        {
            get
            {
                return Name + " - " + GeneratedCode;
            }
        }
        public string DescriptionAr
        {
            get
            {
                return NameAr + " - " + GeneratedCode;
            }
        }
        public virtual IList<FinancialAccountViewModel> ChildVMs { get; set; }

        public IList<FinancialAccountViewModel> UnarchivedChildVMs
        {
            get
            {
                return ChildVMs != null ? ChildVMs.Where(c => !c.IsArchived).ToList() : new();
            }
        }

        public int ChildsCount
        {
            get
            {
                return UnarchivedChildVMs.Count;
            }
        }

        public FinancialAccountViewModel()
        {
            if (ChildVMs == null) ChildVMs = new List<FinancialAccountViewModel>();
        }
    }

    public class FinancialAccountTransactionViewModel
    {
        public int Id { get; set; }

        public int FinancialAccountId { get; set; }
        public FinancialAccountViewModel FinancialAccountVM { get; set; }

        public TransactionType Type { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }
        public string DescriptionAr { get; set; }

        public DateTime DateCreated { get; set; }

        public int CurrencyId { get; set; }
        public CurrencyViewModel CurrencyVM { get; set; }

        public decimal ExchangeRate { get; set; }
    }
}
