using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class UserActivityViewModel
    {
        public int Id { get; set; }
        
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserProfilePicture { get; set; }

        public string DepartmentName { get; set; }
        public string DepartmentNameAr { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public string Activity { get; set; }

        public string Description { get; set; }
    }
}
