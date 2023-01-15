using Microsoft.EntityFrameworkCore;

using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class QuotationOffersRepository : RepositoryBase<QuotationOffer>, IQuotationOffersRepository
    {
        public QuotationOffersRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IList<QuotationOffer>> GetFullItemsByQuotationId(int quotationId)
        {
            var offers = await dbSet.Where(o => o.QuotationId == quotationId)
                .Include(x => x.Versions).ThenInclude(y => y.CreatedBy)
                .Include(x => x.Versions).ThenInclude(y => y.Products).ThenInclude(z => z.Product.TechnicalSpecificationProduct)
                .Include(x => x.Versions).ThenInclude(y => y.Currency)
                .Include(x => x.Versions).ThenInclude(y => y.Validity)
                .Include(x => x.Versions).ThenInclude(y => y.GuaranteeTerm)
                .Include(x => x.Versions).ThenInclude(y => y.DeliveryPlace)
                .Include(x => x.Versions).ThenInclude(y => y.OriginDocument)
                .Include(x => x.Versions).ThenInclude(y => y.Certificates).ThenInclude(z => z.Certificate)
                .Include(x => x.Versions).ThenInclude(y => y.AlternateVersions).ThenInclude(z => z.Products).ThenInclude(z => z.Product.TechnicalSpecificationProduct)
                .Include(x => x.Versions).ThenInclude(y => y.AlternateVersions).ThenInclude(z => z.Products).ThenInclude(s=>s.SelectedOrigins)
                .Include(x => x.Versions).ThenInclude(y => y.Products).ThenInclude(z => z.SelectedOrigins)
                .ToListAsync();

            return offers;
        }

        public async Task<QuotationOffer> GetFullItemById(int id)
        {
            return await dbSet.Where(o => o.Id == id)
                .Include(x => x.Versions).ThenInclude(y => y.CreatedBy)
                .Include(x => x.Versions).ThenInclude(y => y.Currency)
                .Include(x => x.Versions).ThenInclude(y => y.Validity)
                .Include(x => x.Versions).ThenInclude(y => y.AlternateVersions)
                .Include(x => x.Versions).ThenInclude(y => y.DeliveryPlace)
                .Include(x => x.Versions).ThenInclude(y => y.OriginDocument)
                .Include(x => x.Versions).ThenInclude(y => y.Certificates).ThenInclude(z => z.Certificate)
                .Include(x => x.Versions).ThenInclude(y => y.Products).ThenInclude(z => z.SelectedOrigins)
                .FirstOrDefaultAsync();
        }
    }
}
