using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class SupplierOfferAttachmentsBiz : BizBaseT<SupplierOfferAttachment>
    {
        public SupplierOfferAttachmentsBiz(ISupplierOfferAttachmentsRepository repository) : base(repository)
        {

        }

        public override Task<SupplierOfferAttachment> GetByIdAsync(int id)
        {
            return Repository.GetFirstOrDefaultAsync(o => o.Id == id,
                x => x.Attachment);
        }
    }
}
