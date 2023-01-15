using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Abstracts
{
    public interface IQuotationsRepository : IRepository<Quotation>
    {
        Task<Quotation> GetFullItemById(int id);
    }
}
