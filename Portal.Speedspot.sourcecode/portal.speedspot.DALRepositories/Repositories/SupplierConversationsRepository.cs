using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class SupplierConversationsRepository : RepositoryBase<SupplierConversation>, ISupplierConversationsRepository
    {
        public SupplierConversationsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
