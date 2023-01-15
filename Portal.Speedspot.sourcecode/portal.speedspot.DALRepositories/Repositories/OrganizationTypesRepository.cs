using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class OrganizationTypesRepository : RepositoryBase<OrganizationType>, IOrganizationTypesRepository
    {
        public OrganizationTypesRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
