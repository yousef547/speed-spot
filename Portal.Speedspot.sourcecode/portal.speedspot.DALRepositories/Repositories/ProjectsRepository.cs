using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class ProjectsRepository : RepositoryBase<Project>, IProjectsRepository
    {
        public ProjectsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
