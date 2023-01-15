using portal.speedspot.Models.Helpers;
using portal.speedspot.Resources.Resources;

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace portal.speedspot.Models.Constants
{
    public class ApplicationEnums
    {
        public enum OpportunityStatusEnum
        {
            SalesInformation,
            TechnicalSpecifications,
            SupplierOffers
        }

        public enum EmployeeTypeEnum
        {
            Executive,
            SalesAgent,
            Manager,
            CEO,
            Accountant,
            OutsideCoordinator
        }

        public enum OpportunityTypeEnum
        {
            DirectOffer,
            Envelope,
            TwoEnvelopes,
            Auction,
            Contract
        }

        public enum FavouriteTypeEnum
        {
            Opportunities,
            Quotations,
            Customers,
            Suppliers,
            Competitors,
            Logistics,
            Department
        }

        public enum AuditType
        {
            None = 0,
            Create = 1,
            Update = 2,
            Delete = 3
        }

        public enum ContactTypeEnum
        {
            Mobile,
            Fax,
            Office
        }

        public enum RequestStatusEnum
        {
            Pending,
            Approved,
            Rejected
        }

        public enum PendingRequestSourceEnum
        {
            OpportunityBidBond,
            OpportunityBookTender,
            GeneralRequest,
            PerformanceLG,
            FinalLG
        }

        public enum PriorityLevelEnum
        {
            Critical,
            Important,
            Normal,
            Low
        }

        public enum QuotationStatusEnum
        {
            New,
            TechnicalSession,
            FinancialSession,
            Approved
        }

        public enum NotificationTypeEnum
        {
            Request_Approved,
            Request_New,
            Request_Rejected,
            Request_Pending,
            Task_New,
            Task_Done,
            Task_Pending,
            Task_Reminder,
            Request_Reminder,
            Opportunity_New
        }

        public enum QuotationTechnicalSessionStatus
        {
            Accepted,
            Rejected
        }

        public enum QuotationFinancialSessionStatus
        {
            Accepted,
            Rejected
        }

        public enum LetterOfGuaranteeType
        {
            NoLG,
            FinalLG,
            PerformanceAndFinalLG
        }

        public enum PurchaseOrderStatus
        {
            New,
            Completed
        }

        public enum AdvancePaymentType
        {
            Cheque,
            Transfer
        }

        public enum ProjectStatus
        {
            Opportunity,
            TechnicalSession,
            StoppedTechnicalSession,
            FinanialSession,
            StoppedFinanialSession,
            PerchasingOrder,
            Finished
        }

        public enum PaymentTypeEnum
        {
            Cash,
            Transfer,
            Cheque,
            LC
        }

        public enum JournalStatus
        {
            Draft,
            Posted
        }

        public enum TreasuryType
        {
            BankAccount,
            Cash
        }

        public enum TransactionType
        {
            Debit,
            Credit
        }

        public enum FundType
        {
            Funded,
            LightFunded,
            NotFunded
        }
        public enum SupplierPoType
        {
            Tender,
            Import,
            Inventory
        }

        public enum FinancialAccountType
        {
            Expense,
            Income
        }
    }
}
