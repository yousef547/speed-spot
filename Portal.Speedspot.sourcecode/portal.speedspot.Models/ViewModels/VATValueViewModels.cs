using System.ComponentModel.DataAnnotations;

namespace portal.speedspot.Models.ViewModels
{
    public class VATValueViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(Title))]
        public string Title { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [Display(Name = nameof(Value))]
        public decimal Value { get; set; }

        public string DisplayText { get; set; }

        public string DisplayValue { get; set; }

        public bool IsArchived { get; set; }
    }
}
