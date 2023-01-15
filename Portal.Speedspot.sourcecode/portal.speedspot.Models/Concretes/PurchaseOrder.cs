using portal.speedspot.Models.BasesAndAbstracts;
using portal.speedspot.Models.Concretes.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.Concretes
{
    public class PurchaseOrder : EntityBase
    {
        public int QuotationId { get; set; }
        public Quotation Quotation { get; set; }
        public FundDetails FundDetails { get; set; }
        public PurchaseOrderStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        [Required]
        public string LastUpdatedById { get; set; }
        public FundType? Type { get; set; } 
        public ApplicationUser LastUpdatedBy { get; set; }

        public int SelectedVersionId { get; set; }
        public QuotationOfferVersion SelectedVersion { get; set; }

        public IList<PurchaseOrderSupplierOffer> AcceptedOffers { get; set; }
        public IList<SupplierPo> SupplierPos { get; set; }
        public CustomerPo CustomerPo { get; set; }

    }
}
