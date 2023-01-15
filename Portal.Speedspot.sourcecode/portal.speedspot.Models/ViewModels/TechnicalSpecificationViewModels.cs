using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class TechnicalSpecificationViewModel
    {
        public int Id { get; set; }
        public int OpportunityId { get; set; }
        [Display(Name = "TechnicalSpecifications")]
        public string Specifications { get; set; }
        public virtual IList<IFormFile> Files { get; set; }
        [Display(Name = "AttachmentFiles")]
        public virtual IList<TechnicalSpecificationAttachmentViewModel> AttachmentFiles { get; set; }
        public virtual IList<TechnicalSpecificationProductViewModel> ProductVMs { get; set; }
    }

    public class TechnicalSpecificationProductDetailsViewModel
    {
        public int Index { get; set; }
        public TechnicalSpecificationProductViewModel VM { get; set; }
    }

    public class TechnicalSpecificationProductViewModel
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public int TechnicalSpecificationId { get; set; }
        public int? ProductId { get; set; }
        public ProductViewModel ProductVM { get; set; }
        public string ProductName { get; set; }
        public string ProductNameAr { get; set; }
        public int? ProductOriginId { get; set; }
        public string ProductOriginName { get; set; }
        public string ProductOriginNameAr { get; set; }
        [Display(Name = "Quantity")]
        public decimal? Quantity { get; set; }
        public string Description { get; set; }
        public string ProductSelectStr { get; set; }
        public string ProductSelectStrAr { get; set; }
        public IFormFile AttachmentFile { get; set; }
        public int? AttachmentId { get; set; }
        public string AttachmentPath { get; set; }
        public string AttachmentTitle { get; set; }
        public virtual IList<TechnicalSpecificationProductOriginViewModel> RequestedOriginVMs { get; set; }
        public string RequestedOriginIdsStr { get; set; }
        public List<int> RequestedOriginIds
        {
            get
            {
                return RequestedOriginVMs != null ? RequestedOriginVMs.Select(x => x.CountryId).ToList() : new();
            }
        }
        public string RequestedOriginsStr
        {
            get
            {
                return RequestedOriginVMs != null ? string.Join(", ", RequestedOriginVMs.Select(d => d.CountryName).ToList()) : "";
            }
        }
        public string RequestedOriginsStrAr
        {
            get
            {
                return RequestedOriginVMs != null ? string.Join(", ", RequestedOriginVMs.Select(d => d.CountryNameAr).ToList()) : "";
            }
        }
    }

    public class TechnicalSpecificationAttachmentViewModel
    {
        public int Id { get; set; }
        public int TechnicalSpecificationId { get; set; }
        public int? AttachmentId { get; set; }
        public string AttachmentPath { get; set; }
        public string Title { get; set; }
        public string AttachmentUploadedByName { get; set; }
    }

    public class TechnicalSpecificationProductOriginViewModel
    {
        public int Id { get; set; }
        public int TechnicalSpecificationProductId { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryNameAr { get; set; }
    }
}
