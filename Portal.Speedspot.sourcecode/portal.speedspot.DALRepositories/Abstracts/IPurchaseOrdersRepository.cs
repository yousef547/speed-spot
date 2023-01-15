
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Abstracts
{
    public interface IPurchaseOrdersRepository : IRepository<PurchaseOrder>
    {
        Task<PurchaseOrder> GetFullItemByIdAsync(int id);
    }
}
