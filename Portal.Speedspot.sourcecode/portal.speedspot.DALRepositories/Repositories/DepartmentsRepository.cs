using Microsoft.EntityFrameworkCore;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class DepartmentsRepository : RepositoryBase<Department>, IDepartmentsRepository
    {
        public DepartmentsRepository(ApplicationDbContext context) : base(context)
        {

        }
        public async Task<Department> GetFullItemByIdAsync(int id)
        {
            return await dbSet.Where(d => d.Id == id)
               .Include(x => x.ManagingDirector)
               .Include(x => x.SalesDirector)
               .Include(x => x.DepartmentDocuments.Where(z => !z.IsArchived)).ThenInclude(y => y.Attachment)
               .Include(x => x.DepartmentBanks.Where(z => !z.IsArchived)).ThenInclude(y => y.Currencies).ThenInclude(z => z.Currency)
               .Include(x => x.DepartmentBanks.Where(z => !z.IsArchived)).ThenInclude(y => y.Branch.Bank)
               .FirstOrDefaultAsync();
        }

    }
}
