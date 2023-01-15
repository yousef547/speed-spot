using Microsoft.EntityFrameworkCore;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class CompetitorBanksRepository : RepositoryBase<CompetitorBank>, ICompetitorBanksRepository
    {
        public CompetitorBanksRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IList<CompetitorBank>> GetFullByCompetitorIdAsync(int competitorId)
        {
            return await dbSet.Where(cb => cb.CompetitorId == competitorId)
                .Include(x => x.Currencies).ThenInclude(y => y.Currency)
                .Include(x => x.Branch.Bank)
                .ToListAsync();
        }
    }
}
