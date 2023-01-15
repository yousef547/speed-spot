using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class CompetitorOfferViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "RequiredField")]
        public int CompetitorId { get; set; }
        public string CompetitorName { get; set; }
        public string CompetitorNameAr { get; set; }
        public string CompetitorImagePath { get; set; }

        public int QuotationId { get; set; }

        [Display(Name = nameof(DownPaymentPercentage))]
        [Required(ErrorMessage = "RequiredField")]
        public decimal DownPaymentPercentage { get; set; }

        [Display(Name = nameof(UponDeliveryPercentage))]
        [Required(ErrorMessage = "RequiredField")]
        public decimal UponDeliveryPercentage { get; set; }

        [Display(Name = nameof(AfterInstallationPercentage))]
        [Required(ErrorMessage = "RequiredField")]
        public decimal AfterInstallationPercentage { get; set; }

        [Display(Name = nameof(ValidityId))]
        [Required(ErrorMessage = "RequiredField")]
        public int ValidityId { get; set; }
        public string ValidityName { get; set; }
        public string ValidityNameAr { get; set; }

        [Display(Name = nameof(OriginDocumentId))]
        public int? OriginDocumentId { get; set; }
        public string OriginDocumentName { get; set; }
        public string OriginDocumentNameAr { get; set; }

        [Display(Name = nameof(GuaranteeTermId))]
        [Required(ErrorMessage = "RequiredField")]
        public int GuaranteeTermId { get; set; }
        public string GuaranteeTermName { get; set; }
        public string GuaranteeTermNameAr { get; set; }

        [Display(Name = nameof(DeliveryPlaceId))]
        [Required(ErrorMessage = "RequiredField")]
        public int DeliveryPlaceId { get; set; }
        public string DeliveryPlaceName { get; set; }
        public string DeliveryPlaceNameAr { get; set; }

        [Display(Name = "DeliveryRangeFrom")]
        [Required(ErrorMessage = "RequiredField")]
        public int DeliveryFromDays { get; set; }

        [Display(Name = "DeliveryRangeTo")]
        [Required(ErrorMessage = "RequiredField")]
        public int DeliveryToDays { get; set; }

        [Display(Name = nameof(CurrencyId))]
        [Required(ErrorMessage = "RequiredField")]
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyNameAr { get; set; }
        public string CurrencySymbol { get; set; }

        [Display(Name = nameof(ExchangeRate))]
        [Required(ErrorMessage = "RequiredField")]
        public decimal ExchangeRate { get; set; }

        [Display(Name = nameof(CurrentRate))]
        [Required(ErrorMessage = "RequiredField")]
        public decimal CurrentRate { get; set; }

        [Display(Name = nameof(IsVATIncluded))]
        public bool IsVATIncluded { get; set; }

        [Display(Name = nameof(Notes))]
        public string Notes { get; set; }

        [Display(Name = nameof(Supplier))]
        public string Supplier { get; set; }
        public bool IsTwoEnvelopes { get; set; }

        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }

        public IList<CompetitorOfferCertificateViewModel> CertificateVMs { get; set; }

        public string CertificateNamesStr
        {
            get
            {
                return CertificateVMs != null ? string.Join(", ", CertificateVMs.Select(x => x.CertificateName)) : "";
            }
        }

        public string CertificateNamesStrAr
        {
            get
            {
                return CertificateVMs != null ? string.Join(", ", CertificateVMs.Select(x => x.CertificateNameAr)) : "";
            }
        }

        [Display(Name = nameof(CertificateIds))]
        [Required(ErrorMessage = "RequiredField")]
        public IList<int> CertificateIds { get; set; }

        public IList<CompetitorOfferProductViewModel> ProductVMs { get; set; }

        public decimal TotalPrice
        {
            get
            {
                return ProductVMs != null ? ProductVMs.Sum(x => x.TotalPrice) : 0;
            }
        }
    }

    public class CompetitorOfferCertificateViewModel
    {
        public int Id { get; set; }
        public int OfferId { get; set; }
        public int CertificateId { get; set; }
        public string CertificateName { get; set; }
        public string CertificateNameAr { get; set; }
    }

    public class CompetitorOfferProductViewModel
    {
        public int Id { get; set; }
        public int OfferId { get; set; }
        public int ProductId { get; set; }
        public TechnicalSpecificationProductViewModel ProductVM { get; set; }

        [Display(Name = nameof(UnitPrice))]
        public decimal? UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public bool IsIncluded { get; set; }

        public IList<CompetitorOfferProductOriginViewModel> OriginVMs { get; set; }

        [Display(Name = "SelectedOrigins")]
        public List<int> OriginIds { get; set; }

        [Display(Name = "Total")]
        public decimal TotalPrice
        {
            get
            {
                return UnitPrice != null ? (decimal)UnitPrice * Quantity : 0;
            }
        }
    }

    public class CompetitorOfferProductOriginViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryNameAr { get; set; }
    }
}
