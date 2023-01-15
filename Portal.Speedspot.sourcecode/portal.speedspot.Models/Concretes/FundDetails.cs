using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class FundDetails : EntityBase
    {
        public int PurchaseOrderId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        public int? BankId { get; set; }
        public Bank Bank { get; set; }
        public int? CurrencyId { get; set; }
        public Currency Currency { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Amount { get; set; }
        public int? FundPeriod { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? BankInterest { get; set; }
        public string ProjectNo { get; set; }
        public int? ContractFileId { get; set; }
        public Attachment ContractFile { get; set; }
        public int? CollectionFileId { get; set; }
        public Attachment CollectionFile { get; set; }
        public IList<SupplierPayment> SupplierPayments { get; set; }
    }
}
