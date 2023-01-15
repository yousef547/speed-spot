using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Abstracts
{
    public interface IOpportunitiesRepository : IRepository<Opportunity>
    {
        Task<Opportunity> GetFullItemById(int id);

        Task<Opportunity> GetFullInfoById(int id);
    }
}
