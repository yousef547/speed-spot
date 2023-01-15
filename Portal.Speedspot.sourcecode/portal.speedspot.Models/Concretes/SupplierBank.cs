using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class SupplierBank : EntityBase
    {
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public int BranchId { get; set; }
        public BankBranch Branch { get; set; }

        public IList<SupplierBankCurrency> Currencies { get; set; }
    }
}
