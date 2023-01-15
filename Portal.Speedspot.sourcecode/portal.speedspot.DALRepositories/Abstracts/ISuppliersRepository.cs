using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Abstracts
{
    public interface ISuppliersRepository : IRepository<Supplier>
    {
        Task<Supplier> GetFullItemById(int id);

        Task<IList<Supplier>> GetFullParents();

        Task<IList<Supplier>> GetSelectedWithProducts(List<int> supplierIds, List<int> productIds);

        Task<Supplier> GetByIdWithProducts(int supplierId, List<int> productIds);
    }
}
