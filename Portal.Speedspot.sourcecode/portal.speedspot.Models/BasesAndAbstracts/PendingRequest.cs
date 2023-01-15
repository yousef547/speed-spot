using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.Concretes.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.BasesAndAbstracts
{
    public class PendingRequest : EntityBase
    {
       
        [Required]
        public string RequestedById { get; set; }
        public ApplicationUser RequestedBy { get; set; }
        public DateTime RequestedDate { get; set; }
        [Required]
        public RequestStatusEnum Status { get; set; }
        public DateTime? StatusDate { get; set; }
    
    }
}
