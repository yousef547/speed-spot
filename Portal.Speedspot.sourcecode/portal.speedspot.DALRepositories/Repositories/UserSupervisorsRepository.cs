using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class UserSupervisorsRepository : RepositoryBase<UserSupervisor>, IUserSupervisorsRepository
    {
        public UserSupervisorsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
