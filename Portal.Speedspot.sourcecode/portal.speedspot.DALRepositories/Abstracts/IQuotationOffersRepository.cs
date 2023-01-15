using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Abstracts
{
    public interface IQuotationOffersRepository : IRepository<QuotationOffer>
    {
        Task<IList<QuotationOffer>> GetFullItemsByQuotationId(int quotationId);
        Task<QuotationOffer> GetFullItemById(int id);
    }
}
