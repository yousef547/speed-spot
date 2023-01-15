using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class PartnerEmployeesRepository : RepositoryBase<PartnerEmployee>, IPartnerEmployeesRepository
    {
        public PartnerEmployeesRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
