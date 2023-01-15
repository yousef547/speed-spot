using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class DepartmentSettingsRepository : RepositoryBase<DepartmentSettings>, IDepartmentSettingsRepository
    {
        public DepartmentSettingsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
