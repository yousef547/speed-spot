using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Abstracts
{
    public interface ILogisticBanksRepository : IRepository<LogisticBank>
    {
        Task<IList<LogisticBank>> GetFullByLogisticIdAsync(int logisticId);
    }
}
