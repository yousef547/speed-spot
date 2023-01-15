using Microsoft.EntityFrameworkCore;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class SupplierBanksRepository : RepositoryBase<SupplierBank>, ISupplierBanksRepository
    {
        public SupplierBanksRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IList<SupplierBank>> GetFullBySupplierIdAsync(int supplierId)
        {
            return await dbSet.Where(cb => cb.SupplierId == supplierId)
                .Include(x => x.Currencies).ThenInclude(y => y.Currency)
                .Include(x => x.Branch.Bank)
                .ToListAsync();
        }
    }
}
