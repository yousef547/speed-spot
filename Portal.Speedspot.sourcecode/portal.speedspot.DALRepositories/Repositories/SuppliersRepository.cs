using Microsoft.EntityFrameworkCore;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class SuppliersRepository : RepositoryBase<Supplier>, ISuppliersRepository
    {
        public SuppliersRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<Supplier> GetFullItemById(int id)
        {
            var supplier = await dbSet.Where(s => s.Id == id)
                .Include(x => x.Address.City.Country)
                .Include(x => x.LogoAttachment)
                .Include(x => x.Classifications).ThenInclude(y => y.Classification)
                .Include(x => x.Departments).ThenInclude(y => y.Department)
                .Include(x => x.LegalInfo.Attachment)
                .Include(x => x.Contacts).ThenInclude(y => y.Contact.Type)
                .Include(x => x.Contacts).ThenInclude(y => y.Contact.PhoneCode)
                .Include(x => x.Employees).ThenInclude(y => y.Employee)
                .Include(x => x.Files).ThenInclude(y => y.Attachment)
                .Include(x => x.Certificates).ThenInclude(y => y.Certificate.Attachment)
                .Include(x => x.Products).ThenInclude(y => y.Product.Category.Department)
                .Include(x => x.ActivityType)
                .Include(x => x.OrganizationType)
                .Include(x => x.Parent)
                .Include(x => x.Childs)
                .Include(x => x.Banks).ThenInclude(y => y.Currencies).ThenInclude(z => z.Currency)
                .Include(x => x.Banks).ThenInclude(y => y.Branch.Bank)
                .Include(x => x.Competitor)
                .FirstOrDefaultAsync();

            return supplier;
        }

        public async Task<IList<Supplier>> GetFullParents()
        {
            return await dbSet.Where(c => c.ParentId == null)
                .Include(x => x.Childs).ThenInclude(y => y.Products).ThenInclude(z => z.Product)
                .Include(x => x.Childs).ThenInclude(y => y.Address).ThenInclude(z => z.City).ThenInclude(k => k.Country)
                .Include(x => x.Childs).ThenInclude(y => y.Competitor)
                .Include(x => x.Products).ThenInclude(x => x.Product)
                .Include(x => x.Address).ThenInclude(y => y.City).ThenInclude(z => z.Country)
                .Include(x => x.Departments)
                .Include(x => x.Competitor)
                .ToListAsync();
        }

        public async Task<IList<Supplier>> GetSelectedWithProducts(List<int> supplierIds, List<int> productIds)
        {
            var suppliers = await dbSet.Where(s => supplierIds.Contains(s.Id))
                .Include(x => x.Products.Where(p => productIds.Contains(p.ProductId))).ThenInclude(y => y.Product).ToListAsync();

            return suppliers;
        }

        public async Task<Supplier> GetByIdWithProducts(int supplierId, List<int> productIds)
        {
            var supplier = await dbSet.Where(s => s.Id == supplierId)
                .Include(x => x.Products.Where(p => productIds.Contains(p.ProductId))).ThenInclude(y => y.Product).FirstOrDefaultAsync();

            return supplier;
        }
    }
}
