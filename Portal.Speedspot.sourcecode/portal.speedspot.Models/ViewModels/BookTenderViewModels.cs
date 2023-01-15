using Microsoft.AspNetCore.Http;
using portal.speedspot.Models.Helpers;
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
    public class BookTenderViewModel
    {
        public int Id { get; set; }
        public int OpportunityId { get; set; }
        [Display(Name = "AssignedToId")]
        public string AssignedToId { get; set; }
        public string AssignedToName { get; set; }
        [Display(Name = "BookTenderFees")]
        public decimal? Fees { get; set; }
        public decimal? VAT { get; set; }
        [Display(Name = "Total")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? TotalFees
        {
            get
            {
                if (Fees.HasValue && VAT.HasValue)
                    return Math.Round((Fees.Value * VAT.Value / 100) + Fees.Value, 2);
                else
                {
                    return null;
                }
            }
            set
            {

            }
        }
        [Display(Name = "ReceiptNo")]
        public string ReceiptNo { get; set; }
        [Display(Name = "ReceiptDate")]
        [DataType(DataType.Date)]
        public DateTime? ReceiptDate { get; set; }
        public int? TodoTaskId { get; set; }
        [Display(Name = "ReceiptAttachmentId")]
        public int? ReceiptAttachmentId { get; set; }
        public string ReceiptAttachmentPath { get; set; }
        public string ReceiptAttachmentTitle { get; set; }
        public string ReceiptAttachmentUploadedByName { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = nameof(ReceiptAttachmentFile))]
        [RequiredIf(nameof(ReceiptNo), ErrorMessage = "RequiredField")]
        public IFormFile ReceiptAttachmentFile { get; set; }

        public BookTenderRequestViewModel RequestVM { get; set; }
        public bool IsRequested { get; set; }
        public bool IsSent { get; set; }
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
        public bool IsPrinted { get; set; }
    }

    public class BookTenderRequestViewModel
    {
        public int? Id { get; set; }
        public int BookTenderId { get; set; }
        public BookTenderViewModel BookTenderVM { get; set; }
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
        public string Address { get; set; }
        public string TenderDescription { get; set; }
    }
}
