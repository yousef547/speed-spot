using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class CustomerBank : EntityBase
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int BranchId { get; set; }
        public BankBranch Branch { get; set; }

        public IList<CustomerBankCurrency> Currencies { get; set; }
    }
}
