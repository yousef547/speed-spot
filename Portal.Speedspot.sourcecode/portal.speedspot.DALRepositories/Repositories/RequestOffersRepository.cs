using Microsoft.EntityFrameworkCore;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class RequestOffersRepository : RepositoryBase<RequestOffer>, IRequestOffersRepository
    {
        public RequestOffersRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<List<RequestOffer>> GetByOpportunityId(int opportunityId)
        {
            var requestOffers = await dbSet.Where(ro => ro.OpportunityId == opportunityId)
                .Include(x => x.Supplier.Address.City.Country)
                .Include(x => x.Supplier.LogoAttachment)
                .Include(x => x.Products).ThenInclude(x => x.Product.Product)
                .ToListAsync();

            return requestOffers;
        }

        public async Task<RequestOffer> GetRequestOfferAsync(int opportunityId, int supplierId)
        {
            var requestOffer = await dbSet.Where(ro => ro.OpportunityId == opportunityId && ro.SupplierId == supplierId)
                .Include(x => x.Products).ThenInclude(x => x.Product.Product)
                .Include(x => x.Products).ThenInclude(x => x.Product.RequestedOrigins).ThenInclude(z => z.Country)
                .FirstOrDefaultAsync();

            return requestOffer;
        }
    }
}
