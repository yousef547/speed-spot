using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class CertificatesBiz : BizBaseT<Certificate>
    {
        public CertificatesBiz(ICertificatesRepository repository) : base(repository)
        {

        }
    }
}
