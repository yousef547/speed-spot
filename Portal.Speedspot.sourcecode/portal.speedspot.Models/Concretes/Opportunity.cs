using portal.speedspot.Models.BasesAndAbstracts;
using portal.speedspot.Models.Concretes.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.Concretes
{
    public class Opportunity : EntityBase
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int TypeId { get; set; }
        public OpportunityType Type { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int? CountryId { get; set; }
        public Country Country { get; set; }
        public string Address { get; set; }
        [Required]
        public string SalesAgentId { get; set; }
        public ApplicationUser SalesAgent { get; set; }
        public string ProjectManagerId { get; set; }
        public ApplicationUser ProjectManager { get; set; }
        public string GuestId { get; set; }
        public ApplicationUser Guest { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Budget { get; set; }
        public bool IsTechnicalApproved { get; set; }
        public string TechnicalApprovedById { get; set; }
        public ApplicationUser TechnicalApprovedBy { get; set; }
        public bool IsConverted { get; set; }
        public string ConvertedById { get; set; }
        public ApplicationUser ConvertedBy { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public string Notes { get; set; }
        [Required]
        public int StatusId { get; set; }
        public OpportunityStatus Status { get; set; }
        [Required]
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public bool? IsLimitedTenderType { get; set; }
        public bool? IsLocalTenderType { get; set; }
        public string TenderNumber { get; set; }
        [Required]
        public int SerialNo { get; set; }
        public BidBond BidBond { get; set; }
        public BookTender BookTender { get; set; }
        public virtual TechnicalSpecification TechnicalSpecification { get; set; }
        public virtual IList<RequestOffer> RequestOffers { get; set; }
        public virtual IList<SupplierOffer> SupplierOffers { get; set; }
        public virtual IList<OpportunityAttachment> OtherAttachments { get; set; }

        public static implicit operator string(Opportunity v)
        {
            throw new NotImplementedException();
        }

        //public Opportunity()
        //{
        //    if (BidBond == null) BidBond = new BidBond();
        //    if (BookTender == null) BookTender = new BookTender();
        //    if (TechnicalSpecification == null) TechnicalSpecification = new TechnicalSpecification();
        //    if (RequestOffers == null) RequestOffers = new List<RequestOffer>();
        //    if (SupplierOffers == null) SupplierOffers = new List<SupplierOffer>();
        //    if (OtherAttachments == null) OtherAttachments = new List<OpportunityAttachment>();
        //}
    }
}
