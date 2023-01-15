using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class CityViewModel
    {
        public int Id { get; set; }

        [Display(Name = nameof(CountryId))]
        [Required(ErrorMessage = "RequiredField")]
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryNameAr { get; set; }

        [Display(Name = nameof(Name))]
        [Required(ErrorMessage = "RequiredField")]
        public string Name { get; set; }

        [Display(Name = nameof(NameAr))]
        [Required(ErrorMessage = "RequiredField")]
        public string NameAr { get; set; }
        public bool IsArchived { get; set; }
    }

    public class CityListViewModel
    {
        public int? CityId { get; set; }
        public IList<City> Cities { get; set; }

        public CityListViewModel()
        {
            if (Cities == null)
                Cities = new List<City>();
        }
    }
}
