using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class OtherNegotiationResultsBiz : BizBaseT<OtherNegotiationResult>
    {
        public OtherNegotiationResultsBiz(IOtherNegotiationResultsRepository repository) : base(repository)
        {

        }

        public async Task<bool> DeleteByQuotationIdAsync(int quotationId)
        {
            var otherNegotiationResults = await Repository.GetAllAsync(or => or.QuotationId == quotationId);
            if (otherNegotiationResults.Count > 0)
            {
                foreach (var or in otherNegotiationResults)
                {
                    await Repository.DeleteAsync(or);
                }
            }

            return true;
        }
    }
}
