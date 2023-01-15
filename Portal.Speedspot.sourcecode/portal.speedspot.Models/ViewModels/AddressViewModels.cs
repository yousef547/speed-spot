using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class AddressViewModel
    {
        public int? Id { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        [Display(Name = nameof(CountryId))]
        public int? CountryId { get; set; }
        [Display(Name = nameof(CountryId))]
        public string CountryName { get; set; }
        [Display(Name = nameof(CountryId))]
        public string CountryNameAr { get; set; }

        [Display(Name = nameof(CityId))]
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public string CityNameAr { get; set; }

        [Display(Name = nameof(Street))]
        public string Street { get; set; }

        [Display(Name = nameof(BillingAddress))]
        public string BillingAddress { get; set; }
    }
}
