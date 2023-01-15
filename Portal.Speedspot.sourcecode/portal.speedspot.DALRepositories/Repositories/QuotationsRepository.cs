using Microsoft.EntityFrameworkCore;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class QuotationsRepository : RepositoryBase<Quotation>, IQuotationsRepository
    {
        public QuotationsRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<Quotation> GetFullItemById(int id)
        {
            var quotation = dbSet.Where(q => q.Id == id)
                .Include(x => x.Admin)
                .Include(x => x.Status)
                .Include(x => x.CreatedBy)
                .Include(x => x.RejectReason)
                .Include(x => x.Opportunity.Type)
                .Include(x => x.Opportunity.Customer.LogoAttachment)
                .Include(x => x.Opportunity.Country)
                .Include(x => x.Opportunity.Department)
                .Include(x => x.Opportunity.SupplierOffers).ThenInclude(y => y.Products).ThenInclude(z => z.TechnicalSpecificationProduct.Product)
                .Include(x => x.Opportunity.SupplierOffers).ThenInclude(y => y.Products)/*.ThenInclude(z => z.TechnicalSpecificationProduct.RequestedOrigins).ThenInclude(q => q.Country)*/
                .Include(x => x.Opportunity.SupplierOffers).ThenInclude(y => y.Currency)
                .Include(x => x.Opportunity.SupplierOffers).ThenInclude(y => y.Supplier.LogoAttachment)
                //.Include(x => x.Offers).ThenInclude(y => y.SupplierOffer.Supplier.LogoAttachment)
                .Include(x => x.Offers).ThenInclude(y => y.Versions).ThenInclude(z => z.Currency)
                .Include(x => x.Offers).ThenInclude(y => y.Versions).ThenInclude(z => z.DeliveryPlace)
                .Include(x => x.Offers).ThenInclude(y => y.Versions).ThenInclude(z => z.Validity)
                .Include(x => x.Offers).ThenInclude(y => y.Versions).ThenInclude(z => z.Products).ThenInclude(q => q.SelectedOrigins).ThenInclude(r => r.Country)
                .Include(x => x.Offers).ThenInclude(y => y.Versions).ThenInclude(z => z.GuaranteeTerm)
                .Include(x => x.Offers).ThenInclude(y => y.Versions).ThenInclude(z => z.OriginDocument)
                .Include(x => x.Offers).ThenInclude(y => y.Versions).ThenInclude(z => z.Certificates).ThenInclude(q => q.Certificate)
                .Include(x => x.Offers).ThenInclude(y => y.Versions).ThenInclude(z => z.AlternateVersions)
                .Include(x => x.CompetitorOffers).ThenInclude(y => y.Competitor.LogoAttachment)
                .Include(x => x.CompetitorOffers).ThenInclude(y => y.Certificates).ThenInclude(z => z.Certificate)
                .Include(x => x.CompetitorOffers).ThenInclude(y => y.Currency)
                .Include(x => x.CompetitorOffers).ThenInclude(y => y.DeliveryPlace)
                .Include(x => x.CompetitorOffers).ThenInclude(y => y.GuaranteeTerm)
                .Include(x => x.CompetitorOffers).ThenInclude(y => y.OriginDocument)
                .Include(x => x.CompetitorOffers).ThenInclude(y => y.Products).ThenInclude(z => z.Product)
                .Include(x => x.CompetitorOffers).ThenInclude(y => y.Products).ThenInclude(z => z.Origins).ThenInclude(q => q.Country)
                .Include(x => x.CompetitorOffers).ThenInclude(y => y.Validity)
                .Include(x => x.Competitors).ThenInclude(y => y.Competitor.LogoAttachment)
                .Include(x => x.NegotiationResults)
                .Include(x => x.OtherNegotiationResults)
                .Include(x => x.PerformanceLG.Request)
                .Include(x => x.PerformanceLG.Bank)
                .Include(x => x.PerformanceLG.BankBranch)
                .Include(x => x.PerformanceLG.AssignedTo)
                .Include(x => x.PerformanceLG.IssueBank)
                .Include(x => x.PerformanceLG.ReceiveBank)
                .Include(x => x.FinalLG.Request)
                .Include(x => x.FinalLG.Bank)
                .Include(x => x.FinalLG.BankBranch)
                .Include(x => x.FinalLG.AssignedTo)
                .AsSplitQuery();

            return await quotation.FirstOrDefaultAsync();
        }
    }
}
