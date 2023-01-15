using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Abstracts
{
    public interface IQuotationOfferVersionsRepository : IRepository<QuotationOfferVersion>
    {
        Task<QuotationOfferVersion> GetFullItemById(int id);
    }
}
