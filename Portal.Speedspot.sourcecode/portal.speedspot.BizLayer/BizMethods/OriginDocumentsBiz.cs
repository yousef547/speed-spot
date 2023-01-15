using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class OriginDocumentsBiz : BizBaseT<OriginDocument>
    {
        public OriginDocumentsBiz(IOriginDocumentsRepository repository) : base(repository)
        {

        }
    }
}
