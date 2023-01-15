using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.ViewModels
{
    public class SupplierBankViewModel
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int BranchId { get; set; }
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string BankNameAr { get; set; }
        public string BranchName { get; set; }
        public string BranchNameAr { get; set; }
        public string BranchAddress { get; set; }
        public string SwiftCode { get; set; }

        public IList<SupplierBankCurrencyViewModel> CurrencyVMs { get; set; }
    }

    public class SupplierBankCurrencyViewModel
    {
        public int Id { get; set; }
        public int SupplierBankId { get; set; }
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyNameAr { get; set; }
        public string AccountNo { get; set; }
        public string IBAN { get; set; }
    }
}
