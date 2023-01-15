using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class BookTendersBiz : BizBaseT<BookTender>
    {
        public BookTendersBiz(IBookTendersRepository repository) : base(repository)
        {

        }

        public async Task<BookTender> GetByOpportunityId(int opportunityId)
        {
            return await Repository.GetFirstOrDefaultAsync(bb => bb.OpportunityId == opportunityId, x => x.AssignedTo, x => x.ReceiptAttachment);
        }
    }
}
