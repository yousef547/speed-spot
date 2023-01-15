using Microsoft.EntityFrameworkCore;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class TechnicalSpecificationProductsRepository : RepositoryBase<TechnicalSpecificationProduct>, ITechnicalSpecificationProductsRepository
    {
        public TechnicalSpecificationProductsRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<TechnicalSpecificationProduct> GetFullItemByIdAsync(int id)
        {
            return await dbSet.Where(p => p.Id == id)
                .Include(x => x.RequestedOrigins).ThenInclude(y => y.Country)
                .Include(x => x.Product)
                .FirstOrDefaultAsync();
        }
    }
}
