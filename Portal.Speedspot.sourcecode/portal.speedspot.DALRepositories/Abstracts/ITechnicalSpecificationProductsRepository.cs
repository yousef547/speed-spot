using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Abstracts
{
    public interface ITechnicalSpecificationProductsRepository : IRepository<TechnicalSpecificationProduct>
    {
        public Task<TechnicalSpecificationProduct> GetFullItemByIdAsync(int id);
    }
}
