using portal.speedspot.Models.BasesAndAbstracts;
using portal.speedspot.Models.Concretes.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace portal.speedspot.Models.Concretes
{
    public class UserActivity : EntityBase
    {
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }

        [Required]
        public string Activity { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
