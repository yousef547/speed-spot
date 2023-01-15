using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class SupplierViewModel
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

        [Display(Name = "Departments")]
        [Required(ErrorMessage = "RequiredField")]
        public List<int> DepartmentIds { get; set; }
        public IList<SupplierDepartmentViewModel> DepartmentVMs { get; set; }
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

        [Display(Name = "Classifications")]
        [Required(ErrorMessage = "RequiredField")]
        public List<int> ClassificationIds { get; set; }
        public IList<SupplierClassificationViewModel> ClassificationVMs { get; set; }
        public string ClassificationStr
        {
            get
            {
                return string.Join(", ", ClassificationVMs.Select(d => d.ClassificationName).ToList());
            }
        }
        public string ClassificationStrAr
        {
            get
            {
                return string.Join(", ", ClassificationVMs.Select(d => d.ClassificationNameAr).ToList());
            }
        }

        public virtual IList<SupplierProductViewModel> ProductVMs { get; set; }
        public string ProductsStr
        {
            get
            {
                return string.Join(", ", ProductVMs.Select(d => d.ProductName).ToList());
            }
        }
        public string ProductsStrAr
        {
            get
            {
                return string.Join(", ", ProductVMs.Select(d => d.ProductNameAr).ToList());
            }
        }

        public bool IsFavourite { get; set; }

        public int? LegalInfoId { get; set; }
        public int? AddressId { get; set; }

        public string BeneficiaryName { get; set; }
        public string BeneficiaryAddress { get; set; }

        [Display(Name = nameof(IsAgency))]
        public bool IsAgency { get; set; }

        [Display(Name = "AgencyName")]
        public int? CompetitorId { get; set; }
        public string CompetitorName { get; set; }
        public string CompetitorNameAr { get; set; }

        public virtual IList<SupplierViewModel> ChildVMs { get; set; }
        public virtual LegalInfoViewModel LegalInfoVM { get; set; }
        public virtual AddressViewModel AddressVM { get; set; }
        public virtual IList<SupplierContactViewModel> ContactVMs { get; set; }
        public virtual IList<SupplierFileViewModel> FileVMs { get; set; }
        public virtual IList<IFormFile> Attachments { get; set; }
        public virtual IList<SupplierEmployeeViewModel> EmployeeVMs { get; set; }
        public virtual IList<SupplierCertificateViewModel> CertificateVMs { get; set; }
        public virtual IList<SupplierBankViewModel> BankVMs { get; set; }

        public List<IGrouping<string, SupplierProductViewModel>> DepartmentProductsGrouping
        {
            get
            {
                try
                {
                    return ProductVMs.GroupBy(g => g.ProductVM.CategoryVM.DepartmentName).ToList();
                }
                catch
                {
                    return new List<IGrouping<string, SupplierProductViewModel>>();
                }
            }
        }

        public List<IGrouping<string, SupplierProductViewModel>> DepartmentProductsGroupingAr
        {
            get
            {
                try
                {
                    return ProductVMs.GroupBy(g => g.ProductVM.CategoryVM.DepartmentNameAr).ToList();
                }
                catch
                {
                    return new List<IGrouping<string, SupplierProductViewModel>>();
                }
            }
        }

        public SupplierViewModel()
        {
            if (DepartmentIds == null) DepartmentIds = new List<int>();
            if (DepartmentVMs == null) DepartmentVMs = new List<SupplierDepartmentViewModel>();

            if (ClassificationIds == null) ClassificationIds = new List<int>();
            if (ClassificationVMs == null) ClassificationVMs = new List<SupplierClassificationViewModel>();

            if (ContactVMs == null) ContactVMs = new List<SupplierContactViewModel>();

            if (FileVMs == null) FileVMs = new List<SupplierFileViewModel>();

            if (EmployeeVMs == null) EmployeeVMs = new List<SupplierEmployeeViewModel>();

            if (CertificateVMs == null) CertificateVMs = new List<SupplierCertificateViewModel>();

            if (ProductVMs == null) ProductVMs = new List<SupplierProductViewModel>();

            if (ChildVMs == null) ChildVMs = new List<SupplierViewModel>();
        }
    }

    public class SupplierDepartmentViewModel
    {
        public int SupplierId { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentNameAr { get; set; }
    }

    public class SupplierClassificationViewModel
    {
        public int SupplierId { get; set; }
        public int ClassificationId { get; set; }
        public string ClassificationName { get; set; }
        public string ClassificationNameAr { get; set; }
    }

    public class SupplierContactViewModel
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int ContactId { get; set; }
        public ContactViewModel ContactVM { get; set; }

        public SupplierContactViewModel()
        {
            if (ContactVM == null) ContactVM = new ContactViewModel();
        }
    }

    public class SupplierFileViewModel
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int AttachmentId { get; set; }
        public string AttachmentPath { get; set; }
        public string AttachmentTitle { get; set; }
    }

    public class SupplierEmployeeViewModel
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int EmployeeId { get; set; }
        public PartnerEmployeeViewModel EmployeeVM { get; set; }

        public SupplierEmployeeViewModel()
        {
            if (EmployeeVM == null) EmployeeVM = new PartnerEmployeeViewModel();
        }
    }

    public class SupplierCertificateViewModel
    {
        public int? Id { get; set; }
        public int? SupplierId { get; set; }
        public int? CertificateId { get; set; }
        public CertificateViewModel CertificateVM { get; set; }
        public IFormFile File { get; set; }
    }

    public class SupplierProductViewModel
    {
        public int? Id { get; set; }
        public int? SupplierId { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductNameAr { get; set; }
        public ProductViewModel ProductVM { get; set; }
    }

    public class ProductSuppliersTreeViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public IList<SuppliersTreeViewModel> Suppliers { get; set; }
    }

    public class SuppliersTreeViewModel
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string CountryName { get; set; }
        public string CountryNameAr { get; set; }
    }

    public class SupplierProductsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public IList<ProductsViewModel> ProductVMs { get; set; }
        public string ProductsStr
        {
            get
            {
                return string.Join(", ", ProductVMs.Select(d => d.Name).ToList());
            }
        }
        public string ProductsStrAr
        {
            get
            {
                return string.Join(", ", ProductVMs.Select(d => d.NameAr).ToList());
            }
        }
    }

    public class ProductsViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
    }

    public class SupplierInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
    }

    public class DepartmentProductsViewModel
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmnetNameAr { get; set; }
        public IList<SupplierProductViewModel> ProductVMs { get; set; }
    }

    public class AgencyViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
    }
    public class CreateSupplierViewModel 
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
        public IList<SupplierDepartmentViewModel> DepartmentVMs { get; set; }
        public List<int> SupplierProductIds { get; set; } 
        public IList<SupplierProductViewModel> ProductVMs { get; set; }
    }
}
