using Microsoft.EntityFrameworkCore;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class CompetitorOffersRepository : RepositoryBase<CompetitorOffer>, ICompetitorOffersRepository
    {
        public CompetitorOffersRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<CompetitorOffer> GetFullItemById(int id)
        {
            var offer = await dbSet.Where(o => o.Id == id)
                .Include(x => x.Competitor)
                .Include(x => x.Products).ThenInclude(y => y.Origins)
                .Include(x => x.Products).ThenInclude(y => y.Product.Product)
                .Include(x => x.Certificates)
                .FirstOrDefaultAsync();

            return offer;
        }

        public async Task<IList<CompetitorOffer>> GetFullByQuotationIdAsync(int quotationId)
        {
            var offers = await dbSet.Where(o => o.QuotationId == quotationId)
                .Include(x => x.Competitor.LogoAttachment)
                .Include(x => x.Certificates).ThenInclude(y => y.Certificate)
                .Include(x => x.Currency)
                .Include(x => x.DeliveryPlace)
                .Include(x => x.GuaranteeTerm)
                .Include(x => x.OriginDocument)
                .Include(x => x.Products).ThenInclude(y => y.Product.Product)
                .Include(x => x.Products).ThenInclude(y => y.Origins)
                .Include(x => x.Validity)
                .ToListAsync();

            return offers;
        }
    }
}
