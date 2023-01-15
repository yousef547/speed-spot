using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Abstracts
{
    public interface ICompetitorOffersRepository : IRepository<CompetitorOffer>
    {
        Task<CompetitorOffer> GetFullItemById(int id);
        Task<IList<CompetitorOffer>> GetFullByQuotationIdAsync(int quotationId);
    }
}
