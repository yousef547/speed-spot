using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class LogisticViewModel
    {
        public int Id { get; set; }
        public bool IsArchived { get; set; }

        [Display(Name = "LogoAttachmentId")]
        public int LogoAttachmentId { get; set; }

        public string LogoAttachmentPath { get; set; }

        [Display(Name = nameof(LogoFile))]
        public IFormFile LogoFile { get; set; }

        [Display(Name = nameof(Name))]
        [Required(ErrorMessage = "RequiredField")]
        public string Name { get; set; }

        [Display(Name = nameof(SubName))]
        [Required(ErrorMessage = "RequiredField")]
        public string SubName { get; set; }

        [Display(Name = nameof(NameAr))]
        [Required(ErrorMessage = "RequiredField")]
        public string NameAr { get; set; }

        [Display(Name = nameof(Email))]
        //[Required(ErrorMessage = "RequiredField")]
        [EmailAddress(ErrorMessage = "InvalidEmail")]
        public string Email { get; set; }

        [Display(Name = nameof(WebsiteUrl))]
        //[Required(ErrorMessage = "RequiredField")]
        [Url(ErrorMessage = "InvalidUrl")]
        public string WebsiteUrl { get; set; }

        [Display(Name = nameof(ActivityTypeId))]
        //[Required(ErrorMessage = "RequiredField")]
        public int? ActivityTypeId { get; set; }
        public string ActivityTypeName { get; set; }
        public string ActivityTypeNameAr { get; set; }

        [Display(Name = nameof(OrganizationTypeId))]
        //[Required(ErrorMessage = "RequiredField")]
        public int? OrganizationTypeId { get; set; }
        public string OrganizationTypeName { get; set; }
        public string OrganizationTypeNameAr { get; set; }
        [Display(Name = "ParentCompany")]
        public int? ParentId { get; set; }
        public string ParentName { get; set; }
        public string ParentNameAr { get; set; }

        [Display(Name = "IsSubCompany")]
        public bool IsSubCompany { get; set; }

        public int? LegalInfoId { get; set; }
        public int? AddressId { get; set; }

        public string CreatedById { get; set; }
        public string CreatedByName { get; set; }

        [Display(Name = "Departments")]
        [Required(ErrorMessage = "RequiredField")]
        public List<int> DepartmentIds { get; set; }
        public IList<LogisticDepartmentViewModel> DepartmentVMs { get; set; }
        public string DepartmentsStr
        {
            get
            {
                return string.Join(", ", DepartmentVMs.Select(d => d.DepartmentName).ToList());
            }
        }
        public string DepartmentsStrAr
        {
            get
            {
                return string.Join(", ", DepartmentVMs.Select(d => d.DepartmentNameAr).ToList());
            }
        }
        public bool IsFavourite { get; set; }

        public string BeneficiaryName { get; set; }
        public string BeneficiaryAddress { get; set; }

        public virtual IList<LogisticViewModel> ChildVMs { get; set; }
        public virtual LegalInfoViewModel LegalInfoVM { get; set; }
        public virtual AddressViewModel AddressVM { get; set; }

        public virtual IList<LogisticContactViewModel> ContactVMs { get; set; }
        public virtual IList<LogisticFileViewModel> FileVMs { get; set; }
        public virtual IList<IFormFile> Attachments { get; set; }
        public virtual IList<LogisticEmployeeViewModel> EmployeeVMs { get; set; }
        public virtual IList<LogisticBankViewModel> BankVMs { get; set; }

        public LogisticViewModel()
        {
            if (DepartmentIds == null) DepartmentIds = new List<int>();
            if (DepartmentVMs == null) DepartmentVMs = new List<LogisticDepartmentViewModel>();

            if (ContactVMs == null) ContactVMs = new List<LogisticContactViewModel>();

            if (FileVMs == null) FileVMs = new List<LogisticFileViewModel>();

            if (EmployeeVMs == null) EmployeeVMs = new List<LogisticEmployeeViewModel>();

            if (ChildVMs == null) ChildVMs = new List<LogisticViewModel>();
        }
    }

    public class LogisticDepartmentViewModel
    {
        public int LogisticId { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentNameAr { get; set; }
    }

    public class LogisticContactViewModel
    {
        public int Id { get; set; }
        public int LogisticId { get; set; }
        public int ContactId { get; set; }
        public ContactViewModel ContactVM { get; set; }

        public LogisticContactViewModel()
        {
            if (ContactVM == null) ContactVM = new ContactViewModel();
        }
    }

    public class LogisticFileViewModel
    {
        public int Id { get; set; }
        public int LogisticId { get; set; }
        public int AttachmentId { get; set; }
        public string AttachmentPath { get; set; }
        public string AttachmentTitle { get; set; }
    }

    public class LogisticEmployeeViewModel
    {
        public int Id { get; set; }
        public int LogisticId { get; set; }
        public int EmployeeId { get; set; }
        public PartnerEmployeeViewModel EmployeeVM { get; set; }

        public LogisticEmployeeViewModel()
        {
            if (EmployeeVM == null) EmployeeVM = new PartnerEmployeeViewModel();
        }
    }
}
