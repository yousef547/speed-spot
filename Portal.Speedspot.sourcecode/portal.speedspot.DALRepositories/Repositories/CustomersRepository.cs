using Microsoft.EntityFrameworkCore;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class CustomersRepository : RepositoryBase<Customer>, ICustomersRepository
    {
        public CustomersRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<Customer> GetFullItemById(int id)
        {
            var customer = await dbSet.Where(c => c.Id == id)
                .Include(x => x.Departments).ThenInclude(y => y.Department)
                .Include(x => x.LogoAttachment)
                .Include(x => x.LegalInfo.Attachment)
                .Include(x => x.Address.City.Country)
                .Include(x => x.Contacts).ThenInclude(y => y.Contact.Type)
                .Include(x => x.Contacts).ThenInclude(y => y.Contact.PhoneCode)
                .Include(x => x.VendorRegistrations).ThenInclude(y => y.Attachment)
                .Include(x => x.Files).ThenInclude(y => y.Attachment)
                .Include(x => x.Employees).ThenInclude(y => y.Employee.PhoneCode)
                .Include(x => x.ActivityType)
                .Include(x => x.OrganizationType)
                .Include(x => x.Parent)
                .Include(x => x.Childs)
                .Include(x => x.SalesAgents).ThenInclude(x => x.SalesAgent)
                .Include(x => x.Banks).ThenInclude(y => y.Currencies).ThenInclude(z => z.Currency)
                .Include(x => x.Banks).ThenInclude(y => y.Branch.Bank)
                .Include(x => x.Visits).ThenInclude(y => y.Reason)
                .Include(x => x.Visits).ThenInclude(y => y.SalesAgent)
                .FirstOrDefaultAsync();

            return customer;
        }
    }
}
