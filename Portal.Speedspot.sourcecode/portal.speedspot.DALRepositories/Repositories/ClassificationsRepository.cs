using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class ClassificationsRepository : RepositoryBase<Classification>, IClassificationsRepository
    {
        public ClassificationsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
