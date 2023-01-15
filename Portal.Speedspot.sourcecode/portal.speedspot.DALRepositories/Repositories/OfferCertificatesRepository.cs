using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class OfferCertificatesRepository : RepositoryBase<OfferCertificate>, IOfferCertificatesRepository
    {
        public OfferCertificatesRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
