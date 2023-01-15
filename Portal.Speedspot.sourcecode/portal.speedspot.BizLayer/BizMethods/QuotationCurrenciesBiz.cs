using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class QuotationCurrenciesBiz : BizBaseT<QuotationCurrency>
    {
        public QuotationCurrenciesBiz(IQuotationCurrenciesRepository repository) : base(repository)
        {

        }

        public async Task<IList<QuotationCurrency>> GetByQuotationIdAsync(int quotationId)
        {
            return await Repository.GetAllAsync(qc => qc.QuotationId == quotationId, x => x.Currency);
        }
    }
}
