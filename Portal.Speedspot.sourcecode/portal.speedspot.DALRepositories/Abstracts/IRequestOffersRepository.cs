using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Abstracts
{
    public interface IRequestOffersRepository : IRepository<RequestOffer>
    {
        Task<List<RequestOffer>> GetByOpportunityId(int opportunityId);
        Task<RequestOffer> GetRequestOfferAsync(int opportunityId, int supplierId);
    }
}
