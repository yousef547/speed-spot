using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Abstracts
{
    public interface ISupplierOffersRepository : IRepository<SupplierOffer>
    {
        Task<SupplierOffer> GetFullItemById(int id);
        Task<IList<SupplierOffer>> GetFullByOpportunityId(int opportunityId);
        Task<IList<SupplierOffer>> GetSupplierOfferAsync(int opportunityId, int supplierId);

    }
}
