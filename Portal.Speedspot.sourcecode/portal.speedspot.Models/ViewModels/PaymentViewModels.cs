using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.ViewModels
{
    public class PaymentRequestViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(TotalAmount))]
        [Range(1, double.MaxValue, ErrorMessage = "InvalidRange")]
        public decimal TotalAmount { get; set; }
        public decimal LocalTotalAmount
        {
            get
            {
                if (PaymentDetailsVM != null)
                {
                    if (PaymentDetailsVM.ExchangeRate == null || PaymentDetailsVM.ExchangeRate == 0)
                    {
                        return TotalAmount;
                    }
                    else
                    {
                        return TotalAmount * PaymentDetailsVM.ExchangeRate.Value;
                    }
                }
                return TotalAmount * ExchangeRate;
            }
        }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(CurrencyId))]
        public int CurrencyId { get; set; }
        public string CurrencySymbol { get; set; }
        public decimal ExchangeRate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(Deadline))]
        public DateTime Deadline { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(DepartmentId))]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentNameAr { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(IsProject))]
        public bool IsProject
        {
            get
            {
                return ProjectId != null;
            }
        }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(ProjectId))]
        public int? ProjectId { get; set; }
        public string ProjectName { get; set; }

        public DateTime DateCreated { get; set; }

        public int SerialNo { get; set; }

        public RequestStatusEnum Status { get; set; }

        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string CreatedByUserProfilePicture { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "Direction")]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(Description))]
        public string Description { get; set; }

        [Display(Name = nameof(Directions))]
        public IList<PaymentRequestDirectionViewModel> Directions { get; set; }
        public string DirectionStr
        {
            get
            {
                return Directions != null ? string.Join(", ", Directions.Select(x => x.AccountName)) : "";
            }
        }
        public string DirectionStrAr
        {
            get
            {
                return Directions != null ? string.Join(", ", Directions.Select(x => x.AccountNameAr)) : "";
            }
        }

        //[Required(ErrorMessage = "RequiredField")]
        public PaymentDetailsViewModel PaymentDetailsVM { get; set; }

        public IFormFile ReceiptAttachmentFile { get; set; }
        public int? ReceiptAttachmentId { get; set; }
        public string ReceiptAttachmentTitle { get; set; }
        public string ReceiptAtachmentPath { get; set; }

        public IFormFile CashAttachmentFile { get; set; }
        public int? CashAttachmentId { get; set; }
        public string CashAttachmentTitle { get; set; }
        public string CashAtachmentPath { get; set; }

        public IFormFile TransferAttachmentFile { get; set; }
        public int? TransferAttachmentId { get; set; }
        public string TransferAttachmentTitle { get; set; }
        public string TransferAtachmentPath { get; set; }

        public PaymentRequestViewModel()
        {
            Deadline = DateTime.UtcNow;
            Directions = new List<PaymentRequestDirectionViewModel>
            {
                new PaymentRequestDirectionViewModel()
            };
        }
    }

    public class PaymentRequestDirectionViewModel
    {
        public int Id { get; set; }

        public int RequestPaymentId { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "Direction")]
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountNameAr { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Range(1, double.MaxValue, ErrorMessage = "InvalidRange")]
        [Display(Name = nameof(Amount))]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(Description))]
        public string Description { get; set; }
    }

    public class PaymentDetailsViewModel
    {
        public int Id { get; set; }

        public int PaymentRequestId { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(TreasuryId))]
        public int TreasuryId { get; set; }
        public TreasuryViewModel TreasuryVM { get; set; }

        [Display(Name = nameof(Description))]
        public string Description { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        public PaymentTypeEnum Type { get; set; }
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(ExchangeRate))]
        public decimal? ExchangeRate { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(ReceiptNo))]
        public string ReceiptNo { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(IssueDate))]
        public DateTime? IssueDate { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(ReceiveBankId))]
        public int? ReceiveBankId { get; set; }
        public BankViewModel ReceiveBankVM { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(DueDate))]
        public DateTime? DueDate { get; set; }
    }
}
