using Microsoft.EntityFrameworkCore;

using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class LogisticsRepository : RepositoryBase<Logistic>, ILogisticsRepository
    {
        public LogisticsRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<Logistic> GetFullItemById(int id)
        {
            var logistic = await dbSet.Where(s => s.Id == id)
                .Include(x => x.Address.City.Country)
                .Include(x => x.LogoAttachment)
                .Include(x => x.Departments).ThenInclude(y => y.Department)
                .Include(x => x.LegalInfo.Attachment)
                .Include(x => x.Contacts).ThenInclude(y => y.Contact.Type)
                .Include(x => x.Contacts).ThenInclude(y => y.Contact.PhoneCode)
                .Include(x => x.Employees).ThenInclude(y => y.Employee)
                .Include(x => x.Files).ThenInclude(y => y.Attachment)
                .Include(x => x.ActivityType)
                .Include(x => x.OrganizationType)
                .Include(x => x.Parent)
                .Include(x => x.Childs)
                .Include(x => x.Banks).ThenInclude(y => y.Currencies).ThenInclude(z => z.Currency)
                .Include(x => x.Banks).ThenInclude(y => y.Branch.Bank)
                .FirstOrDefaultAsync();

            return logistic;
        }

        public async Task<IList<Logistic>> GetFullParents()
        {
            return await dbSet.Where(c => c.ParentId == null)
                .Include(x => x.Childs)
                .Include(x => x.Childs).ThenInclude(y => y.Address).ThenInclude(z => z.City).ThenInclude(k => k.Country)
                .Include(x => x.Address).ThenInclude(y => y.City).ThenInclude(z => z.Country)
                .Include(x => x.Departments)
                .Include(x => x.Banks).ThenInclude(y => y.Currencies).ThenInclude(z => z.Currency)
                .Include(x => x.Banks).ThenInclude(y => y.Branch.Bank)
                .Include(x => x.ActivityType)
                .Include(x => x.CreatedBy)
                .ToListAsync();
        }
    }
}
