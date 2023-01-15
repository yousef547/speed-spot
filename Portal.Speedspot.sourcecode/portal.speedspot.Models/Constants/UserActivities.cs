using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.Models.Constants
{
    public static class UserActivities
    {
        public static class Opportunities
        {
            public const string DeleteTechSpecsAttachment = "Opportunities.DeleteTechSpecsAttachment";
            public const string UploadTechSpecsAttachment = "Opportunities.UploadTechSpecsAttachment";
            public const string DeleteTechSpecsProduct = "Opportunities.DeleteTechSpecsProduct";
            public const string UploadOfferFile = "Opportunities.UploadOfferFile";
            public const string AddRequestOffer = "Opportunities.AddRequestOffer";
            public const string RequestOfferEmailSent = "Opportunities.RequestOfferEmailSent";
            public const string DeleteRequestOffer = "Opportunities.DeleteRequestOffer";
            public const string AddSupplierOffer = "Opportunities.AddSupplierOffer";
            public const string DeleteOfferAttachment = "Opportunities.DeleteOfferAttachment";
            public const string UpdateSupplierOffer = "Opportunities.UpdateSupplierOffer";
            public const string DeleteSupplierOffer = "Opportunities.DeleteSupplierOffer";
            public const string DeleteAttachment = "Opportunities.DeleteAttachment";
            public const string Create = "Opportunities.Create";
            public const string Update = "Opportunities.Update";
            public const string TechnicalApprove = "Opportunities.TechnicalApprove";
            public const string ConvertToQuotation = "Opportunities.ConvertToQuotation";
            public const string Archive = "Opportunities.Archive";
            public const string Unarchive = "Opportunities.Unarchive";
            public const string ChangeOfferAcceptance = "Opportunities.ChangeOfferAcceptance";
            public const string CreateCustomer = "Opportunities.CreateCustomer";
            public const string CreateSupplier = "Opportunities.CreateSupplier";
        }

        public static class Quotations
        {
            public const string Archive = "Quotations.Archive";
            public const string Unarchive = "Quotations.Unarchive";
            public const string AddOffer = "Quotations.AddOffer";
            public const string AddOfferVersion = "Quotations.AddOfferVersion";
            public const string AddAlternateVersion = "Quotations.AddAlternateVersion";
            public const string UpdateOfferVersion = "Quotations.UpdateOfferVersion";
            public const string ChangeSelectedOfferVersion = "Quotations.ChangeSelectedOfferVersion";
            public const string AddCompetitorOffer = "Quotations.AddCompetitorOffer";
            public const string UpdateCompetitorOffer = "Quotations.UpdateCompetitorOffer";
            public const string AddSupplierConversation = "Quotations.AddSupplierConversation";
            public const string AddCustomerConversation = "Quotations.AddCustomerConversation";
            public const string UpdateSupplierConversation = "Quotations.UpdateSupplierConversation";
            public const string UpdateCustomerConversation = "Quotations.UpdateCustomerConversation";
            public const string ConvertToOpportunity = "Quotations.ConvertToOpportunity";
            public const string RejectTechnicalSession = "Quotations.RejectTechnicalSession";
            public const string AcceptTechnicalSession = "Quotations.AcceptTechnicalSession";
            public const string SaveFinancialDetails = "Quotations.SaveFinancialDetails";
            public const string UpdateNegotiationResults = "Quotations.UpdateNegotiationResults";
            public const string AddNegotiationResults = "Quotations.AddNegotiationResults";
            public const string AddSupplierNegotiation = "Quotations.AddSupplierNegotiation";
            public const string AddCustomerNegotiation = "Quotations.AddCustomerNegotiation";
            public const string UpdateSupplierNegotiation = "Quotations.UpdateSupplierNegotiation";
            public const string UpdateCustomerNegotiation = "Quotations.UpdateCustomerNegotiation";
            public const string RejectFinancialSession = "Quotations.RejectFinancialSession";
            public const string AcceptFinancialSession = "Quotations.AcceptFinancialSession";
            public const string SaveLGs = "Quotations.SaveLGs";
            public const string ConvertToPurchaseOrder = "Quotations.ConvertToPurchaseOrder";
        }

        public static class Visits
        {
            public const string Create = "Visits.Create";
            public const string Update = "Visits.Update";
            public const string Archive = "Visits.Archive";
            public const string Unarchive = "Visits.Unarchive";
        }
    }
}
