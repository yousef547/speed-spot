using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Concretes
{
    public class BankFees : EntityBase
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal BidBondMinFees { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal BidBondPercentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal BidBondCreditPercentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PerformanceLGMinFees { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PerformanceLGPercentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PerformanceLGCreditPercentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal FinalLGMinFees { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal FinalLGPercentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal FinalLGCreditPercentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ChequeCollectionMinFees { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ChequeCollectionPercentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ChequeCollectionCreditPercentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TransferMinFees { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TransferPercentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TransferCreditPercentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal LCMinFees { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal LCPercentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal LCCreditPercentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Form4MinFees { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Form4Percentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Form4CreditPercentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Form5MinFees { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Form5Percentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Form5CreditPercentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ForeignExchangeMinFees { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ForeignExchangePercentage { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ForeignExchangeCreditPercentage { get; set; }
        public int BankId { get; set; }
        public Bank Bank { get; set; }
    }
}
