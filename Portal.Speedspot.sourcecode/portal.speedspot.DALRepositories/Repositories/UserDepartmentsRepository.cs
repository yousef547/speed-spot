using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class UserDepartmentsRepository : RepositoryBase<UserDepartment>, IUserDepartmentsRepository
    {
        public UserDepartmentsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
