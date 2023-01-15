using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class ContactsRepository : RepositoryBase<Contact>, IContactsRepository
    {
        public ContactsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
