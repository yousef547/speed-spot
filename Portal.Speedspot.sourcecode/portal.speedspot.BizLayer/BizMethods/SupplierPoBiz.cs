using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class SupplierPoBiz : BizBaseT<SupplierPo>
    {
        private static ISupplierPoRepositery _repository;

        public SupplierPoBiz(ISupplierPoRepositery repositery) : base(repositery)
        {
            _repository = repositery;
        }

        public async Task<IList<SupplierPo>> GetAllSupplierByPurchasingOrder(int PurchasingOrderId, int? SupplierPoId=null)
        {
            return await _repository.GetAllAsync(t => t.PurchaseOrderId == PurchasingOrderId && (SupplierPoId==null|| t.SupplierId == SupplierPoId),
                x=>x.Supplier);
        }
        public async Task<SupplierPo> GetDetailsSupplierPo(int SupplierPoId)
        {
            return await _repository.GetFullItemByIdAsync(SupplierPoId);
        }

        //public async Task<IList<SupplierPo>> SupplierPoIsExists(int SupplierPoId, int PurchasingOrderId)
        //{
        //    return await _repository.GetAllAsync(t => t.SupplierId == SupplierPoId, x => x.PurchaseOrderId == PurchasingOrderId);
        //}
    }
}
