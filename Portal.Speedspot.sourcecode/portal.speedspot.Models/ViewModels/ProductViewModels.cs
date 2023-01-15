using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Display(Name = nameof(IsService))]
        public bool IsService { get; set; }

        [Display(Name = nameof(DepartmentId))]
        [Required(ErrorMessage = "RequiredField")]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentNameAr { get; set; }

        [Display(Name = nameof(Code))]
        [Required(ErrorMessage = "RequiredField")]
        public string Code { get; set; }

        [Display(Name = nameof(Name))]
        [Required(ErrorMessage = "RequiredField")]
        public string Name { get; set; }

        [Display(Name = nameof(NameAr))]
        [Required(ErrorMessage = "RequiredField")]
        public string NameAr { get; set; }

        [Display(Name = nameof(CategoryId))]
        [Required(ErrorMessage = "RequiredField")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryNameAr { get; set; }
        public ProductCategoryViewModel CategoryVM { get; set; }
        public string AutoCode { get; set; }
        public string CategoryTree
        {
            get
            {
                return CategoryVM != null ? (CategoryVM.ParentTree != "" ? CategoryVM.ParentTree + ", " : "") + CategoryVM.Name : "";
            }
        }
        public string CategoryTreeAr
        {
            get
            {
                return CategoryVM != null ? (CategoryVM.ParentTreeAr != "" ? CategoryVM.ParentTreeAr + ", " : "") + CategoryVM.NameAr : "";
            }
        }
        public bool IsArchived { get; set; }
        public string GeneratedCode
        {
            get
            {
                return (CategoryVM != null ? CategoryVM.GeneratedCode : "") + AutoCode;
            }
        }
    }

    public class ProductCategoryViewModel
    {
        public int Id { get; set; }

        [Display(Name = nameof(IsService))]
        public bool IsService { get; set; }

        [Display(Name = nameof(DepartmentId))]
        [Required(ErrorMessage = "RequiredField")]
        public int DepartmentId { get; set; }
        public DepartmentViewModel DepartmentVM { get; set; }

        public string DepartmentName { get; set; }
        public string DepartmentNameAr { get; set; }
        public string DepartmentCode { get; set; }

        [Display(Name = nameof(Code))]
        [Required(ErrorMessage = "RequiredField")]
        public string Code { get; set; }

        [Display(Name = nameof(Name))]
        [Required(ErrorMessage = "RequiredField")]
        public string Name { get; set; }

        [Display(Name = nameof(NameAr))]
        [Required(ErrorMessage = "RequiredField")]
        public string NameAr { get; set; }

        [Display(Name = "ParentCategory")]
        public int? ParentId { get; set; }

        public string ParentName { get; set; }
        public string ParentNameAr { get; set; }
        public bool IsArchived { get; set; }
        public string AutoCode { get; set; }

        public ProductCategoryViewModel ParentVM { get; set; }

        public string ParentTree
        {
            get
            {
                return ParentVM != null ? (ParentVM.ParentTree != "" ? ParentVM.ParentTree + ", " : "") + ParentVM.Name : "";
            }
        }

        public string ParentTreeAr
        {
            get
            {
                return ParentVM != null ? (ParentVM.ParentTreeAr != "" ? ParentVM.ParentTreeAr + ", " : "") + ParentVM.NameAr : "";
            }
        }

        public virtual IList<ProductCategoryViewModel> ChildVMs { get; set; }
        public virtual IList<ProductViewModel> ProductVMs { get; set; }

        public IList<ProductCategoryViewModel> UnarchivedChildVMs
        {
            get
            {
                return ChildVMs != null ? ChildVMs.Where(c => !c.IsArchived).ToList() : new();
            }
        }

        public IList<ProductViewModel> UnarchivedProductVMs
        {
            get
            {
                return ProductVMs != null ? ProductVMs.Where(c => c.IsArchived == false).ToList() : new List<ProductViewModel>();
            }
        }

        public int ChildsCount
        {
            get
            {
                return UnarchivedChildVMs.Count;
            }
        }

        public int ProductsCount
        {
            get
            {
                return UnarchivedProductVMs.Count;
            }
        }

        public string GeneratedCode
        {
            get
            {
                return (ParentVM != null ? ParentVM.GeneratedCode : DepartmentCode) + AutoCode;
            }
        }
    }
}
