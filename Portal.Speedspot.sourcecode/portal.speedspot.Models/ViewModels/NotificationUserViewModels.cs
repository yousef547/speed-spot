using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.ViewModels
{
    public class NotificationUserViewModel
    {
        public int Id { get; set; }
        public string CreatedByName { get; set; }
        public int NotificationId { get; set; }
        public string SendToId { get; set; }
        public string SendToName { get; set; }
        public string Description { get; set; }
        public NotificationTypeEnum NotificationType { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDelivered { get; set; }

    }
}
