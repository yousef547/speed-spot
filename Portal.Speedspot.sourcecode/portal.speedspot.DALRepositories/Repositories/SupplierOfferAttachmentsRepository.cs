using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class SupplierOfferAttachmentsRepository : RepositoryBase<SupplierOfferAttachment>, ISupplierOfferAttachmentsRepository
    {
        public SupplierOfferAttachmentsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
