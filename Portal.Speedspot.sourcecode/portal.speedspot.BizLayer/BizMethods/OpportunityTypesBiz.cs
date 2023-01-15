using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class OpportunityTypesBiz : BizBaseT<OpportunityType>
    {
        public OpportunityTypesBiz(IOpportunityTypesRepository repository) : base(repository)
        {

        }

        public async Task<OpportunityType> GetByEnumValue(int enumValue)
        {
            return await Repository.GetFirstAsync(s => s.EnumValue == enumValue);
        }
    }
}
