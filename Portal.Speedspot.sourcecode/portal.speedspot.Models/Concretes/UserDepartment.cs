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
    public class UserDepartment : EntityBase
    {
        [Required]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public bool IsActive { get; set; }
    }
}
