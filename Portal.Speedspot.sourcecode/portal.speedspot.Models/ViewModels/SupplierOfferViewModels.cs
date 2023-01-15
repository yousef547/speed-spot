using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class SupplierOfferViewModel
    {
        public int? Id { get; set; }
        public int OpportunityId { get; set; }

        //[Required(ErrorMessage = "RequiredField")]
        //[Display(Name = nameof(SupplierId))]
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierNameAr { get; set; }

        public string SupplierLogoAttachmentPath { get; set; }

        //[Required(ErrorMessage = "RequiredField")]
        //[Display(Name = nameof(CurrencyId))]
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyNameAr { get; set; }
        public string CurrencySymbol { get; set; }

        //[Required(ErrorMessage = "RequiredField")]
        //[Display(Name = nameof(ExchangeRate))]
        public decimal ExchangeRate { get; set; }

        //[Required(ErrorMessage = "RequiredField")]
        //[Display(Name = nameof(PaymentTypeId))]
        public int PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }
        public string PaymentTypeNameAr { get; set; }

        //[Required(ErrorMessage = "RequiredField")]
        //[Display(Name = nameof(DeliveryTypeId))]
        public int DeliveryTypeId { get; set; }
        public string DeliveryTypeName { get; set; }
        public string DeliveryTypeNameAr { get; set; }
        public decimal CIFCost { get; set; }

        //[Required(ErrorMessage = "RequiredField")]
        //[Display(Name = nameof(DeliveryDate))]
        public DateTime? DeliveryDate { get; set; } = DateTime.Now;

        //[Required(ErrorMessage = "RequiredField")]
        //[Display(Name = nameof(DeliveryRangeFrom))]
        public int? DeliveryRangeFrom { get; set; }

        //[Required(ErrorMessage = "RequiredField")]
        //[Display(Name = nameof(DeliveryRangeTo))]
        public int? DeliveryRangeTo { get; set; }

        //[Required(ErrorMessage = "RequiredField")]
        //[Display(Name = nameof(AdditionalCost))]
        public decimal AdditionalCost { get; set; }
        public bool IsAccepted { get; set; }
        public decimal TotalPrice
        {
            get
            {
                decimal price = 0;
                if (ProductVMs != null)
                {
                    for (int i = 0; i < ProductVMs.Count; i++)
                    {
                        price += ProductVMs[i].TotalPrice;
                    }
                }
                price += AdditionalCost;
                return price;
            }
        }
        public decimal LocalTotalPrice
        {
            get
            {
                return TotalPrice * ExchangeRate;
            }
        }
        public decimal TotalPriceWCIF
        {
            get
            {
                return TotalPrice + CIFCost;
            }
        }
        public decimal LocalTotalPriceWCIF
        {
            get
            {
                return TotalPriceWCIF * ExchangeRate;
            }
        }

        //[Required(ErrorMessage = "RequiredField")]
        //[Display(Name = nameof(Files))]
        public List<IFormFile> Files { get; set; }
        public List<SupplierOfferAttachmentViewModel> AttachmentVMs { get; set; }
        public List<SupplierOfferProductViewModel> ProductVMs { get; set; }
    }

    public class SupplierOfferProductViewModel
    {
        public int Id { get; set; }
        public int OfferId { get; set; }
        public int TechnicalSpecificationProductId { get; set; }
        public int ProductIndex { get; set; }
        public TechnicalSpecificationProductViewModel ProductVM { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice
        {
            get
            {
                if (ProductVM != null)
                    return Price * (ProductVM.Quantity != null ? (decimal)ProductVM.Quantity : 0);
                else
                    return 0;
            }
        }
        public IFormFile File { get; set; }
        public int? AttachmentId { get; set; }
        public string AttachmentTitle { get; set; }
        public string AttachmentPath { get; set; }

        public string SupplierName { get; set; }
        public string SupplierNameAr { get; set; }
        public decimal ExchangeRate { get; set; }
    }

    public class SupplierOfferAttachmentViewModel
    {
        public int Id { get; set; }
        public int OfferId { get; set; }
        public int AttachmentId { get; set; }
        public string AttachmentPath { get; set; }
        public string AttachmentTitle { get; set; }
    }

    public class CalculatedCurrencyViewModel
    {
        public decimal Price { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal LocalPrice
        {
            get
            {
                return Price * ExchangeRate;
            }
        }
    }

    public class SupplierOffersViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public int OffersCount { get; set; }
        public bool HasOffers
        {
            get
            {
                return OffersCount > 0;
            }
        }
    }
}
