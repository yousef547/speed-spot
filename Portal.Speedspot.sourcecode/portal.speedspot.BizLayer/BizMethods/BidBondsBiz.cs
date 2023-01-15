using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class BidBondsBiz : BizBaseT<BidBond>
    {
        public BidBondsBiz(IBidBondsRepository repository) : base(repository)
        {

        }

        public async Task<BidBond> GetByOpportunityId(int opportunityId)
        {
            return await Repository.GetFirstOrDefaultAsync(bb => bb.OpportunityId == opportunityId,
                x => x.AssignedTo, x => x.BidBondAttachment, x => x.BidBondOn_Bank);
        }

        public async override Task<BidBond> GetByIdAsync(int id)
        {
            return await Repository.GetFirstOrDefaultAsync(bb => bb.Id == id, x => x.Opportunity);
        }
    }
}
