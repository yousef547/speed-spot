using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class CustomerViewModel
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

        [Display(Name = nameof(Rank))]
        [Range(10, 100, ErrorMessage = "InvalidRange")]
        public int Rank { get; set; } = 10;

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
        public IList<CustomerDepartmentViewModel> DepartmentVMs { get; set; }
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

        [Display(Name = "SalesAgents")]
        [Required(ErrorMessage = "RequiredField")]
        public List<string> SalesAgentIds { get; set; }
        public IList<CustomerAgentViewModel> SalesAgentVMs { get; set; }
        public string SalesAgentsStr
        {
            get
            {
                return string.Join(", ", SalesAgentVMs.Select(d => d.SalesAgentName).ToList());
            }
        }

        public bool IsFavourite { get; set; }

        public string BeneficiaryName { get; set; }
        public string BeneficiaryAddress { get; set; }

        public virtual IList<CustomerViewModel> ChildVMs { get; set; }
        public virtual LegalInfoViewModel LegalInfoVM { get; set; }
        public virtual AddressViewModel AddressVM { get; set; }

        public virtual IList<CustomerContactViewModel> ContactVMs { get; set; }

        public virtual IList<CustomerVendorRegistrationViewModel> VendorRegistrationVMs { get; set; }

        public virtual IList<CustomerFileViewModel> FileVMs { get; set; }
        public virtual IList<IFormFile> Attachments { get; set; }
        public virtual IList<CustomerEmployeeViewModel> EmployeeVMs { get; set; }
        public virtual IList<CustomerBankViewModel> BankVMs { get; set; }
        public virtual IList<VisitViewModel> VisitVMs { get; set; }

        public CustomerViewModel()
        {
            if (DepartmentIds == null) DepartmentIds = new List<int>();
            if (DepartmentVMs == null) DepartmentVMs = new List<CustomerDepartmentViewModel>();

            if (SalesAgentIds == null) SalesAgentIds = new List<string>();
            if (SalesAgentVMs == null) SalesAgentVMs = new List<CustomerAgentViewModel>();

            if (ContactVMs == null) ContactVMs = new List<CustomerContactViewModel>();

            if (VendorRegistrationVMs == null) VendorRegistrationVMs = new List<CustomerVendorRegistrationViewModel>();

            if (FileVMs == null) FileVMs = new List<CustomerFileViewModel>();

            if (EmployeeVMs == null) EmployeeVMs = new List<CustomerEmployeeViewModel>();

            if (ChildVMs == null) ChildVMs = new List<CustomerViewModel>();
        }
    }

    public class CustomerDepartmentViewModel
    {
        public int CustomerId { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentNameAr { get; set; }
    }

    public class CustomerContactViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ContactId { get; set; }
        public ContactViewModel ContactVM { get; set; }

        public CustomerContactViewModel()
        {
            if (ContactVM == null) ContactVM = new ContactViewModel();
        }
    }

    public class CustomerVendorRegistrationViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string BookRegistrationNumber { get; set; }
        public decimal? Value { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public decimal? VAT { get; set; }
        public int? AttachmentId { get; set; }
        public string AttachmentPath { get; set; }
        public string AttachmentTitle { get; set; }
        public IFormFile File { get; set; }
    }

    public class CustomerFileViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int AttachmentId { get; set; }
        public string AttachmentPath { get; set; }
        public string AttachmentTitle { get; set; }
    }

    public class CustomerEmployeeViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public PartnerEmployeeViewModel EmployeeVM { get; set; }

        public CustomerEmployeeViewModel()
        {
            if (EmployeeVM == null) EmployeeVM = new PartnerEmployeeViewModel();
        }
    }

    public class CustomerAgentViewModel
    {
        public int CustomerId { get; set; }
        public string SalesAgentId { get; set; }
        public string SalesAgentName { get; set; }
    }

    public class CustomerInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string LogoPath { get; set; }
    }

    public class CreateCustomerViewModel
    {

        [Display(Name = nameof(Name))]
        [Required(ErrorMessage = "RequiredField")]
        public string Name { get; set; }

        [Display(Name = nameof(SubName))]
        [Required(ErrorMessage = "RequiredField")]
        public string SubName { get; set; }

        [Display(Name = nameof(NameAr))]
        [Required(ErrorMessage = "RequiredField")]
        public string NameAr { get; set; }
        [Display(Name = "Departments")]
        [Required(ErrorMessage = "RequiredField")]
        public List<int> DepartmentIds { get; set; }
        public IList<CustomerDepartmentViewModel> DepartmentVMs { get; set; }
    }
}
