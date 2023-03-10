using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class AlternateVersionCertificate : EntityBase
    {
        public int OfferId { get; set; }
        public AlternateVersion Offer { get; set; }
        public int CertificateId { get; set; }
        public OfferCertificate Certificate { get; set; }
    }
}
