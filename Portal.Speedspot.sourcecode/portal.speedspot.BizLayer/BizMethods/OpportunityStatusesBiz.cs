using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class OpportunityStatusesBiz : BizBaseT<OpportunityStatus>
    {
        public OpportunityStatusesBiz(IOpportunityStatusesRepository repository) : base(repository)
        {

        }

        public async Task<OpportunityStatus> GetByEnumValue(int enumValue)
        {
            return await Repository.GetFirstAsync(s => s.EnumValue == enumValue);
        }
    }
}
