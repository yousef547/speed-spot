using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class RequestOffersBiz : BizBaseT<RequestOffer>
    {
        private readonly IRequestOffersRepository _repository;
        public RequestOffersBiz(IRequestOffersRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IList<RequestOffer>> GetByOpportunityId(int opportunityId)
        {
            return await _repository.GetByOpportunityId(opportunityId);
        }

        public async Task<RequestOffer> GetRequestOfferAsync(int opportunityId, int supplierId)
        {
            return await _repository.GetRequestOfferAsync(opportunityId, supplierId);
        }

        public async Task<RequestOffer> OfferEmailSent(int id, string userId)
        {
            RequestOffer offer = await _repository.GetFirstOrDefaultAsync(o => o.Id == id, x => x.Supplier);
            offer.IsEmailSent = true;
            await _repository.UpdateAsync(offer, userId);
            return offer;
        }
    }
}
