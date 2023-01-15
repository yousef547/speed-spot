using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class QuotationCompetitorsBiz : BizBaseT<QuotationCompetitor>
    {
        public QuotationCompetitorsBiz(IQuotationCompetitorsRepository repository) : base(repository)
        {

        }

        public async Task<IList<QuotationCompetitor>> GetByQuotationId(int quotationId)
        {
            return await Repository.GetAllAsync(qc => qc.QuotationId == quotationId);
        }
    }
}
