using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Abstracts
{
    public interface ICompetitorBanksRepository : IRepository<CompetitorBank>
    {
        Task<IList<CompetitorBank>> GetFullByCompetitorIdAsync(int competitorId);
    }
}
