using Microsoft.AspNetCore.Http;
using portal.speedspot.Models.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.ViewModels
{
    public class Period
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }

    public class BidBondViewModel
    {
        public int Id { get; set; }
        public int OpportunityId { get; set; }
        public OpportunityInfoViewModel OpportunityVM { get; set; }
        [Display(Name = "AssignedToId")]
        public string AssignedToId { get; set; }
        public string AssignedToName { get; set; }
        [Display(Name = "Amount")]
        public decimal? Amount { get; set; }
        [Display(Name = "IssueDate")]
        [DataType(DataType.Date)]
        public DateTime? BidBondOn_IssueDate { get; set; }
        [Display(Name = "PaymentDate")]
        [DataType(DataType.Date)]
        public DateTime? BidBondOff_PaymentDate { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "CashType")]
        public bool CashType { get; set; }
        [Display(Name = "BidBondAttachmentId")]
        public int? BidBondAttachmentId { get; set; }
        public string BidBondAttachmentPath { get; set; }
        public string BidBondAttachmentTitle { get; set; }
        public string BidBondAttachmentUploadedByName { get; set; }
        [Display(Name = nameof(BidBondFile))]
        [RequiredIf(nameof(BidBondOff_ReceiptNo), ErrorMessage = "RequiredField")]
        public IFormFile BidBondFile { get; set; }
        [Display(Name = "ReceiptNo")]
        public string BidBondOff_ReceiptNo { get; set; }
        [Display(Name = "IsCredit")]
        public bool BidBondOn_IsCredit { get; set; }
        [Display(Name = "NoOfPeriods")]
        //public int? BidBondOn_NoOfPeriods
        //{
        //    get
        //    {
        //        if (BidBondOn_ExpiryDate != null && BidBondOn_IssueDate != null)
        //            return MonthDifference(BidBondOn_ExpiryDate.Value, BidBondOn_IssueDate.Value) / 3;
        //        else
        //            return 1;
        //    }
        //    set
        //    {

        //    }
        //}
        public int? BidBondOn_NoOfPeriods { get; set; }
        [Display(Name = "BankId")]
        public int? BidBondOn_BankId { get; set; }
        public string BidBondOn_BankName { get; set; }
        public string BidBondOn_BankNameAr { get; set; }
        public decimal? BidBondOn_HoldAmount { get; set; }
        [Display(Name = "BankBranch")]
        public string BidBondOn_BankBranch { get; set; }
        public decimal? BidBondOn_Percentage { get; set; }
        public decimal? BidBondOn_Fees { get; set; }
        [Display(Name = "BidBondNo")]
        public string BidBondOn_BidBondNo { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "ExpiryDate")]
        public DateTime? BidBondOn_ExpiryDate { get; set; }
        //public int? TodoTaskId { get; set; }

        public BidBondRequestViewModel RequestVM { get; set; }
        public bool IsRequested { get; set; }
        public bool IsSent { get; set; }
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
        public bool IsPrinted { get; set; }

        public static int MonthDifference(DateTime lValue, DateTime rValue)
        {
            return (lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year);
        }
    }

    public class BidBondRequestViewModel
    {
        public int? Id { get; set; }
        public int BidBondId { get; set; }
        public BidBondViewModel BidBondVM { get; set; }
        public string RequestedById { get; set; }
        public string RequestedByName { get; set; }
        [DataType(DataType.Date)]
        public DateTime RequestedDate { get; set; }
        public bool IsSent { get; set; }
        public string SentById { get; set; }
        public string SentByName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? SentDate { get; set; }
        public RequestStatusEnum Status { get; set; }
        public string StatusById { get; set; }
        public string StatusByName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? StatusDate { get; set; }
        public bool IsPrinted { get; set; }
        public string PrintedById { get; set; }
        public string PrintedByName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? PrintedDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNameAr { get; set; }
    }
}
