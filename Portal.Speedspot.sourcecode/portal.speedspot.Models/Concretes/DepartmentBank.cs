using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class DepartmentBank : EntityBase
    {
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int BranchId { get; set; }
        public BankBranch Branch { get; set; }
        public IList<DepartmentBankCurrency> Currencies { get; set; }
    }
}
