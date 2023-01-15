using Microsoft.EntityFrameworkCore;

using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class QuotationOfferVersionsRepository : RepositoryBase<QuotationOfferVersion>, IQuotationOfferVersionsRepository
    {
        public QuotationOfferVersionsRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<QuotationOfferVersion> GetFullItemById(int id)
        {
            return await dbSet.Where(v => v.Id == id)
                .Include(x => x.Products).ThenInclude(y => y.Product.TechnicalSpecificationProduct.RequestedOrigins).ThenInclude(z => z.Country)
                .Include(x => x.Products).ThenInclude(y => y.SelectedOrigins).ThenInclude(z => z.Country)
                .Include(x => x.Validity)
                .Include(x => x.DeliveryPlace)
                .Include(x => x.Offer.Quotation)
                .Include(x => x.Certificates)
                .Include(x => x.GuaranteeTerm)
                .Include(X => X.Currency)
                .Include(x => x.AlternateVersions).ThenInclude(y => y.Products).ThenInclude(z => z.Product.TechnicalSpecificationProduct.RequestedOrigins).ThenInclude(q => q.Country)
                .Include(x => x.AlternateVersions).ThenInclude(y => y.Products).ThenInclude(z => z.SelectedOrigins).ThenInclude(q => q.Country)
                .Include(x => x.AlternateVersions).ThenInclude(y => y.Validity)
                .Include(x => x.AlternateVersions).ThenInclude(y => y.DeliveryPlace)
                .Include(x => x.AlternateVersions).ThenInclude(y => y.Certificates)
                .Include(x => x.AlternateVersions).ThenInclude(y => y.GuaranteeTerm)
                .Include(x => x.AlternateVersions).ThenInclude(y => y.Currency)
                .FirstOrDefaultAsync();
        }
    }
}
