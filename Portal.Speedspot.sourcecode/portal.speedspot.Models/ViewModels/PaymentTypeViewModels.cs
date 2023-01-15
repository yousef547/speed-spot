using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class PaymentTypeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = "NameAr")]
        public string NameAr { get; set; }
        public bool IsArchived { get; set; }
    }
}
