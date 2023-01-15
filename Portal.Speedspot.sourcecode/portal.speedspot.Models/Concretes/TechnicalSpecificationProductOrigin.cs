using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class TechnicalSpecificationProductOrigin : EntityBase
    {
        public int TechnicalSpecificationProductId { get; set; }
        public TechnicalSpecificationProduct TechnicalSpecificationProduct { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
