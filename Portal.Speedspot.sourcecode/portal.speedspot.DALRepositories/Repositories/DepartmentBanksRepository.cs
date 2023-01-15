using Microsoft.EntityFrameworkCore;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class DepartmentBanksRepository : RepositoryBase<DepartmentBank>, IDepartmentBanksRepository
    {
        public DepartmentBanksRepository(ApplicationDbContext context) : base(context)
        {

        }
        public async Task<IList<DepartmentBank>> GetFullByDepartmentIdAsync(int departmentId)
        {
            return await dbSet.Where(cb => cb.DepartmentId == departmentId)
                .Include(x => x.Currencies).ThenInclude(y => y.Currency)
                .Include(x => x.Branch.Bank)
                .ToListAsync();
        }
    }
}
