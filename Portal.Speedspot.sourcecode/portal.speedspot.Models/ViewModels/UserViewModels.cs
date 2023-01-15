using Microsoft.AspNetCore.Http;
using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Display(Name = nameof(FirstName))]
        [Required(ErrorMessage = "RequiredField")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(LastName))]
        public string LastName { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [EmailAddress(ErrorMessage = "InvalidEmail")]
        [Display(Name = nameof(Email))]
        public string Email { get; set; }

        public virtual bool EmailConfirmed { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(PhoneNumber))]
        [Phone(ErrorMessage = "InvalidPhoneNumber")]
        [StringLength(20, MinimumLength = 9, ErrorMessage = "PhoneNumberLength")]
        public string PhoneNumber { get; set; }

        public string ProfilePicture { get; set; }

        public int EmployeeTypeId { get; set; }
        public string EmployeeTypeName { get; set; }
        public string EmployeeTypeNameAr { get; set; }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentNameAr { get; set; }

        public bool IsArchived { get; set; }
    }

    public class UserInfoViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Display(Name = nameof(FullName))]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [Display(Name = nameof(Email))]
        public string Email { get; set; }

        [Display(Name = nameof(PhoneNumber))]
        public string PhoneNumber { get; set; }

        public string ProfilePicture { get; set; }

        [Display(Name = nameof(EmployeeTypeId))]
        public int EmployeeTypeId { get; set; }
        public string EmployeeTypeName { get; set; }
        public string EmployeeTypeNameAr { get; set; }

        [Display(Name = nameof(DefaultDepartmentId))]
        public int DefaultDepartmentId { get; set; }
        public string DefaultDepartmentName { get; set; }
        public string DefaultDepartmentNameAr { get; set; }

        [Display(Name = nameof(DirectManagerId))]
        public string DirectManagerId { get; set; }
        public string DirectManagerName { get; set; }

        public bool IsArchived { get; set; }
        [Display(Name = "Departments")]
        public ICollection<UserDepartmentViewModel> DepartmentVMs { get; set; }

        [Display(Name = "Roles")]
        public ICollection<UserRoleViewModel> RoleVMs { get; set; }

        public UserInfoViewModel()
        {
            if (DepartmentVMs == null) DepartmentVMs = new List<UserDepartmentViewModel>();

            if (RoleVMs == null) RoleVMs = new List<UserRoleViewModel>();
        }
    }

    public class UserDepartmentViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentNameAr { get; set; }
        public bool IsActive { get; set; }
        public IList<DepartmentViewModel> DepartmentVMs { get; set; }
    }

    public class UserRoleViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleNameAr { get; set; }
    }

    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(FirstName))]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(LastName))]
        public string LastName { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [EmailAddress(ErrorMessage = "InvalidEmail")]
        [Display(Name = nameof(Email))]
        public string Email { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [DataType(DataType.Password)]
        [Display(Name = nameof(Password))]
        public string Password { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "InvalidConfirmPassword")]
        [Display(Name = nameof(ConfirmPassword))]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Phone(ErrorMessage = "InvalidPhoneNumber")]
        [StringLength(20, MinimumLength = 9, ErrorMessage = "PhoneNumberLength")]
        [Display(Name = nameof(PhoneNumber))]
        public string PhoneNumber { get; set; }

        [Display(Name = nameof(ProfileImage))]
        public IFormFile ProfileImage { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(EmployeeTypeId))]
        public int EmployeeTypeId { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "DefaultDepartment")]
        public int DepartmentId { get; set; }

        [Display(Name = nameof(DirectManagerId))]
        public string DirectManagerId { get; set; }

        [Display(Name = nameof(SupervisorIds))]
        public IList<string> SupervisorIds { get; set; }
    }

    public class EditUserViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(FirstName))]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(LastName))]
        public string LastName { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [EmailAddress(ErrorMessage = "InvalidEmail")]
        [Display(Name = nameof(Email))]
        public string Email { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Phone(ErrorMessage = "InvalidPhoneNumber")]
        [StringLength(20, MinimumLength = 9, ErrorMessage = "PhoneNumberLength")]
        [Display(Name = nameof(PhoneNumber))]
        public string PhoneNumber { get; set; }

        [Display(Name = nameof(ProfileImage))]
        public IFormFile ProfileImage { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(EmployeeTypeId))]
        public int EmployeeTypeId { get; set; }


        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "DefaultDepartment")]
        public int DepartmentId { get; set; }

        [Display(Name = nameof(DirectManagerId))]
        public string DirectManagerId { get; set; }

        [Display(Name = nameof(SupervisorIds))]
        public IList<string> SupervisorIds { get; set; }
    }
}
