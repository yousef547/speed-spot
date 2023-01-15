using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class CompetitorOfferProductOrigin : EntityBase
    {
        public int ProductId { get; set; }
        public CompetitorOfferProduct Product { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
