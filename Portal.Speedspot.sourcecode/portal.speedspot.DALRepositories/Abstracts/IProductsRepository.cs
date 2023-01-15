using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Abstracts
{
    public interface IProductsRepository : IRepository<Product>
    {
        Task<IList<Product>> GetWithSuppliers(List<int> departmentIds, List<int> productIds);
    }
}
