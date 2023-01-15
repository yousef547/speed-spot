using Microsoft.EntityFrameworkCore;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class SupplierPosRepositery : RepositoryBase<SupplierPo>, ISupplierPoRepositery
    {
        public SupplierPosRepositery(ApplicationDbContext context):base(context)
        {
        }

        public async Task<SupplierPo> GetFullItemByIdAsync(int SupplierPoId)
        {
            var SupplierPo = dbSet.Where(t => t.SupplierId == SupplierPoId)
               .Include(x => x.Offers).ThenInclude(y => y.SupplierOffer)
               .Include(x => x.Supplier);
            return await SupplierPo.FirstOrDefaultAsync();
        }
    }
}
