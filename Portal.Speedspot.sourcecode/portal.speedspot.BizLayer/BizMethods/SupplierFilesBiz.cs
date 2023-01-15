using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class SupplierFilesBiz : BizBaseT<SupplierFile>
    {
        public SupplierFilesBiz(ISupplierFilesRepository repository) : base(repository)
        {

        }

        public async Task<IList<SupplierFile>> GetBySupplierIdAsync(int supplierId)
        {
            var supplierFiles = await Repository.GetAllAsync(sf => sf.SupplierId == supplierId,
                x => x.Attachment);

            return supplierFiles;
        }
    }
}
