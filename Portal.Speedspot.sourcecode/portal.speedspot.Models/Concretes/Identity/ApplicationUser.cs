using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.Concretes.Identity
{
    [Index(nameof(Id), Name = "IX_User", IsUnique = true)]
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            if (string.IsNullOrEmpty(Id)) Id = Guid.NewGuid().ToString();
        }

        public override string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Name
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        [Required]
        [EmailAddress]
        public override string Email { get; set; }
        [Required]
        [Phone]
        [StringLength(20, MinimumLength = 9, ErrorMessage = "Phone must be between 9 to 20 letters.")]
        public override string PhoneNumber { get; set; }
        public string ProfilePicture { get; set; }
        [Required]
        public int EmployeeTypeId { get; set; }
        public EmployeeType EmployeeType { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public bool IsArchived { get; set; }
        public string DirectManagerId { get; set; }
        public ApplicationUser DirectManager { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public virtual ICollection<UserDepartment> UserDepartments { get; set; }
        public virtual ICollection<ApplicationUser> Employees { get; set; }

        [InverseProperty("ManagingDirector")]
        public virtual ICollection<Department> ManagingDepartments { get; set; }

        [InverseProperty("SalesDirector")]
        public virtual ICollection<Department> SalesDepartments { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UserSupervisor> Supervisors { get; set; }
    }
}