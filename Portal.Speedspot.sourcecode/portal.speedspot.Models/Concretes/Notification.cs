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
    public class Notification : EntityBase
    {
        [Required]
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public NotificationTypeEnum Type { get; set; }
        public string Route { get; set; }
        public string Details { get; set; }
        public ICollection<NotificationUser> NotificationUsers { get; set; }
    }
}
