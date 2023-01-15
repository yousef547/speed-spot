using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Abstracts
{
    public interface IDepartmentsRepository : IRepository<Department>
    {
        Task<Department> GetFullItemByIdAsync(int id);
    }
}
