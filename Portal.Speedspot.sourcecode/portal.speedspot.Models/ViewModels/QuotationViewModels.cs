using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static portal.speedspot.Models.Constants.ApplicationEnums;
using static portal.speedspot.Models.Constants.Permissions;

namespace portal.speedspot.Models.ViewModels
{
    public class QuotationViewModel
    {
        public int Id { get; set; }

        public int OpportunityId { get; set; }
        public string OpportunityName { get; set; }

        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string StatusNameAr { get; set; }

        public DateTime DueDate { get; set; }

        [Display(Name = nameof(AdminId))]
        public string AdminId { get; set; }
        public string AdminName { get; set; }

        public string CreatedById { get; set; }
        public string CreatedByName { get; set; }

        public DateTime CreatedDate { get; set; }

        public QuotationTechnicalSessionStatus? TechnicalSessionStatus { get; set; }
        public DateTime? FinancialSessionDate { get; set; }

        public QuotationFinancialSessionStatus? FinancialSessionStatus { get; set; }

        public string RejectLetterPath { get; set; }
        public DateTime? RejectLetterDate { get; set; }
        public int? RejectReasonId { get; set; }
        public string RejectReasonName { get; set; }
        public string RejectReasonNameAr { get; set; }

        public string AcceptanceLetterPath { get; set; }
        public DateTime? AcceptanceLetterDate { get; set; }

        [Display(Name = nameof(AwardingLetter))]
        [Required(ErrorMessage = "RequiredField")]
        public IFormFile AwardingLetter { get; set; }
        public string AwardingLetterPath { get; set; }

        public int LGType { get; set; }

        [Display(Name = nameof(AwardingLetterDate))]
        [Required(ErrorMessage = "RequiredField")]
        public DateTime? AwardingLetterDate { get; set; }

        public bool IsArchived { get; set; }

        public bool IsFavourite { get; set; }

        public OpportunityViewModel OpportunityVM { get; set; }

        public IList<QuotationCurrencyViewModel> CurrencyVMs { get; set; }

        public IList<QuotationOfferViewModel> OfferVMs { get; set; }

        public IList<CompetitorOfferViewModel> CompetitorOfferVMs { get; set; }

        public IList<QuotationCompetitorViewModel> CompetitorVMs { get; set; }

        public IList<CustomerConversationViewModel> CustomerConversationVMs { get; set; }

        public IList<SupplierConversationViewModel> SupplierConversationVMs { get; set; }
        public IList<SupplierNegotiationViewModel> SupplierNegtiationVMs { get; set; }
        public IList<CustomerNegotiationViewModel> CustomerNegtiationVMs { get; set; }

        public NegotiationResultViewModel NegotiationResultsVM { get; set; }
        public IList<OtherNegotiationResultViewModel> OtherNegotiationResultVMs { get; set; }

        public LetterOfGuaranteeViewModel PerformanceLGVM { get; set; }
        public LetterOfGuaranteeViewModel FinalLGVM { get; set; }


        public int OffersNo
        {
            get
            {
                return OfferVMs.Count;
            }
        }

        public bool HasRemainingOffers
        {
            get
            {
                return OffersNo < OpportunityVM.SupplierOffersNo;
            }
        }

        public QuotationViewModel()
        {
            if (CurrencyVMs == null) CurrencyVMs = new List<QuotationCurrencyViewModel>();
        }
    }

    public class FinancialSessionAcceptanceModel
    {
        [Display(Name = nameof(AwardingLetter))]
        [Required(ErrorMessage = "RequiredField")]
        public IFormFile AwardingLetter { get; set; }

        [Display(Name = nameof(AwardingLetterDate))]
        [Required(ErrorMessage = "RequiredField")]
        public DateTime AwardingLetterDate { get; set; }

        public int LGType { get; set; }
        public LetterOfGuaranteeViewModel PerformanceLGVM { get; set; }
        public LetterOfGuaranteeViewModel FinalLGVM { get; set; }
    }

    public class LetterOfGuaranteeViewModel
    {
        public int Id { get; set; }
        public int QuotationId { get; set; }

