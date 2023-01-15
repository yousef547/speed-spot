using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class OfferCertificatesBiz : BizBaseT<OfferCertificate>
    {
        public OfferCertificatesBiz(IOfferCertificatesRepository repository) : base(repository)
        {

        }
    }
}
