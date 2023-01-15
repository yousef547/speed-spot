using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Abstracts
{
    public interface ICustomersRepository : IRepository<Customer>
    {
        Task<Customer> GetFullItemById(int id);
    }
}
