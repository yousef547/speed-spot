using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class BankViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "CountryId")]
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryNameAr { get; set; }
        public CountryViewModel CountryVM { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "NameAr")]
        public string NameAr { get; set; }

        //[Required(ErrorMessage = "RequiredField")]
        //[Display(Name = "BidBondPercentage")]
        //[Range(1, 100, ErrorMessage = "InvalidRange")]
        //public decimal BidBondPercentage { get; set; }

        //[Required(ErrorMessage = "RequiredField")]
        //[Display(Name = "BidBondFees")]
        //public decimal BidBondFees { get; set; }

        public bool IsArchived { get; set; }

        public virtual IList<BankBranchViewModel> BranchVMs { get; set; }

        public int BranchesCount
        {
            get
            {
                return BranchVMs.Where(b => !b.IsArchived).ToList().Count;
            }
        }
        public BankViewModel()
        {
            if (BranchVMs == null) BranchVMs = new List<BankBranchViewModel>();
        }

        public int BankFeesId { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "BidBondMinFees")]
        [Range(0, double.MaxValue, ErrorMessage = "InvalidRange")]
        public decimal BidBondMinFees { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "BidBondPercentage")]
        [Range(0, 100, ErrorMessage = "InvalidRange")]
        public decimal BidBondPercentage { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "BidBondCreditPercentage")]
        [Range(0, 100, ErrorMessage = "InvalidRange")]
        public decimal BidBondCreditPercentage { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "PerformanceLGMinFees")]
        [Range(0, double.MaxValue, ErrorMessage = "InvalidRange")]
        public decimal PerformanceLGMinFees { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "PerformanceLGPercentage")]
        [Range(0, 100, ErrorMessage = "InvalidRange")]
        public decimal PerformanceLGPercentage { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "PerformanceLGCreditPercentage")]
        [Range(0, 100, ErrorMessage = "InvalidRange")]
        public decimal PerformanceLGCreditPercentage { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "FinalLGMinFees")]
        [Range(0, double.MaxValue, ErrorMessage = "InvalidRange")]
        public decimal FinalLGMinFees { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "FinalLGPercentage")]
        [Range(0, 100, ErrorMessage = "InvalidRange")]
        public decimal FinalLGPercentage { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "FinalLGCreditPercentage")]
        [Range(0, 100, ErrorMessage = "InvalidRange")]
        public decimal FinalLGCreditPercentage { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "ChequeCollectionMinFees")]
        [Range(0, double.MaxValue, ErrorMessage = "InvalidRange")]
        public decimal ChequeCollectionMinFees { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "ChequeCollectionPercentage")]
        [Range(0, 100, ErrorMessage = "InvalidRange")]
        public decimal ChequeCollectionPercentage { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "ChequeCollectionCreditPercentage")]
        [Range(0, 100, ErrorMessage = "InvalidRange")]
        public decimal ChequeCollectionCreditPercentage { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "TransferMinFees")]
        [Range(0, double.MaxValue, ErrorMessage = "InvalidRange")]
        public decimal TransferMinFees { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "TransferPercentage")]
        [Range(0, 100, ErrorMessage = "InvalidRange")]
        public decimal TransferPercentage { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "TransferCreditPercentage")]
        [Range(0, 100, ErrorMessage = "InvalidRange")]
        public decimal TransferCreditPercentage { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "LCMinFees")]
        [Range(0, double.MaxValue, ErrorMessage = "InvalidRange")]
        public decimal LCMinFees { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "LCPercentage")]
        [Range(0, 100, ErrorMessage = "InvalidRange")]
        public decimal LCPercentage { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "LCCreditPercentage")]
        [Range(0, 100, ErrorMessage = "InvalidRange")]
        public decimal LCCreditPercentage { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "Form4MinFees")]
        [Range(0, double.MaxValue, ErrorMessage = "InvalidRange")]
        public decimal Form4MinFees { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "Form4Percentage")]
        [Range(0, 100, ErrorMessage = "InvalidRange")]
        public decimal Form4Percentage { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "Form4CreditPercentage")]
        [Range(0, 100, ErrorMessage = "InvalidRange")]
        public decimal Form4CreditPercentage { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "Form5MinFees")]
        [Range(0, double.MaxValue, ErrorMessage = "InvalidRange")]
        public decimal Form5MinFees { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "Form5Percentage")]
        [Range(0, 100, ErrorMessage = "InvalidRange")]
        public decimal Form5Percentage { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "Form5CreditPercentage")]
        [Range(0, 100, ErrorMessage = "InvalidRange")]
        public decimal Form5CreditPercentage { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "ForginExchangeMinFees")]
        [Range(0, double.MaxValue, ErrorMessage = "InvalidRange")]
        public decimal ForginExchangeMinFees { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "ForginExchangePercentage")]
        [Range(0, 100, ErrorMessage = "InvalidRange")]
        public decimal ForginExchangePercentage { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "ForginExchangeCreditPercentage")]
        [Range(0, 100, ErrorMessage = "InvalidRange")]
        public decimal ForginExchangeCreditPercentage { get; set; }
    }

    public class BankBranchViewModel
    {
        public int Id { get; set; }
        public int BankId { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "NameAr")]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "SwiftCode")]
        public string SwiftCode { get; set; }

        public bool IsArchived { get; set; }
    }

    public class Bankgrouping
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryNameAr { get; set; }
        public IList<BankViewModel> BankVMs { get; set; }

        public int BanksCount
        {
            get
            {
                return BankVMs.Count;
            }
        }
    }
}
