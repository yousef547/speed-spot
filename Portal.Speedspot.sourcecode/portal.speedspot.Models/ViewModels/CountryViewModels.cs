using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class CountryViewModel
    {
        public int Id { get; set; }

        [Display(Name = nameof(Name))]
        [Required(ErrorMessage = "RequiredField")]
        public string Name { get; set; }

        [Display(Name = nameof(NameAr))]
        [Required(ErrorMessage = "RequiredField")]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(PhoneCode))]
        [Phone(ErrorMessage = "InvalidNumber")]
        public string PhoneCode { get; set; }

        [Display(Name = nameof(Code2))]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "InvalidStringLength")]
        public string Code2 { get; set; }

        [Display(Name = nameof(Code3))]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "InvalidStringLength")]
        public string Code3 { get; set; }
        public bool IsArchived { get; set; }
    }
}
