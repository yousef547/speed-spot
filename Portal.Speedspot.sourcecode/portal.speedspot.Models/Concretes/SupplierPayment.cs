using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class SupplierPayment : EntityBase
    {
        public int FundDetailsId { get; set; }
        public FundDetails FundDetails { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
        public int SupplierPoId { get; set; }
        public SupplierPo SupplierPo { get; set; }

    }
}
