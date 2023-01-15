
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Abstracts
{
    public interface IAlternateVersionsRepository : IRepository<AlternateVersion>
    {
        Task<AlternateVersion> GetFullItemById(int id);
    }
}
