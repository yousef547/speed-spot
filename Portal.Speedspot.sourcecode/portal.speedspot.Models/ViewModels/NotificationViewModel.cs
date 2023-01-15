using portal.speedspot.Models.Concretes.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.ViewModels
{
    public class NotificationViewModel
    {
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public NotificationTypeEnum Type { get; set; }
        public string TypeStr { get; set; }
        public string Route { get; set; }
        public string Details { get; set; }
        public ICollection<NotificationUserViewModel> NotificationUserVMs { get; set; }
    }
}
