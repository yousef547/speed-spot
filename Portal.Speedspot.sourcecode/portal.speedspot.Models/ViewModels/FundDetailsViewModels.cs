using Microsoft.AspNetCore.Http;
using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class FundDetailsViewModel
    {
        public int? Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public int? BankId { get; set; }
        public int? CurrencyId { get; set; }
        public decimal? Amount { get; set; }
        public int? FundPeriod { get; set; }
        [ Range(0, 100, ErrorMessage = "FundPeriod Must be between 0 to 100")]
        public decimal BankInterest { get; set; }
        public string ProjectNo { get; set; }
        public int? ContractFileId { get; set; }
        public string ContractFileTitle { get; set; }
        public string ContractFilePath { get; set; }
        public IFormFile ContractFormFile { get; set; }
        public int? CollectionFileId { get; set; }
        public string CollectionFileTitle { get; set; }
        public string CollectionFilePath { get; set; }
        public IFormFile CollectionFormFile { get; set; }
        public IList<FundDetailsPaymentViewModel> AllSupplierPayments { get; set; }
        public string BankName { get; set; }
        public string BankNameAr { get; set; }
        public string CountryName { get; set; }
        public string CountryNameAr { get; set; }

        public decimal? TotalInterest
        {
            get
            {
                return ((Amount * BankInterest) / 100) * FundPeriod;
            }
        }
    }

    public class FundDetailsPaymentViewModel
    {
        public int Id { get; set; }
        public int FundDetailsId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierNameAr { get; set; }
        public string SupplierPONumber { get; set; }
        public int? PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }
        public string PaymentTypeNameAr { get; set; }
        public decimal Amount { get; set; }
        public int? SupplierPoId { get; set; }

    }
}
