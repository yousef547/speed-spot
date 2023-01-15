using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class QuotationStatusesBiz : BizBaseT<QuotationStatus>
    {
        public QuotationStatusesBiz(IQuotationStatusesRepository repository) : base(repository)
        {

        }

        public async Task<QuotationStatus> GetByEnumValue(int enumValue)
        {
            return await Repository.GetFirstAsync(s => s.EnumValue == enumValue);
        }
    }
}
