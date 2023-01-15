using portal.speedspot.Models.Concretes;
using System.ComponentModel.DataAnnotations;

namespace portal.speedspot.Models.BasesAndAbstracts
{
    public class Partner : EntityBase
    {
        public int? LogoAttachmentId { get; set; }
        public Attachment LogoAttachment { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string SubName { get; set; }
        [Required]
        public string NameAr { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Url]
        public string WebsiteUrl { get; set; }
        public int? ActivityTypeId { get; set; }
        public ActivityType ActivityType { get; set; }
        public int? OrganizationTypeId { get; set; }
        public OrganizationType OrganizationType { get; set; }
        public int? ParentId { get; set; }
        public int? LegalInfoId { get; set; }
        public LegalInfo LegalInfo { get; set; }
        public int? AddressId { get; set; }
        public Address Address { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryAddress { get; set; }
    }
}
