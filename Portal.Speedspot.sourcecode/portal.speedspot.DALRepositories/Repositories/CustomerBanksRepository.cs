using Microsoft.EntityFrameworkCore;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class CustomerBanksRepository : RepositoryBase<CustomerBank>, ICustomerBanksRepository
    {
        public CustomerBanksRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IList<CustomerBank>> GetFullByCustomerIdAsync(int customerId)
        {
            return await dbSet.Where(cb => cb.CustomerId == customerId)
                .Include(x => x.Currencies).ThenInclude(y => y.Currency)
                .Include(x => x.Branch.Bank)
                .ToListAsync();
        }
    }
}
