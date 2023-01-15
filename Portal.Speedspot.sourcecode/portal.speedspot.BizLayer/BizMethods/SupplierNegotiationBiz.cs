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
    public class SupplierNegotiationBiz : BizBaseT<SupplierNegotiation>
    {
        public SupplierNegotiationBiz(ISupplierNegotiationsRepository repository) : base(repository)
        {
        }
        public async Task<List<SupplierNegotiation>> GetSupplierNegotiation(int supplierId, int quotationId, DateTime? date)
        {
            DateTime minDate = DateTime.MinValue;
            DateTime maxDate = DateTime.MaxValue;
            if (date.HasValue)
            {
                minDate = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, 0, 0, 0);
                maxDate = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, 23, 59, 59);
            }

            var supplierConversations = await Repository.GetAllAsync(q => q.IsArchived == false &&
            q.SupplierId == supplierId && q.QuotationId == quotationId && q.CreatedDate >= minDate && q.CreatedDate <= maxDate,
            x => x.Supplier.LogoAttachment, c => c.Attachment);
            return supplierConversations.OrderBy(x => x.CreatedDate).ToList();
        }
    }

}
