using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class RequestOfferViewModel
    {
        public int Id { get; set; }
        public int OpportunityId { get; set; }
        public int SupplierId { get; set; }
        public SupplierInfoViewModel SupplierVM { get; set; }
        public string SupplierName { get; set; }
        public string SupplierNameAr { get; set; }
        //public string Description { get; set; }
        public bool IsSendEmail { get; set; }
        public bool IsEmailSent { get; set; }
        //public int RequestOfferAttachmentId { get; set; }
        //public string RequestOfferAttachmentPath { get; set; }
        //public string RequestOfferAttachmentTitle { get; set; }
        //public string RequestOfferAttachmentUploadedByName { get; set; }
        //public IFormFile RequestOfferFile { get; set; }
        public virtual IList<RequestOfferProductViewModel> ProductVMs { get; set; }
    }

    public class RequestOfferProductViewModel
    {
        public int Id { get; set; }
        public int RequestOfferId { get; set; }
        public int? ProductId { get; set; }
        public TechnicalSpecificationProductViewModel ProductVM { get; set; }
    }
}
