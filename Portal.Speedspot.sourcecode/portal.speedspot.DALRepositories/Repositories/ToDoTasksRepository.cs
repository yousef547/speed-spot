using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class ToDoTasksRepository : RepositoryBase<ToDoTask>, IToDoTasksRepository
    {
        public ToDoTasksRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
