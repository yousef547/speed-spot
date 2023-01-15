using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class SupplierOffersBiz : BizBaseT<SupplierOffer>
    {
        private static ISupplierOffersRepository _repository;

        public SupplierOffersBiz(ISupplierOffersRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IList<SupplierOffer>> GetByOpportunityId(int opportunityId)
        {
            return await _repository.GetFullByOpportunityId(opportunityId);
        }

        public override async Task<SupplierOffer> GetByIdAsync(int id)
        {
            return await _repository.GetFullItemById(id);
        }

        public async Task<bool> ChangeAcceptance(int id, string userId)
        {
            SupplierOffer offer = await Repository.FindByIdAsync(id);
            offer.IsAccepted = !offer.IsAccepted;
            return await Repository.UpdateAsync(offer, userId);
        }

        public async Task<IList<SupplierOffer>> GetAcceptedOffers(int opportunityId)
        {
            return await Repository.GetAllAsync(so => so.OpportunityId == opportunityId && so.IsAccepted);
        }

        public async Task<IList<SupplierOffer>> GetSupplierOffersAsync(int opportunityId, int supplierId)
        {
            return await _repository.GetSupplierOfferAsync(opportunityId, supplierId);
        }
    }
}
