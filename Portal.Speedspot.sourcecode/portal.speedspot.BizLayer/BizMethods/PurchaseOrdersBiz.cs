
using Microsoft.EntityFrameworkCore;

using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class PurchaseOrdersBiz : BizBaseT<PurchaseOrder>
    {
        private static IPurchaseOrdersRepository _repository;
        public PurchaseOrdersBiz(IPurchaseOrdersRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async override Task<PurchaseOrder> GetByIdAsync(int id)
        {
            return await _repository.GetFullItemByIdAsync(id);
        }

        public override async Task<IList<PurchaseOrder>> GetAllUnarchivedAsync()
        {
            return await Repository.DbContext.PurchaseOrders
                .Where(x => !x.IsArchived)
                .Include(y => y.CustomerPo)
                .Include(y => y.SupplierPos).ThenInclude(x => x.Supplier)
                .Include(y => y.Quotation.Opportunity.Department)
                .Include(y => y.Quotation.Opportunity.Customer)
                .Include(y => y.CreatedBy).ToListAsync();
        }

        public async Task<IList<PurchaseOrder>> GetCompletedAsync()
        {
            return await Repository.GetAllAsync(p => p.CustomerPo != null,
                x => x.CustomerPo,
                x => x.Quotation.Opportunity.Department,
                x => x.Quotation.Opportunity.Customer.LegalInfo);
        }

        public async Task<PurchaseOrder> GetByCustomerPONumber(string customerPONumber)
        {
            return await Repository.GetFirstOrDefaultAsync(p => p.CustomerPo.CustomerPONumber == customerPONumber,
                x => x.CustomerPo,
                x => x.Quotation.Opportunity.Department,
                x => x.Quotation.Opportunity.Customer.LegalInfo);
        }
    }
}
