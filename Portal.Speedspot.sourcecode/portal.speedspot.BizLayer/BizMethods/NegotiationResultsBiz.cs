using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class NegotiationResultsBiz : BizBaseT<NegotiationResult>
    {
        public NegotiationResultsBiz(INegotiationResultsRepository repository) : base(repository)
        {

        }

        public async Task<NegotiationResult> GetByQuotationIdAsync(int quotationId)
        {
            return await Repository.GetFirstOrDefaultAsync(nr => nr.QuotationId == quotationId);
        }
    }
}
