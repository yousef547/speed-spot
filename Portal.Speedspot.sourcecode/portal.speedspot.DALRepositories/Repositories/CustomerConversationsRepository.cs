using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class CustomerConversationsRepository : RepositoryBase<CustomerConversation>, ICustomerConversationsRepository
    {
        public CustomerConversationsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
