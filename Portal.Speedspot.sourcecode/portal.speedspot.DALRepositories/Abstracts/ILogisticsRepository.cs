using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Abstracts
{
    public interface ILogisticsRepository : IRepository<Logistic>
    {
        public Task<Logistic> GetFullItemById(int id);
        public Task<IList<Logistic>> GetFullParents();
    }
}
