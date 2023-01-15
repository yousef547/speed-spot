using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(Name))]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(NameAr))]
        public string NameAr { get; set; }

        [Display(Name = nameof(Description))]
        public string Description { get; set; }

        public bool IsArchived { get; set; }
    }
    public class PermissionViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleNameAr { get; set; }
        public IList<RolePermissionsViewModel> RolePermissionsVM { get; set; }
    }

    public class RolePermissionsViewModel
    {
        public string Module { get; set; }
        public List<RoleClaimsViewModel> PermissionVMs { get; set; }
    }

    public class RoleClaimsViewModel
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }
    }

    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public IList<RolesViewModel> UserRoles { get; set; }
    }
    public class RolesViewModel
    {
        public string RoleName { get; set; }
        public string RoleNameAr { get; set; }
        public bool Selected { get; set; }
    }
}
