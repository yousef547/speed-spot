using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class SupplierProductsBiz : BizBaseT<SupplierProduct>
    {
        public SupplierProductsBiz(ISupplierProductsRepository repository) : base(repository)
        {

        }

        public async Task<IList<SupplierProduct>> GetBySupplierIdAsync(int supplierId)
        {
            var products = await Repository.GetAllAsync(sp => sp.SupplierId == supplierId,
                x => x.Product.Category.Department);

            return products;
        }
    }
}
