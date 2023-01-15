
using Microsoft.EntityFrameworkCore;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class PurchaseOrdersRepository : RepositoryBase<PurchaseOrder>, IPurchaseOrdersRepository
    {
        public PurchaseOrdersRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<PurchaseOrder> GetFullItemByIdAsync(int id)
        {
            var PurchaseOrder = dbSet.Where(q => q.Id == id)
                .Include(x => x.Quotation.Opportunity.Customer)
                .Include(x => x.CustomerPo).ThenInclude(y=>y.Attachments).ThenInclude(z=>z.Attachment)
                .Include(x => x.SupplierPos).ThenInclude(y => y.Offers)
                .Include(x => x.FundDetails.Bank)
                .Include(x => x.FundDetails.Currency)
                .Include(x => x.FundDetails.CollectionFile)
                .Include(x => x.FundDetails.ContractFile)
                .Include(x => x.FundDetails.SupplierPayments).ThenInclude(y=>y.SupplierPo.Supplier)
                .Include(x => x.FundDetails.SupplierPayments).ThenInclude(z=>z.PaymentType)
                .Include(x => x.SelectedVersion.Products).ThenInclude(y => y.Product.TechnicalSpecificationProduct.Product.Category)
                .Include(x => x.SelectedVersion.Products).ThenInclude(y => y.Product.TechnicalSpecificationProduct.Attachment)
                .Include(x => x.SelectedVersion.Products).ThenInclude(y => y.SelectedOrigins).ThenInclude(z => z.Country)
                .Include(x => x.SelectedVersion.Certificates).ThenInclude(y => y.Certificate)
                .Include(x => x.SelectedVersion.DeliveryPlace)
                .Include(x => x.AcceptedOffers).ThenInclude(y => y.SupplierOffer.Supplier.LogoAttachment)
                .Include(x => x.AcceptedOffers).ThenInclude(y => y.SupplierOffer.DeliveryType)
                .Include(x => x.AcceptedOffers).ThenInclude(y => y.SupplierOffer.PaymentType)

                .Include(x => x.Quotation).ThenInclude(y => y.Opportunity.Department)

                .Include(x => x.AcceptedOffers).ThenInclude(y => y.SupplierOffer.Attachments).ThenInclude(z => z.Attachment);

            return await PurchaseOrder.FirstOrDefaultAsync();
        }
    }
}
