using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes.Identity
{
    public class ApplicationRole : IdentityRole<string>
    {
        public ApplicationRole() : base()
        {
            if (string.IsNullOrEmpty(Id)) Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string NameAr { get; set; }
        public bool IsArchived { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
