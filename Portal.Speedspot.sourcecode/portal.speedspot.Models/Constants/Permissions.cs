using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Constants
{
    public static class Permissions
    {
        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string ViewArchived = "Permissions.Users.View Archived";
            public const string Details = "Permissions.Users.Details";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
            public const string ManageRoles = "Permissions.Users.Manage Roles";
            public const string ManageDepartments = "Permissions.Users.Manage Departments";
            public const string ResetPassword = "Permissions.Users.Reset Password";
            public const string SendConfirmationEmail = "Permissions.Users.Send Confirmation Email";
        }

        public static class Roles
        {
            public const string View = "Permissions.Roles.View";
            public const string ViewArchived = "Permissions.Roles.View Archived";
            public const string Details = "Permissions.Roles.Details";
            public const string Create = "Permissions.Roles.Create";
            public const string Edit = "Permissions.Roles.Edit";
            public const string Delete = "Permissions.Roles.Delete";
            public const string ManagePermissions = "Permissions.Roles.Manage Permissions";
        }

        public static class Banks
        {
            public const string View = "Permissions.Banks.View";
            public const string ViewArchived = "Permissions.Banks.View Archived";
            public const string Details = "Permissions.Banks.Details";
            public const string Create = "Permissions.Banks.Create";
            public const string Edit = "Permissions.Banks.Edit";
            public const string Delete = "Permissions.Banks.Delete";
            public const string CreateBranch = "Permissions.Banks.Create Branch";
            public const string EditBranch = "Permissions.Banks.Edit Branch";
            public const string DeleteBranch = "Permissions.Banks.Delete Branch";
        }

        public static class Countries
        {
            public const string View = "Permissions.Countries.View";
            public const string ViewArchived = "Permissions.Countries.View Archived";
            public const string Details = "Permissions.Countries.Details";
            public const string Create = "Permissions.Countries.Create";
            public const string Edit = "Permissions.Countries.Edit";
            public const string Delete = "Permissions.Countries.Delete";
        }

        public static class Cities
        {
            public const string View = "Permissions.Cities.View";
            public const string ViewArchived = "Permissions.Cities.View Archived";
            public const string Details = "Permissions.Cities.Details";
            public const string Create = "Permissions.Cities.Create";
            public const string Edit = "Permissions.Cities.Edit";
            public const string Delete = "Permissions.Cities.Delete";
        }

        public static class Classifications
        {
            public const string View = "Permissions.Classifications.View";
            public const string ViewArchived = "Permissions.Classifications.View Archived";
            public const string Details = "Permissions.Classifications.Details";
            public const string Create = "Permissions.Classifications.Create";
            public const string Edit = "Permissions.Classifications.Edit";
            public const string Delete = "Permissions.Classifications.Delete";
        }

        public static class Customers
        {
            public const string View = "Permissions.Customers.View";
            public const string ViewArchived = "Permissions.Customers.View Archived";
            public const string Details = "Permissions.Customers.Details";
            public const string Create = "Permissions.Customers.Create";
            public const string Edit = "Permissions.Customers.Edit";
            public const string Delete = "Permissions.Customers.Delete";
            public const string BankDetails = "Permissions.Customers.Bank Details";
        }

        public static class Suppliers
        {
            public const string View = "Permissions.Suppliers.View";
            public const string ViewArchived = "Permissions.Suppliers.View Archived";
            public const string Details = "Permissions.Suppliers.Details";
            public const string ViewProducts = "Permissions.Suppliers.View Products";
            public const string Create = "Permissions.Suppliers.Create";
            public const string Edit = "Permissions.Suppliers.Edit";
            public const string Delete = "Permissions.Suppliers.Delete";
            public const string BankDetails = "Permissions.Suppliers.Bank Details";
        }

        public static class Competitors
        {
            public const string View = "Permissions.Competitors.View";
            public const string ViewArchived = "Permissions.Competitors.View Archived";
            public const string Details = "Permissions.Competitors.Details";
            public const string ViewProducts = "Permissions.Competitors.View Products";
            public const string Create = "Permissions.Competitors.Create";
            public const string Edit = "Permissions.Competitors.Edit";
            public const string Delete = "Permissions.Competitors.Delete";
            public const string BankDetails = "Permissions.Competitors.Bank Details";
        }
        public static class Logistics
        {
            public const string View = "Permissions.Logistics.View";
            public const string ViewArchived = "Permissions.Logistics.View Archived";
            public const string Details = "Permissions.Logistics.Details";
            public const string Create = "Permissions.Logistics.Create";
            public const string Edit = "Permissions.Logistics.Edit";
            public const string Delete = "Permissions.Logistics.Delete";
            public const string BankDetails = "Permissions.Logistics.Bank Details";
        }

        public static class Departments
        {
            public const string View = "Permissions.Departments.View";
            public const string ViewArchived = "Permissions.Departments.View Archived";
            public const string Details = "Permissions.Departments.Details";
            public const string Create = "Permissions.Departments.Create";
            public const string Edit = "Permissions.Departments.Edit";
            public const string Delete = "Permissions.Departments.Delete";
            public const string Settings = "Permissions.Departments.Settings";
        }

        public static class Opportunities
        {
            public const string View = "Permissions.Opportunities.View";
            public const string ViewArchived = "Permissions.Opportunities.View Archived";
            public const string Details = "Permissions.Opportunities.Details";
            public const string Create = "Permissions.Opportunities.Create";
            public const string Edit = "Permissions.Opportunities.Edit";
            public const string Delete = "Permissions.Opportunities.Delete";
            public const string ViewSupplierOffers = "Permissions.Opportunities.View Supplier Offers";
            public const string TechnicalApprove = "Permissions.Opportunities.Technical Approve";
            public const string ConvertToQuotation = "Permissions.Opportunities.Convert To Quotation";
            public const string SendBidBond = "Permissions.Opportunities.Send Bidbond";
            public const string ApproveBidBond = "Permissions.Opportunities.Approve Bidbond";
            public const string PrintBidBond = "Permissions.Opportunities.Print Bidbond";
            public const string ViewBidBondTotals = "Permissions.Opportunities.View Bidbond Totals";
            public const string SendBookTender = "Permissions.Opportunities.Send Book Tender";
            public const string ApproveBookTender = "Permissions.Opportunities.Approve Book Tender";
            public const string PrintBookTender = "Permissions.Opportunities.Print Book Tender";
            public const string SelectSuppliers = "Permissions.Opportunities.Select Suppliers";
            public const string AddSupplierOffers = "Permissions.Opportunities.Add Supplier Offers";
            public const string SuperEdit = "Permissions.Opportunities.Super Edit";
        }

        public static class Dashboard
        {
            public const string BusinessOverview = "Permissions.Dashboard.Business Overview";
        }

        public static class Quotations
        {
            public const string View = "Permissions.Quotations.View";
            public const string ViewArchived = "Permissions.Quotations.View Archived";
            public const string Details = "Permissions.Quotations.Details";
            public const string Edit = "Permissions.Quotations.Edit";
            public const string Delete = "Permissions.Quotations.Delete";
            public const string ConversationMessageOptions = "Permissions.Quotations.Conversation Message Options";
            public const string NegotiationMessageOptions = "Permissions.Quotations.Negotiation Message Options";
            public const string SendLG = "Permissions.Opportunities.Send LG";
            public const string ApproveLG = "Permissions.Opportunities.Approve LG";
            public const string PrintLG = "Permissions.Opportunities.Print LG";
            public const string ViewLGTotals = "Permissions.Opportunities.View LG Totals";

        }

        public static class ProductCategories
        {
            public const string View = "Permissions.ProductCategories.View";
            public const string ViewArchived = "Permissions.ProductCategories.View Archived";
            public const string Details = "Permissions.ProductCategories.Details";
            public const string Create = "Permissions.ProductCategories.Create";
            public const string Edit = "Permissions.ProductCategories.Edit";
            public const string Delete = "Permissions.ProductCategories.Delete";
        }

        public static class PurchasingOrders
        {
            public const string View = "Permissions.PurchasingOrders.View";
            public const string ViewArchived = "Permissions.PurchasingOrders.View Archived";
            public const string Details = "Permissions.PurchasingOrders.Details";
            public const string Create = "Permissions.PurchasingOrders.Create";
            public const string Edit = "Permissions.PurchasingOrders.Edit";
            public const string Delete = "Permissions.PurchasingOrders.Delete";
        }

        public static class Products
        {
            public const string View = "Permissions.Products.View";
            public const string ViewArchived = "Permissions.Products.View Archived";
            public const string Details = "Permissions.Products.Details";
            public const string Create = "Permissions.Products.Create";
            public const string Edit = "Permissions.Products.Edit";
            public const string Delete = "Permissions.Products.Delete";
        }

        public static class ProductsTree
        {
            public const string View = "Permissions.ProductsTree.View";
            public const string Create = "Permissions.ProductsTree.Create";
            public const string Details = "Permissions.ProductsTree.Details";
            public const string Edit = "Permissions.ProductsTree.Edit";
            public const string Delete = "Permissions.ProductsTree.Delete";
        }

        public static class AuditLogs
        {
            public const string View = "Permissions.AuditLogs.View";
        }

        public static class Archives
        {
            public const string View = "Permissions.Archives.View";
        }

        public static class Currencies
        {
            public const string View = "Permissions.Currencies.View";
            public const string ViewArchived = "Permissions.Currencies.View Archived";
            public const string Details = "Permissions.Currencies.Details";
            public const string Create = "Permissions.Currencies.Create";
            public const string Edit = "Permissions.Currencies.Edit";
            public const string Delete = "Permissions.Currencies.Delete";
        }

        public static class OrganizationTypes
        {
            public const string View = "Permissions.OrganizationTypes.View";
            public const string ViewArchived = "Permissions.OrganizationTypes.View Archived";
            public const string Details = "Permissions.OrganizationTypes.Details";
            public const string Create = "Permissions.OrganizationTypes.Create";
            public const string Edit = "Permissions.OrganizationTypes.Edit";
            public const string Delete = "Permissions.OrganizationTypes.Delete";
        }

        //public static class ProductOrigins
        //{
        //    public const string View = "Permissions.ProductOrigins.View";
        //    public const string ViewArchived = "Permissions.ProductOrigins.View Archived";
        //    public const string Details = "Permissions.ProductOrigins.Details";
        //    public const string Create = "Permissions.ProductOrigins.Create";
        //    public const string Edit = "Permissions.ProductOrigins.Edit";
        //    public const string Delete = "Permissions.ProductOrigins.Delete";
        //}

        public static class Visits
        {
            public const string View = "Permissions.Visits.View";
            public const string ViewArchived = "Permissions.Visits.View Archived";
            public const string Details = "Permissions.Visits.Details";
            public const string Create = "Permissions.Visits.Create";
            public const string Edit = "Permissions.Visits.Edit";
            public const string Delete = "Permissions.Visits.Delete";
        }

        public static class CompanyProfile
        {
            public const string View = "Permissions.CompanyProfile.View";
            public const string Edit = "Permissions.CompanyProfile.Edit";
        }

        public static class ChartOfAccounts
        {
            public const string View = "Permissions.ChartOfAccounts.View";
            public const string ViewArchived = "Permissions.ChartOfAccounts.View Archived";
            public const string Details = "Permissions.ChartOfAccounts.Details";
            public const string Create = "Permissions.ChartOfAccounts.Create";
            public const string Edit = "Permissions.ChartOfAccounts.Edit";
            public const string Report = "Permissions.ChartOfAccounts.Report";
            public const string Delete = "Permissions.ChartOfAccounts.Delete";
        }
        public static class Treasuries
        {
            public const string View = "Permissions.Treasuries.View";
            public const string ViewArchived = "Permissions.Treasuries.View Archived";
            public const string Details = "Permissions.Treasuries.Details";
            public const string Create = "Permissions.Treasuries.Create";
            public const string Edit = "Permissions.Treasuries.Edit";
            public const string Delete = "Permissions.Treasuries.Delete";
        }
        public static class JournalEntries
        {
            public const string View = "Permissions.JournalEntries.View";
            public const string ViewArchived = "Permissions.JournalEntries.View Archived";
            public const string Details = "Permissions.JournalEntries.Details";
            public const string Create = "Permissions.JournalEntries.Create";
            public const string Edit = "Permissions.JournalEntries.Edit";
            public const string Delete = "Permissions.JournalEntries.Delete";
        }


        public static class Projects
        {
            public const string View = "Permissions.Projects.View";
            public const string ViewArchived = "Permissions.Projects.View Archived";
            public const string Details = "Permissions.Projects.Details";
            public const string Create = "Permissions.Projects.Create";
            public const string Edit = "Permissions.Projects.Edit";
            public const string Delete = "Permissions.Projects.Delete";
        }

        public static class Payments
        {
            public const string View = "Permissions.Payments.View";
            public const string ViewArchived = "Permissions.Payments.View Archived";
            public const string Details = "Permissions.Payments.Details";
            public const string Create = "Permissions.Payments.Create";
            public const string Edit = "Permissions.Payments.Edit";
            public const string Delete = "Permissions.Payments.Delete";
        }

        public static class DeliveryPlaces
        {
            public const string View = "Permissions.DeliveryPlaces.View";
            public const string ViewArchived = "Permissions.DeliveryPlaces.View Archived";
            public const string Create = "Permissions.DeliveryPlaces.Create";
            public const string Edit = "Permissions.DeliveryPlaces.Edit";
            public const string Delete = "Permissions.DeliveryPlaces.Delete";
        }

        public static class OfferCertificates
        {
            public const string View = "Permissions.OfferCertificates.View";
            public const string ViewArchived = "Permissions.OfferCertificates.View Archived";
            public const string Create = "Permissions.OfferCertificates.Create";
            public const string Edit = "Permissions.OfferCertificates.Edit";
            public const string Delete = "Permissions.OfferCertificates.Delete";
        }

        public static class OfferValidities
        {
            public const string View = "Permissions.OfferValidities.View";
            public const string ViewArchived = "Permissions.OfferValidities.View Archived";
            public const string Create = "Permissions.OfferValidities.Create";
            public const string Edit = "Permissions.OfferValidities.Edit";
            public const string Delete = "Permissions.OfferValidities.Delete";
        }

        public static class OriginDocuments
        {
            public const string View = "Permissions.OriginDocuments.View";
            public const string ViewArchived = "Permissions.OriginDocuments.View Archived";
            public const string Create = "Permissions.OriginDocuments.Create";
            public const string Edit = "Permissions.OriginDocuments.Edit";
            public const string Delete = "Permissions.OriginDocuments.Delete";

        }

        public static class GuaranteeTerms
        {
            public const string View = "Permissions.GuaranteeTerms.View";
            public const string ViewArchived = "Permissions.GuaranteeTerms.View Archived";
            public const string Create = "Permissions.GuaranteeTerms.Create";
            public const string Edit = "Permissions.GuaranteeTerms.Edit";
            public const string Delete = "Permissions.GuaranteeTerms.Delete";

        }

        public static class VisitReasons
        {
            public const string View = "Permissions.VisitReasons.View";
            public const string ViewArchived = "Permissions.VisitReasons.View Archived";
            public const string Create = "Permissions.VisitReasons.Create";
            public const string Edit = "Permissions.VisitReasons.Edit";
            public const string Delete = "Permissions.VisitReasons.Delete";

        }

        public static class PaymentTypes
        {
            public const string View = "Permissions.PaymentTypes.View";
            public const string ViewArchived = "Permissions.PaymentTypes.View Archived";
            public const string Create = "Permissions.PaymentTypes.Create";
            public const string Edit = "Permissions.PaymentTypes.Edit";
            public const string Delete = "Permissions.PaymentTypes.Delete";
        }

        public static class VATValues
        {
            public const string View = "Permissions.VATValues.View";
            public const string ViewArchived = "Permissions.VATValues.View Archived";
            public const string Create = "Permissions.VATValues.Create";
            public const string Edit = "Permissions.VATValues.Edit";
            public const string Delete = "Permissions.VATValues.Delete";
        }

        public static class RejectReasons
        {
            public const string View = "Permissions.RejectReasons.View";
            public const string ViewArchived = "Permissions.RejectReasons.View Archived";
            public const string Create = "Permissions.RejectReasons.Create";
            public const string Edit = "Permissions.RejectReasons.Edit";
            public const string Delete = "Permissions.RejectReasons.Delete";
        }
    }
}
