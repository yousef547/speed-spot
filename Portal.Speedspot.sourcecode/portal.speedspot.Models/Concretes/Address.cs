using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class Address : EntityBase
    {
        [Column(TypeName = "decimal(12,9)")]
        public decimal? Latitude { get; set; }

        [Column(TypeName = "decimal(12,9)")]
        public decimal? Longitude { get; set; }

        public int? CityId { get; set; }
        public City City { get; set; }
        public string Street { get; set; }
        public string BillingAddress { get; set; }
    }
}
