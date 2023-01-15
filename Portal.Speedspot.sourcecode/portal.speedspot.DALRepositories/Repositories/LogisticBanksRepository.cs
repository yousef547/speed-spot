using Microsoft.EntityFrameworkCore;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class LogisticBanksRepository : RepositoryBase<LogisticBank>, ILogisticBanksRepository
    {
        public LogisticBanksRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IList<LogisticBank>> GetFullByLogisticIdAsync(int logisticId)
        {
            return await dbSet.Where(cb => cb.LogisticId == logisticId)
                .Include(x => x.Currencies).ThenInclude(y => y.Currency)
                .Include(x => x.Branch.Bank)
                .ToListAsync();
        }
    }
}
