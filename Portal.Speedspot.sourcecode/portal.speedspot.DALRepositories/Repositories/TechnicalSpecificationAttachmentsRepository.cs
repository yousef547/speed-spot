using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class TechnicalSpecificationAttachmentsRepository : RepositoryBase<TechnicalSpecificationAttachment>, ITechnicalSpecificationAttachmentsRepository
    {
        public TechnicalSpecificationAttachmentsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
