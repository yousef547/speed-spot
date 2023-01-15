using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class EmployeeTypesRepository : RepositoryBase<EmployeeType>, IEmployeeTypesRepository
    {
        public EmployeeTypesRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
