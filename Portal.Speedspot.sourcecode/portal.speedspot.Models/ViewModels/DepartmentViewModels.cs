using Microsoft.AspNetCore.Http;
using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [Display(Name = nameof(Code))]
        [Required(ErrorMessage = "RequiredField")]
        public string Code { get; set; }
        [Display(Name = nameof(Name))]
        [Required(ErrorMessage = "RequiredField")]
        public string Name { get; set; }

        [Display(Name = nameof(NameAr))]
        [Required(ErrorMessage = "RequiredField")]
        public string NameAr { get; set; }

        [Display(Name = nameof(Description))]
        public string Description { get; set; }
        //[RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.JPG|.PNG|.jpg|.png)$", ErrorMessage = "FileNotAllowed")]

        [Display(Name = nameof(ImageFile))]
        public IFormFile ImageFile { get; set; }
        public string Image { get; set; }


        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(DefaultOfferCoverLetter))]
        public string DefaultOfferCoverLetter { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(DefaultOfferCoverLetterAr))]
        public string DefaultOfferCoverLetterAr { get; set; }


        [Display(Name = nameof(ManagingDirectorId))]
        public string ManagingDirectorId { get; set; }
        public string ManagingDirectorName { get; set; }
        public string ManagingDirectorPhone { get; set; }
        public string ManagingDirectorEmail { get; set; }

        [Display(Name = nameof(SalesDirectorId))]
        public string SalesDirectorId { get; set; }
        public string SalesDirectorName { get; set; }
        public string SalesDirectorPhone { get; set; }
        public string SalesDirectorEmail { get; set; }

        [Display(Name = nameof(Color))]
        public Color Color { get; set; }
        public bool IsArchived { get; set; }
        public IList<OpportunityViewModel> OpportunitiesVMs { get; set; }
        public IList<DepartmentDocumentViewModel> DepartmentDocumentVMs { get; set; }
        public IList<DepartmentBankViewModel> DepartmentBankVMs { get; set; }

        public int OpportunitiesCount
        {
            get { return OpportunitiesVMs != null ? OpportunitiesVMs.Count : 0; }
        }
        [Display(Name = nameof(BeneficiaryName))]
        public string BeneficiaryName { get; set; }
        [Display(Name = nameof(BeneficiaryAddress))]
        public string BeneficiaryAddress { get; set; }
        public bool IsFavourite { get; set; }


    }

    public class UserDepartmentsViewModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public IList<DepartmentsViewModel> UserDepartments { get; set; }
    }
    public class DepartmentsViewModel
    {
        public string DepartmentName { get; set; }
        public string DepartmentNameAr { get; set; }
        public int DepartmentId { get; set; }
        public bool Selected { get; set; }
    }

    public class BeneficiaryInfo
    {
        public int Id { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryAddress { get; set; }
    }

    public class DepartmentSettingsViewModel
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public DepartmentViewModel DepartmentVM { get; set; }

        [Display(Name = nameof(DefaultTreasuryAccountId))]
        public int? DefaultTreasuryAccountId { get; set; }
    }
}
