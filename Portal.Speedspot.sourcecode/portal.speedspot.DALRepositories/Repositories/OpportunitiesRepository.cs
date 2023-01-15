using Microsoft.EntityFrameworkCore;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class OpportunitiesRepository : RepositoryBase<Opportunity>, IOpportunitiesRepository
    {
        public OpportunitiesRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<Opportunity> GetFullItemById(int id)
        {
            var opportunity = dbSet.Where(o => o.Id == id)
                .Include(x => x.Type)
                .Include(x => x.Customer)
                .Include(x => x.Country)
                .Include(x => x.Status)
                .Include(x => x.SalesAgent)
                .Include(x => x.Department)
                .Include(x => x.BidBond.BidBondAttachment.UploadedBy)
                .Include(x => x.BidBond.BidBondOn_Bank)
                .Include(x => x.BidBond.AssignedTo)
                .Include(x => x.BidBond.Request.RequestedBy)
                .Include(x => x.BidBond.Request.SentBy)
                .Include(x => x.BidBond.Request.StatusBy)
                .Include(x => x.BidBond.Request.PrintedBy)
                .Include(x => x.BookTender.ReceiptAttachment.UploadedBy)
                .Include(x => x.BookTender.AssignedTo)
                .Include(x => x.BookTender.Request.RequestedBy)
                .Include(x => x.BookTender.Request.SentBy)
                .Include(x => x.BookTender.Request.StatusBy)
                .Include(x => x.BookTender.Request.PrintedBy)
                .Include(x => x.TechnicalSpecification.Attachments).ThenInclude(z => z.Attachment.UploadedBy)
                .Include(x => x.TechnicalSpecification.Products).ThenInclude(z => z.Product.Category)
                .Include(x => x.TechnicalSpecification.Products).ThenInclude(z => z.Attachment)
                .Include(x => x.TechnicalSpecification.Products).ThenInclude(y => y.RequestedOrigins).ThenInclude(z => z.Country)
                .Include(x => x.RequestOffers).ThenInclude(y => y.Supplier.Address.City.Country)
                .Include(x => x.RequestOffers).ThenInclude(y => y.Supplier.LogoAttachment)
                .Include(x => x.RequestOffers).ThenInclude(y => y.Products).ThenInclude(z => z.Product.Product)
                .Include(x => x.SupplierOffers).ThenInclude(y => y.Currency)
                .Include(x => x.SupplierOffers).ThenInclude(y => y.DeliveryType)
                .Include(x => x.SupplierOffers).ThenInclude(y => y.PaymentType)
                .Include(x => x.SupplierOffers).ThenInclude(y => y.Supplier)
                .Include(x => x.SupplierOffers).ThenInclude(y => y.Products)
                .Include(x => x.SupplierOffers).ThenInclude(y => y.Attachments)
                .Include(x => x.OtherAttachments).ThenInclude(y => y.Attachment.UploadedBy)
                .Include(x => x.CreatedBy)
                .Include(x => x.ProjectManager)
                .Include(x => x.Guest);

            return await opportunity.FirstOrDefaultAsync();
        }

        public async Task<Opportunity> GetFullInfoById(int id)
        {
            var opportunity = await dbSet.Where(o => o.Id == id)
                .Include(x => x.TechnicalSpecification.Products).ThenInclude(y => y.RequestedOrigins).ThenInclude(z => z.Country)
                .FirstOrDefaultAsync();

            return opportunity;
        }
    }
}