        [Display(Name = nameof(CashType))]
        public bool CashType { get; set; }

        [Display(Name = nameof(IsCredit))]
        public bool IsCredit { get; set; }

        [Display(Name = nameof(NoOfPeriods))]
        public int? NoOfPeriods { get; set; }

        [Display(Name = nameof(BankId))]
        public int? BankId { get; set; }
        public string BankName { get; set; }
        public string BankNameAr { get; set; }

        [Display(Name = nameof(BankBranchId))]
        public int? BankBranchId { get; set; }
        public string BankBranchName { get; set; }
        public string BankBranchNameAr { get; set; }

        [Display(Name = nameof(AssignedToId))]
        public string AssignedToId { get; set; }
        public string AssignedToName { get; set; }

        [Display(Name = nameof(Amount))]
        public decimal? Amount { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = nameof(IssueDate))]
        public DateTime? IssueDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = nameof(ExpiryDate))]
        public DateTime? ExpiryDate { get; set; }

        [Display(Name = nameof(Description))]
        public string Description { get; set; }

        [Display(Name = nameof(LetterOfGuaranteeNo))]
        public string LetterOfGuaranteeNo { get; set; }

        [Display(Name = nameof(AttachmentFile))]
        public IFormFile AttachmentFile { get; set; }
        public string AttachmentPath { get; set; }
        public string AttachmentTitle { get; set; }

        public decimal? Percentage { get; set; }
        public decimal? Fees { get; set; }
        public decimal? PaidAmount { get; set; }

        public LetterOfGuaranteeRequestViewModel RequestVM { get; set; }

        public bool IsRequested { get; set; }
        public bool IsSent { get; set; }
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
        public bool IsPrinted { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = nameof(ReceivingDate))]
        public DateTime? ReceivingDate { get; set; }

        [Display(Name = nameof(EmployeeName))]
        public string EmployeeName { get; set; }

        [Display(Name = nameof(AttachmentFile))]
        public IFormFile ReceiptAttachmentFile { get; set; }
        public string ReceiptAttachmentPath { get; set; }
        public string ReceiptAttachmentTitle { get; set; }

        public AdvancePaymentType? AdvanceType { get; set; }

        public int? IssueBankId { get; set; }
        public string IssueBankName { get; set; }
        public string IssueBankNameAr { get; set; }

        public decimal? AdvanceAmount { get; set; }
        public string AdvanceNo { get; set; }
        public DateTime? AdvanceDate { get; set; }

        public int? ReceiveBankId { get; set; }
        public string ReceiveBankName { get; set; }
        public string ReceiveBankNameAr { get; set; }

        public string AdvanceAttachmentTitle { get; set; }
        public string AdvanceAttachmentPath { get; set; }
        public IFormFile AdvanceAttachmentFile { get; set; }
    }

    public class LetterOfGuaranteeRequestViewModel
    {
        public int? Id { get; set; }
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

        public LetterOfGuaranteeViewModel LGVM { get; set; }
    }

    public class QuotationCurrencyViewModel
    {
        public int? Id { get; set; }
        public int QuotationId { get; set; }
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyNameAr { get; set; }
        public string CurrencySymbol { get; set; }
        public decimal ExchangeRate { get; set; }
    }

    public class QuotationOfferViewModel
    {
        public int Id { get; set; }

        public int QuotationId { get; set; }

        public bool IsTwoEnvelopes { get; set; }

        public string Code { get; set; }

        public IList<QuotationOfferVersionViewModel> VersionVMs { get; set; }
    }

    public class QuotationOfferVersionViewModel
    {
        public int Id { get; set; }

        public int OfferId { get; set; }

        public QuotationOfferViewModel OfferVM { get; set; }

        [Display(Name = nameof(Title))]
        [Required(ErrorMessage = "RequiredField")]
        public string Title { get; set; }

        [Display(Name = nameof(AttentionTo))]
        [Required(ErrorMessage = "RequiredField")]
        public string AttentionTo { get; set; }

        [Display(Name = nameof(Phone))]
        [Phone]
        [Required(ErrorMessage = "RequiredField")]
        public string Phone { get; set; }

        [Display(Name = nameof(Email))]
        [EmailAddress]
        [Required(ErrorMessage = "RequiredField")]
        public string Email { get; set; }

        [Display(Name = nameof(AttentionTo2))]
        public string AttentionTo2 { get; set; }

        [Display(Name = nameof(Phone2))]
        [Phone]
        public string Phone2 { get; set; }

        [Display(Name = nameof(Email2))]
        [EmailAddress]
        public string Email2 { get; set; }
        [Display(Name = nameof(CoverLetter))]
        [Required(ErrorMessage = "RequiredField")]
        public string CoverLetter { get; set; }
        [Display(Name = nameof(TechnicalSpecifications))]
        [Required(ErrorMessage = "RequiredField")]
        public string TechnicalSpecifications { get; set; }
        [Display(Name = nameof(CurrencyId))]
        [Required(ErrorMessage = "RequiredField")]
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyNameAr { get; set; }
        public string CurrencySymbol { get; set; }

        [Display(Name = nameof(ValidityId))]
        [Required(ErrorMessage = "RequiredField")]
        public int ValidityId { get; set; }
        public string ValidityName { get; set; }
        public string ValidityNameAr { get; set; }

        [Display(Name = "DeliveryRangeFrom")]
        [Required(ErrorMessage = "RequiredField")]
        public int DeliveryFromDays { get; set; }

        [Display(Name = "DeliveryRangeTo")]
        [Required(ErrorMessage = "RequiredField")]
        public int DeliveryToDays { get; set; }

        [Display(Name = nameof(DeliveryNotes))]
        public string DeliveryNotes { get; set; }

        [Display(Name = nameof(DeliveryPlaceId))]
        [Required(ErrorMessage = "RequiredField")]
        public int DeliveryPlaceId { get; set; }
        public string DeliveryPlaceName { get; set; }
        public string DeliveryPlaceNameAr { get; set; }

        [Display(Name = nameof(OriginDocumentId))]
        public int? OriginDocumentId { get; set; }
        public string OriginDocumentName { get; set; }
        public string OriginDocumentNameAr { get; set; }
        public string SupplierName { get; set; }
        public string SupplierNameAr { get; set; }

        public IList<QuotationOfferCertificateViewModel> CertificateVMs { get; set; }

        [Display(Name = nameof(CertificateIds))]
        [Required(ErrorMessage = "RequiredField")]
        public IList<int> CertificateIds { get; set; }

        public IList<string> CertificateNames
        {
            get
            {
                return CertificateVMs?.Select(c => c.CertificateName).ToList();
            }
        }

        public IList<string> CertificateNamesAr
        {
            get
            {
                return CertificateVMs?.Select(c => c.CertificateNameAr).ToList();
            }
        }

        public string CertificateNamesStr
        {
            get
            {
                return CertificateVMs != null ? string.Join(", ", CertificateVMs.Select(d => d.CertificateName).ToList()) : "";
            }
        }

        public string CertificateNamesStrAr
        {
            get
            {
                return CertificateVMs != null ? string.Join(", ", CertificateVMs.Select(d => d.CertificateNameAr).ToList()) : "";
            }
        }

        [Display(Name = nameof(DeliveryGuaranteeMonths))]
        public int? DeliveryGuaranteeMonths { get; set; }

        [Display(Name = nameof(CommissionGuaranteeMonths))]
        public int? CommissionGuaranteeMonths { get; set; }

        [Display(Name = nameof(GuaranteeTermId))]
        public int? GuaranteeTermId { get; set; }
        public string GuaranteeTermName { get; set; }
        public string GuaranteeTermNameAr { get; set; }

        [Display(Name = nameof(DownPaymentPercentage))]
        [Required(ErrorMessage = "RequiredField")]
        public decimal DownPaymentPercentage { get; set; }

        [Display(Name = nameof(UponDeliveryPercentage))]
        [Required(ErrorMessage = "RequiredField")]
        public decimal UponDeliveryPercentage { get; set; }

        [Display(Name = nameof(AfterInstallationPercentage))]
        [Required(ErrorMessage = "RequiredField")]
        public decimal AfterInstallationPercentage { get; set; }

        [Display(Name = nameof(Notes))]
        public string Notes { get; set; }
        [Display(Name = nameof(TechnicalNotes))]
        public string TechnicalNotes { get; set; }

        [Display(Name = nameof(Factor))]
        [Required(ErrorMessage = "RequiredField")]
        public decimal Factor { get; set; }
        public bool IsSelected { get; set; }

        [Display(Name = "RevisionNo")]
        [Required(ErrorMessage = "RequiredField")]
        public int Version { get; set; }

        [Display(Name = nameof(VAT))]
        [Required(ErrorMessage = "RequiredField")]
        public decimal VAT
        {
            get
            {
                if (ProductVMs != null && ProductVMs.Count > 0) return ProductVMs[0].TaxPercentage;
                else return 0;
            }
        }
        public string CreatedById { get; set; }
        [Display(Name = nameof(CreatedById))]
        public string CreatedByName { get; set; }
        [Display(Name = nameof(CreatedDate))]
        public DateTime CreatedDate { get; set; }
        public IList<QuotationOfferProductViewModel> ProductVMs { get; set; }
        public decimal SubTotal
        {
            get
            {
                decimal price = 0;
                foreach (var product in ProductVMs)
                {
                    price += product.SubTotal;
                }
                return price;
            }
        }
        public decimal TotalVAT
        {
            get
            {
                return TotalPrice - SubTotal;
            }
        }
        public decimal TotalPrice
        {
            get
            {
                decimal price = 0;
                foreach (var product in ProductVMs)
                {
                    price += product.Total;
                }
                return price;
            }
        }
        public decimal TotalSupplierPrice
        {
            get
            {
                decimal price = 0;
                foreach (var product in ProductVMs)
                {
                    price += (product.SupplierPrice ?? 0) * product.Quantity;
                }
                return price;
            }
        }
        public decimal LocalSupplierPrice
        {
            get
            {
                decimal price = 0;
                foreach (var product in ProductVMs)
                {
                    price += (product.SupplierPrice ?? 0) * (product.SupplierRate ?? 0) * product.Quantity;
                }
                return price;
            }
        }
        public decimal AverageFactor
        {
            get
            {
                return TotalSupplierPrice > 0 ? TotalPrice / TotalSupplierPrice : 1;
            }
        }

        public IList<AlternateVersionViewModel> AlternateVMs { get; set; }
    }

    public class AlternateVersionViewModel
    {
        public int Id { get; set; }

        public int QuotationOfferVersionId { get; set; }

        [Display(Name = nameof(TechnicalSpecifications))]
        [Required(ErrorMessage = "RequiredField")]
        public string TechnicalSpecifications { get; set; }

        [Display(Name = nameof(CurrencyId))]
        [Required(ErrorMessage = "RequiredField")]
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyNameAr { get; set; }
        public string CurrencySymbol { get; set; }


        [Display(Name = nameof(ValidityId))]
        [Required(ErrorMessage = "RequiredField")]
        public int ValidityId { get; set; }
        public string ValidityName { get; set; }
        public string ValidityNameAr { get; set; }


        [Display(Name = "DeliveryRangeFrom")]
        [Required(ErrorMessage = "RequiredField")]
        public int DeliveryFromDays { get; set; }

        [Display(Name = "DeliveryRangeTo")]
        [Required(ErrorMessage = "RequiredField")]
        public int DeliveryToDays { get; set; }

        [Display(Name = nameof(DeliveryNotes))]
        public string DeliveryNotes { get; set; }


        [Display(Name = nameof(DeliveryPlaceId))]
        [Required(ErrorMessage = "RequiredField")]
        public int DeliveryPlaceId { get; set; }
        public string DeliveryPlaceName { get; set; }
        public string DeliveryPlaceNameAr { get; set; }


        [Display(Name = nameof(OriginDocumentId))]
        public int? OriginDocumentId { get; set; }
        public string OriginDocumentName { get; set; }
        public string OriginDocumentNameAr { get; set; }

        public IList<AlternateVersionCertificateViewModel> CertificateVMs { get; set; }

        [Display(Name = nameof(CertificateIds))]
        [Required(ErrorMessage = "RequiredField")]
        public IList<int> CertificateIds { get; set; }

        public IList<string> CertificateNames
        {
            get
            {
                return CertificateVMs?.Select(c => c.CertificateName).ToList();
            }
        }

        public IList<string> CertificateNamesAr
        {
            get
            {
                return CertificateVMs?.Select(c => c.CertificateNameAr).ToList();
            }
        }

        public string CertificateNamesStr
        {
            get
            {
                return CertificateVMs != null ? string.Join(", ", CertificateVMs.Select(d => d.CertificateName).ToList()) : "";
            }
        }

        public string CertificateNamesStrAr
        {
            get
            {
                return CertificateVMs != null ? string.Join(", ", CertificateVMs.Select(d => d.CertificateNameAr).ToList()) : "";
            }
        }


        [Display(Name = nameof(DeliveryGuaranteeMonths))]
        public int? DeliveryGuaranteeMonths { get; set; }

        [Display(Name = nameof(CommissionGuaranteeMonths))]
        public int? CommissionGuaranteeMonths { get; set; }


        [Display(Name = nameof(GuaranteeTermId))]
        public int? GuaranteeTermId { get; set; }
        public string GuaranteeTermName { get; set; }
        public string GuaranteeTermNameAr { get; set; }


        [Display(Name = nameof(DownPaymentPercentage))]
        [Required(ErrorMessage = "RequiredField")]
        public decimal DownPaymentPercentage { get; set; }

        [Display(Name = nameof(UponDeliveryPercentage))]
        [Required(ErrorMessage = "RequiredField")]
        public decimal UponDeliveryPercentage { get; set; }

        [Display(Name = nameof(AfterInstallationPercentage))]
        [Required(ErrorMessage = "RequiredField")]
        public decimal AfterInstallationPercentage { get; set; }


        [Display(Name = nameof(Factor))]
        [Required(ErrorMessage = "RequiredField")]
        public decimal Factor { get; set; }

        [Display(Name = nameof(VAT))]
        [Required(ErrorMessage = "RequiredField")]
        public decimal VAT
        {
            get
            {
                if (ProductVMs != null && ProductVMs.Count > 0) return ProductVMs[0].TaxPercentage;
                else return 0;
            }
        }


        public IList<AlternateVersionProductViewModel> ProductVMs { get; set; }


        public decimal SubTotal
        {
            get
            {
                decimal price = 0;
                foreach (var product in ProductVMs)
                {
                    price += product.SubTotal;
                }
                return price;
            }
        }

        public decimal TotalVAT
        {
            get
            {
                return TotalPrice - SubTotal;
            }
        }

        public decimal TotalPrice
        {
            get
            {
                decimal price = 0;
                foreach (var product in ProductVMs)
                {
                    price += product.Total;
                }
                return price;
            }
        }

        public decimal TotalSupplierPrice
        {
            get
            {
                decimal price = 0;
                foreach (var product in ProductVMs)
                {
                    price += (product.SupplierPrice ?? 0) * product.Quantity;
                }
                return price;
            }
        }

        public decimal LocalSupplierPrice
        {
            get
            {
                decimal price = 0;
                foreach (var product in ProductVMs)
                {
                    price += (product.SupplierPrice ?? 0) * (product.SupplierRate ?? 0) * product.Quantity;
                }
                return price;
            }
        }

        public int? SerialNo { get; set; }
    }

    public class QuotationOfferProductInfoViewModel
    {
        public int ProductId { get; set; }
        public TechnicalSpecificationProductViewModel ProductVM { get; set; }
        public IList<SupplierOfferProductViewModel> SupplierOfferProductVMs { get; set; }
    }

    public class QuotationOfferProductViewModel
    {
        public int Id { get; set; }
        public int OfferId { get; set; }
        public int ProductId { get; set; }
        public SupplierOfferProductViewModel ProductVM { get; set; }
        public string ProductName { get; set; }
        public string ProductNameAr { get; set; }
        public bool IsIncluded { get; set; }
        public IList<QuotationOfferProductOriginViewModel> SelectedOriginVMs { get; set; }
        public string SelectedOriginStr
        {
            get
            {
                return SelectedOriginVMs != null ? string.Join(", ", SelectedOriginVMs.Select(d => d.CountryName).ToList()) : "";
            }
        }
        public string SelectedOriginStrAr
        {
            get
            {
                return SelectedOriginVMs != null ? string.Join(", ", SelectedOriginVMs.Select(d => d.CountryNameAr).ToList()) : "";
            }
        }
        public List<int> OriginIds { get; set; }

        [Display(Name = nameof(OfferDescription))]
        public string OfferDescription { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal Quantity
        {
            get
            {
                return ProductVM != null ? ProductVM.ProductVM.Quantity ?? 0 : 0;
            }
        }
        public decimal SubTotal
        {
            get
            {
                return UnitPrice * Quantity;
            }
        }
        public decimal TaxPercentage { get; set; }
        public decimal Total
        {
            get
            {
                return SubTotal + (SubTotal * TaxPercentage / 100);
            }
        }
        public decimal? SupplierPrice { get; set; }
        public decimal? SupplierRate { get; set; }
    }

    public class QuotationOfferProductOriginViewModel
    {
        public int ProductId { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryNameAr { get; set; }
    }

    public class AlternateVersionCertificateViewModel
    {
        public int AlternateVersionId { get; set; }
        public int CertificateId { get; set; }
        public string CertificateName { get; set; }
        public string CertificateNameAr { get; set; }
    }

    public class AlternateVersionProductViewModel
    {
        public int Id { get; set; }
        public int AlternateVersionId { get; set; }
        public int ProductId { get; set; }
        public SupplierOfferProductViewModel ProductVM { get; set; }
        public string ProductName { get; set; }
        public string ProductNameAr { get; set; }
        public bool IsIncluded { get; set; }
        public IList<AlternateVersionProductOriginViewModel> SelectedOriginVMs { get; set; }
        public string SelectedOriginStr
        {
            get
            {
                return SelectedOriginVMs != null ? string.Join(", ", SelectedOriginVMs.Select(d => d.CountryName).ToList()) : "";
            }
        }
        public string SelectedOriginStrAr
        {
            get
            {
                return SelectedOriginVMs != null ? string.Join(", ", SelectedOriginVMs.Select(d => d.CountryNameAr).ToList()) : "";
            }
        }
        public List<int> OriginIds { get; set; }

        [Display(Name = nameof(OfferDescription))]
        public string OfferDescription { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal Quantity
        {
            get
            {
                return ProductVM != null ? ProductVM.ProductVM.Quantity ?? 0 : 0;
            }
        }
        public decimal SubTotal
        {
            get
            {
                return UnitPrice * Quantity;
            }
        }
        public decimal TaxPercentage { get; set; }
        public decimal Total
        {
            get
            {
                return SubTotal + (SubTotal * TaxPercentage / 100);
            }
        }
        public decimal? SupplierPrice { get; set; }
        public decimal? SupplierRate { get; set; }
    }

    public class AlternateVersionProductOriginViewModel
    {
        public int ProductId { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryNameAr { get; set; }
    }

    public class QuotationOfferCertificateViewModel
    {
        public int OfferId { get; set; }
        public int CertificateId { get; set; }
        public string CertificateName { get; set; }
        public string CertificateNameAr { get; set; }
    }

    public class QuotationCompetitorViewModel
    {
        public int Id { get; set; }
        public int QuotationId { get; set; }
        public int CompetitorId { get; set; }
        public string CompetitorName { get; set; }
        public string CompetitorNameAr { get; set; }
        public string CompetitorImagePath { get; set; }
        public bool IsTechnicalAccepted { get; set; }
    }

    public class QuotationGroupViewModel
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal SubTotal { get; set; }
        public bool IsCompetitor { get; set; }

        public IList<QuotationOfferViewModel> OfferVMs { get; set; }
        public IList<CompetitorOfferViewModel> CompetitorOfferVMs { get; set; }
    }

    public class QuotationFinancialDetailsViewModel
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal SubTotal { get; set; }
        public bool IsCompetitor { get; set; }

        public QuotationOfferVersionViewModel OfferVersionVM { get; set; }
        public IList<CompetitorOfferViewModel> CompetitorOfferVMs { get; set; }
    }

    public class NegotiationResultsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal OldOfferValue { get; set; }
        public decimal OldDownPaymentPercentage { get; set; }
        public decimal OldUponDeliveryPercentage { get; set; }
        public decimal OldAfterInstallationPercentage { get; set; }
        public int OldDeliveryFromDays { get; set; }
        public int OldDeliveryToDays { get; set; }
        public NegotiationResultViewModel NegotiationResultsVM { get; set; }
        public IList<OtherNegotiationResultViewModel> OtherNegotiationResultVMs { get; set; }
    }

    public class NegotiationResultViewModel
    {
        public int QuotationId { get; set; }

        [Display(Name = "Subtotal")]
        [Required(ErrorMessage = "RequiredField")]
        public decimal OfferValue { get; set; }

        [Display(Name = nameof(DownPaymentPercentage))]
        [Required(ErrorMessage = "RequiredField")]
        public decimal DownPaymentPercentage { get; set; }
        [Display(Name = nameof(UponDeliveryPercentage))]
        [Required(ErrorMessage = "RequiredField")]
        public decimal UponDeliveryPercentage { get; set; }
        [Display(Name = nameof(AfterInstallationPercentage))]
        [Required(ErrorMessage = "RequiredField")]
        public decimal AfterInstallationPercentage { get; set; }

        [Display(Name = "DeliveryRangeFrom")]
        [Required(ErrorMessage = "RequiredField")]
        public int DeliveryFromDays { get; set; }
        [Display(Name = "DeliveryRangeTo")]
        [Required(ErrorMessage = "RequiredField")]
        public int DeliveryToDays { get; set; }
    }

    public class OtherNegotiationResultViewModel
    {
        public int QuotationId { get; set; }

        [Display(Name = "NegotiationTitle")]
        [Required(ErrorMessage = "RequiredField")]
        public string Title { get; set; }

        [Display(Name = nameof(ValueBefore))]
        [Required(ErrorMessage = "RequiredField")]
        public string ValueBefore { get; set; }

        [Display(Name = nameof(ValueAfter))]
        [Required(ErrorMessage = "RequiredField")]
        public string ValueAfter { get; set; }
    }

    public class QuotationComparisonViewModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string ImagePath { get; set; }
        public QuotationComparisonOfferViewModel ComparisonOfferVM { get; set; }
        public IList<QuotationComparisonProductViewModel> ComparisonProductVMs { get; set; }

        public decimal TotalPrice
        {
            get
            {
                if (ComparisonProductVMs != null)
                {
                    return ComparisonProductVMs.Sum(p => p.TotalPrice);
                }
                else
                {
                    return 0;
                }
            }
        }
    }

    public class QuotationComparisonOfferViewModel
    {
        public string CurrencyName { get; set; }
        public decimal ExchangeRate { get; set; }
        public string ValidityName { get; set; }
        public string GuaranteeName { get; set; }
        public int DeliveryFromDays { get; set; }
        public int DeliveryToDays { get; set; }
        public string DeliveryPlaceName { get; set; }
        public bool IsVATIncluded { get; set; }
        public decimal DownPaymentPercentage { get; set; }
        public decimal UponDeliveryPercentage { get; set; }
        public decimal AfterInstallationPercentage { get; set; }
        public string OriginDocumentName { get; set; }
        public string CertificateNames { get; set; }
        public string SupplierName { get; set; }
        public string Notes { get; set; }
    }

    public class QuotationComparisonProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
