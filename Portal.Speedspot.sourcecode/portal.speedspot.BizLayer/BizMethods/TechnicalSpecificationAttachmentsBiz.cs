using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class TechnicalSpecificationAttachmentsBiz : BizBaseT<TechnicalSpecificationAttachment>
    {
        public TechnicalSpecificationAttachmentsBiz(ITechnicalSpecificationAttachmentsRepository repository) : base(repository)
        {

        }

        public async Task<bool> DeleteByTechnicalSpecificationIdAsync(int technicalSpecificationId)
        {
            var attachments = await Repository.GetAllAsync(a => a.TechnicalSpecificationId == technicalSpecificationId);
            foreach (var attachment in attachments)
            {
                await Repository.DeleteAsync(attachment);
            }

            return true;
        }

        public async Task<IList<TechnicalSpecificationAttachment>> GetByTechnicalSpecificationIdAsync(int technicalSpecificationId)
        {
            return await Repository.GetAllAsync(a => a.TechnicalSpecificationId == technicalSpecificationId,
                x => x.Attachment);
        }
    }
}
