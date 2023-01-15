using Microsoft.AspNetCore.Http;
using portal.speedspot.Resources.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class RegisterViewModel
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
        [Display(Name = nameof(Password))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [DataType(DataType.Password)]
        [Display(Name = nameof(ConfirmPassword))]
        [Compare(nameof(Password), ErrorMessage = "InvalidConfirmPassword")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(PhoneNumber))]
        [Phone(ErrorMessage = "InvalidPhoneNumber")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(ProfileImage))]
        public IFormFile ProfileImage { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "RequiredField")]
        [EmailAddress(ErrorMessage = "InvalidEmail")]
        [Display(Name = nameof(Email))]
        public string Email { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [DataType(DataType.Password)]
        [Display(Name = nameof(Password))]
        public string Password { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "RequiredField")]
        [EmailAddress(ErrorMessage = "InvalidEmail")]
        [Display(Name = nameof(Email))]
        public string Email { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "RequiredField")]
        [EmailAddress(ErrorMessage = "InvalidEmail")]
        [Display(Name = nameof(Email))]
        public string Email { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [DataType(DataType.Password)]
        [Display(Name = nameof(Password))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(ConfirmPassword))]
        [Compare(nameof(Password), ErrorMessage = "InvalidConfirmPassword")]
        public string ConfirmPassword { get; set; }

        [Display(Name = nameof(Code))]
        public string Code { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "RequiredField")]
        [DataType(DataType.Password)]
        [Display(Name = nameof(CurrentPassword))]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(ConfirmPassword))]
        [Compare(nameof(Password), ErrorMessage = "InvalidConfirmPassword")]
        public string ConfirmPassword { get; set; }
    }

    public class EditProfileViewModel
    {
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(FirstName))]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(LastName))]
        public string LastName { get; set; }

        [Display(Name = nameof(EmailAddress))]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(PhoneNumber))]
        [Phone(ErrorMessage = "InvalidPhoneNumber")]
        public string PhoneNumber { get; set; }

        [Display(Name = nameof(ProfileImage))]
        public IFormFile ProfileImage { get; set; }
        public string ProfilePicture { get; set; }

        [Display(Name = nameof(EmployeeTypeId))]
        public int EmployeeTypeId { get; set; }
        public string EmployeeTypeName { get; set; }
        public string EmployeeTypeNameAr { get; set; }

        [Display(Name = nameof(DepartmentId))]
        public int DepartmentId { get; set; }
        public string DefaultDepartmentName { get; set; }
        public string DefaultDepartmentNameAr { get; set; }

        [Display(Name = nameof(DirectManagerId))]
        public string DirectManagerId { get; set; }
        public string DirectManagerName { get; set; }

        public bool IsArchived { get; set; }

        public ICollection<UserDepartmentViewModel> DepartmentVMs { get; set; }
    }
}
