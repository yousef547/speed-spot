using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class CompetitorOfferProduct : EntityBase
    {
        public int OfferId { get; set; }
        public CompetitorOffer Offer { get; set; }
        public int ProductId { get; set; }
        public TechnicalSpecificationProduct Product { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? UnitPrice { get; set; }

        public bool IsIncluded { get; set; }
        public IList<CompetitorOfferProductOrigin> Origins { get; set; }
    }
}
