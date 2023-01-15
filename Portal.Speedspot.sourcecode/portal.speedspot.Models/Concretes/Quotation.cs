using portal.speedspot.Models.BasesAndAbstracts;
using portal.speedspot.Models.Concretes.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.Models.Concretes
{
    public class Quotation : EntityBase
    {
        public int OpportunityId { get; set; }
        public Opportunity Opportunity { get; set; }

        public int StatusId { get; set; }
        public QuotationStatus Status { get; set; }

        public DateTime DueDate { get; set; }

        [Required]
        public string AdminId { get; set; }
        public ApplicationUser Admin { get; set; }

        [Required]
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public QuotationTechnicalSessionStatus? TechnicalSessionStatus { get; set; }
        public DateTime? FinancialSessionDate { get; set; }

        public QuotationFinancialSessionStatus? FinancialSessionStatus { get; set; }

        public int? RejectReasonId { get; set; }
        public RejectReason RejectReason { get; set; }
        public string RejectLetterPath { get; set; }
        public DateTime? RejectLetterDate { get; set; }

        public string AcceptanceLetterPath { get; set; }
        public DateTime? AcceptanceLetterDate { get; set; }

        public string AwardingLetterPath { get; set; }
        public DateTime? AwardingLetterDate { get; set; }

        public LetterOfGuaranteeType? LGType { get; set; }

        public PerformanceLG PerformanceLG { get; set; }
        public FinalLG FinalLG { get; set; }

        public IList<QuotationCurrency> Currencies { get; set; }
        public IList<QuotationOffer> Offers { get; set; }
        public IList<CompetitorOffer> CompetitorOffers { get; set; }
        public IList<QuotationCompetitor> Competitors { get; set; }
        public IList<CustomerConversation> CustomerConversations { get; set; }
        public IList<SupplierConversation> SupplierConversations { get; set; }
        public IList<SupplierNegotiation> SupplierNegotiations { get; set; }
        public IList<CustomerNegotiation> CustomerNegotiations { get; set; }

        public NegotiationResult NegotiationResults { get; set; }
        public IList<OtherNegotiationResult> OtherNegotiationResults { get; set; }
    }
}
