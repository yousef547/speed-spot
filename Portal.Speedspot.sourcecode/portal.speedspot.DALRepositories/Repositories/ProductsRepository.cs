using Microsoft.EntityFrameworkCore;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class ProductsRepository : RepositoryBase<Product>, IProductsRepository
    {
        public ProductsRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IList<Product>> GetWithSuppliers(List<int> departmentIds, List<int> productIds)
        {
            return await dbSet.Where(p => !p.IsArchived && productIds.Contains(p.Id) && departmentIds.Contains(p.Category.DepartmentId))
                .Include(x => x.Category)
                .Include(x => x.Suppliers.Where(s => !s.Supplier.IsArchived)).ThenInclude(y => y.Supplier.Address.City.Country).ToListAsync();
        }
    }
}
