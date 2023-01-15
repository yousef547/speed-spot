using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class CustomerConversationsBiz : BizBaseT<CustomerConversation>
    {
        public CustomerConversationsBiz(ICustomerConversationsRepository repository) : base(repository)
        {
        }

        public async Task<List<CustomerConversation>> GetCustomerMessages(int quotationId, DateTime? date)
        {
            DateTime minDate = DateTime.MinValue;
            DateTime maxDate = DateTime.MaxValue;
            if (date.HasValue)
            {
                minDate = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, 0, 0, 0);
                maxDate = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, 23, 59, 59);
            }

            var customerConversations = await Repository.GetAllAsync(q => q.IsArchived == false &&
            q.QuotationId == quotationId && q.CreatedDate >= minDate && q.CreatedDate <= maxDate, c => c.Attachment);

            return customerConversations.OrderBy(x => x.CreatedDate).ToList();
        }
    }
}
