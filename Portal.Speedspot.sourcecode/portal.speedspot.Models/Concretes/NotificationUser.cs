using portal.speedspot.Models.BasesAndAbstracts;
using portal.speedspot.Models.Concretes.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class NotificationUser : EntityBase
    {
        public int NotificationId { get; set; }
        public Notification Notification { get; set; }
        [Required]
        public string SendToId { get; set; }
        public ApplicationUser SendTo { get; set; }
        public bool IsRead { get; set; }
        public bool IsDelivered { get; set; }
    }
}
