using Microsoft.EntityFrameworkCore;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class SupplierOffersRepository : RepositoryBase<SupplierOffer>, ISupplierOffersRepository
    {
        public SupplierOffersRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<SupplierOffer> GetFullItemById(int id)
        {
            return await dbSet.Where(so => so.Id == id)
                .Include(x => x.Supplier)
                .Include(x => x.Products).ThenInclude(y => y.TechnicalSpecificationProduct.Product)
                .Include(x => x.Products).ThenInclude(y => y.TechnicalSpecificationProduct.RequestedOrigins).ThenInclude(z => z.Country)
                .Include(x => x.Products).ThenInclude(y => y.Attachment)
                .Include(x => x.Currency)
                .Include(x => x.Attachments).ThenInclude(y => y.Attachment)
                .FirstOrDefaultAsync();
        }

        public async Task<IList<SupplierOffer>> GetFullByOpportunityId(int opportunityId)
        {
            return await dbSet.Where(so => so.OpportunityId == opportunityId)
                .Include(x => x.Supplier.Address.City.Country)
                .Include(x => x.DeliveryType)
                .Include(x => x.PaymentType)
                .Include(x => x.Currency)
                .Include(x => x.Products).ThenInclude(y => y.TechnicalSpecificationProduct.RequestedOrigins).ThenInclude(z => z.Country)
                .ToListAsync();
        }
    

        public async Task<IList<SupplierOffer>> GetSupplierOfferAsync(int opportunityId, int supplierId)
        {
            return await dbSet.Where(ro => ro.OpportunityId == opportunityId && ro.SupplierId == supplierId)
                .Include(x=>x.Opportunity).ThenInclude(y=>y.Country)
                .Include(x=>x.PaymentType)
                .Include(x => x.DeliveryType).ToListAsync();

            //return requestOffer;
        }
    }
}
