
using Microsoft.EntityFrameworkCore;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class AlternateVersionsRepository : RepositoryBase<AlternateVersion>, IAlternateVersionsRepository
    {
        public AlternateVersionsRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<AlternateVersion> GetFullItemById(int id)
        {
            var alternateVersion = dbSet.Where(v => v.Id == id)
                .Include(x => x.Products).ThenInclude(y => y.Product.TechnicalSpecificationProduct)
                .Include(x => x.Products).ThenInclude(y => y.SelectedOrigins)
                .Include(x => x.Certificates)
                .AsSplitQuery();

            return await alternateVersion.FirstOrDefaultAsync();
        }
    }
}
