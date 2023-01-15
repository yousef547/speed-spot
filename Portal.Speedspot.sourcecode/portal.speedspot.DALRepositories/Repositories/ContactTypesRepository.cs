using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class ContactTypesRepository : RepositoryBase<ContactType>, IContactTypesRepository
    {
        public ContactTypesRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
