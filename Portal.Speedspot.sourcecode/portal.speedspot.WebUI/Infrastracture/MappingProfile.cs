using AutoMapper;

using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.Concretes.Identity;
using portal.speedspot.Models.ViewModels;

using System;
using System.Linq;

using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.WebUI.Infrastracture
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<PurchaseOrder, PurchaseOrderBasicDataViewModel>()
                .ForMember(x => x.DepartmentVM, y => y.MapFrom(z => z.Quotation.Opportunity.Department))
                .ForMember(x => x.CustomerName, y => y.MapFrom(z => z.Quotation.Opportunity.Customer.Name))
                .ForMember(x => x.CustomerNameAr, y => y.MapFrom(z => z.Quotation.Opportunity.Customer.NameAr))
                .ForMember(x => x.CustomerVatNo, y => y.MapFrom(z => z.Quotation.Opportunity.Customer.LegalInfo.VatNumber));

            CreateMap<FinancialAccountTransactionViewModel, FinancialAccountTransaction>()
                .ReverseMap()
                .ForMember(x => x.FinancialAccountVM, y => y.MapFrom(z => z.FinancialAccount))
                .ForMember(x => x.CurrencyVM, y => y.MapFrom(z => z.Currency));

            CreateMap<TreasuryTransactionViewModel, TreasuryTransaction>()
                .ReverseMap();

            CreateMap<DepartmentSettingsViewModel, DepartmentSettings>()
                .ReverseMap()
                .ForMember(x => x.DepartmentVM, y => y.MapFrom(z => z.Department));

            CreateMap<TreasuryViewModel, Treasury>()
                .ReverseMap()
                .ForMember(x => x.CurrencyVM, y => y.MapFrom(z => z.Currency))
                .ForMember(x => x.BankVM, y => y.MapFrom(z => z.Bank))
                .ForMember(x => x.TransactionVMs, y => y.MapFrom(z => z.Transactions));

            CreateMap<PaymentDetailsViewModel, PaymentDetails>()
                .ReverseMap()
                .ForMember(x => x.TreasuryVM, y => y.MapFrom(z => z.Treasury))
                .ForMember(x => x.ReceiveBankVM, y => y.MapFrom(z => z.ReceiveBank));

            CreateMap<FundDetailsPaymentViewModel, SupplierPayment>()
                .ReverseMap()
                .ForMember(x => x.SupplierPONumber, y => y.MapFrom(z => z.SupplierPo.SupplierPONumber))
                .ForMember(x => x.PaymentTypeName, y => y.MapFrom(z => z.PaymentType.Name))
                .ForMember(x => x.PaymentTypeNameAr, y => y.MapFrom(z => z.PaymentType.NameAr))
                .ForMember(x => x.SupplierName, y => y.MapFrom(z => z.SupplierPo.Supplier.Name))
                .ForMember(x => x.SupplierNameAr, y => y.MapFrom(z => z.SupplierPo.Supplier.NameAr));

            CreateMap<FundDetailsViewModel, FundDetails>()
                .ForMember(x => x.SupplierPayments, y => y.MapFrom(z => z.AllSupplierPayments))
                .ReverseMap()
                .ForMember(x => x.CollectionFilePath, y => y.MapFrom(z => z.CollectionFile.FilePath))
                .ForMember(x => x.ContractFilePath, y => y.MapFrom(z => z.ContractFile.FilePath))
                .ForMember(x => x.CollectionFileTitle, y => y.MapFrom(z => z.CollectionFile.Title))
                .ForMember(x => x.ContractFileTitle, y => y.MapFrom(z => z.ContractFile.Title))
                .ForMember(x => x.AllSupplierPayments, y => y.MapFrom(z => z.SupplierPayments))
                .ForMember(x => x.BankName, y => y.MapFrom(z => z.Bank.Name))
                .ForMember(x => x.BankNameAr, y => y.MapFrom(z => z.Bank.NameAr))
                .ForMember(x => x.CountryName, y => y.MapFrom(z => z.Currency.Name))
                .ForMember(x => x.CountryNameAr, y => y.MapFrom(z => z.Currency.NameAr));

            CreateMap<PaymentRequestDirectionViewModel, PaymentRequestDirection>()
                .ReverseMap()
                .ForMember(x => x.AccountName, y => y.MapFrom(z => z.Account.Name))
                .ForMember(x => x.AccountNameAr, y => y.MapFrom(z => z.Account.NameAr));

            CreateMap<PaymentRequestViewModel, PaymentRequest>()
                .ReverseMap()
                .ForMember(x => x.CreatedByUserName, y => y.MapFrom(z => z.CreatedByUser.Name))
                .ForMember(x => x.CreatedByUserProfilePicture, y => y.MapFrom(z => z.CreatedByUser.ProfilePicture))
                .ForMember(x => x.IsProject, y => y.MapFrom(z => z.ProjectId != null))
                .ForMember(x => x.ProjectName, y => y.MapFrom(z => z.Project != null ? z.Project.Name : null))
                .ForMember(x => x.CurrencySymbol, y => y.MapFrom(z => z.Currency.Symbol))
                .ForMember(x => x.ExchangeRate, y => y.MapFrom(z => z.Currency.ExchangeRate))
                .ForMember(x => x.CashAtachmentPath, y => y.MapFrom(z => z.CashAttachment.FilePath))
                .ForMember(x => x.CashAttachmentTitle, y => y.MapFrom(z => z.CashAttachment.Title))
                .ForMember(x => x.ReceiptAtachmentPath, y => y.MapFrom(z => z.ReceiptAttachment.FilePath))
                .ForMember(x => x.ReceiptAttachmentTitle, y => y.MapFrom(z => z.ReceiptAttachment.Title))
                .ForMember(x => x.TransferAtachmentPath, y => y.MapFrom(z => z.TransferAttachment.FilePath))
                .ForMember(x => x.TransferAttachmentTitle, y => y.MapFrom(z => z.TransferAttachment.Title))
                .ForMember(x => x.PaymentDetailsVM, y => y.MapFrom(z => z.PaymentDetails));

            CreateMap<SupplierPoOfferViewModel, SupplierPoOffer>()
                .ReverseMap()
                .ForMember(x => x.SupplierOfferVM, y => y.MapFrom(z => z.SupplierOffer));

            CreateMap<SupplierPOViewModel, SupplierPo>()
                .ForMember(x => x.Offers, y => y.MapFrom(z => z.SupplierPoOffers))
                .ReverseMap()
                .ForMember(x => x.StartDate, y => y.MapFrom(z => z.StartDate))
                .ForMember(x => x.SupplierPoOffers, y => y.MapFrom(z => z.Offers))
                .ForMember(x => x.Supplier, y => y.MapFrom(z => z.Supplier))

                .ForMember(x => x.PurchaseOrder, y => y.MapFrom(z => z.PurchaseOrder));

            CreateMap<CustomerPoAttachmentViewModel, CustomerPoAttachment>()
                .ReverseMap()
                .ForMember(x => x.AttachmentPath, y => y.MapFrom(z => z.Attachment.FilePath))
                .ForMember(x => x.TitleImage, y => y.MapFrom(z => z.Attachment.Title));

            CreateMap<CustomerPOViewModel, CustomerPo>()
                .ForMember(x => x.Attachments, y => y.MapFrom(z => z.PoAttachmentPath))
                .ReverseMap()
                .ForMember(x => x.StartDate, y => y.MapFrom(z => z.StartDate))
                .ForMember(x => x.PurchaseOrder, y => y.MapFrom(z => z.PurchaseOrder));


            CreateMap<AlternateVersionCertificateViewModel, AlternateVersionCertificate>()
                .ReverseMap();

            CreateMap<AlternateVersionProductOriginViewModel, AlternateVersionProductOrigin>()
                .ReverseMap()
                .ForMember(x => x.CountryName, y => y.MapFrom(z => z.Country.Name))
                .ForMember(x => x.CountryNameAr, y => y.MapFrom(z => z.Country.NameAr));

            CreateMap<AlternateVersionProductViewModel, AlternateVersionProduct>()
                .ForMember(x => x.SelectedOrigins, y => y.MapFrom(z => z.SelectedOriginVMs))
                .ReverseMap()
                .ForMember(x => x.SelectedOriginVMs, y => y.MapFrom(z => z.SelectedOrigins))
                .ForMember(x => x.ProductVM, y => y.MapFrom(z => z.Product))
                .ForMember(x => x.ProductName, y => y.MapFrom(z => z.Product.TechnicalSpecificationProduct.Product.Name))
                .ForMember(x => x.ProductNameAr, y => y.MapFrom(z => z.Product.TechnicalSpecificationProduct.Product.NameAr));

            CreateMap<AlternateVersionViewModel, AlternateVersion>()
                .ForMember(x => x.Products, y => y.MapFrom(z => z.ProductVMs))
                .ForMember(x => x.Certificates, y => y.MapFrom(z => z.CertificateVMs))
                .ReverseMap()
                .ForMember(x => x.CertificateVMs, y => y.MapFrom(z => z.Certificates))
                .ForMember(x => x.ProductVMs, y => y.MapFrom(z => z.Products))
                .ForMember(x => x.CurrencyName, y => y.MapFrom(z => z.Currency.Name))
                .ForMember(x => x.CurrencyNameAr, y => y.MapFrom(z => z.Currency.NameAr))
                .ForMember(x => x.CurrencySymbol, y => y.MapFrom(z => z.Currency.Symbol))
                .ForMember(x => x.ValidityName, y => y.MapFrom(z => z.Validity.Name))
                .ForMember(x => x.ValidityNameAr, y => y.MapFrom(z => z.Validity.NameAr))
                .ForMember(x => x.DeliveryPlaceName, y => y.MapFrom(z => z.DeliveryPlace.Name))
                .ForMember(x => x.DeliveryPlaceNameAr, y => y.MapFrom(z => z.DeliveryPlace.NameAr))
                .ForMember(x => x.OriginDocumentName, y => y.MapFrom(z => z.OriginDocument.Name))
                .ForMember(x => x.OriginDocumentNameAr, y => y.MapFrom(z => z.OriginDocument.NameAr))
                .ForMember(x => x.GuaranteeTermName, y => y.MapFrom(z => z.GuaranteeTerm.Name))
                .ForMember(x => x.GuaranteeTermNameAr, y => y.MapFrom(z => z.GuaranteeTerm.NameAr));

            CreateMap<UserActivityViewModel, UserActivity>()
                .ReverseMap()
                .ForMember(x => x.UserName, y => y.MapFrom(z => z.User.Name))
                .ForMember(x => x.UserProfilePicture, y => y.MapFrom(z => z.User.ProfilePicture))
                .ForMember(x => x.DepartmentName, y => y.MapFrom(z => z.User.Department.Name))
                .ForMember(x => x.DepartmentNameAr, y => y.MapFrom(z => z.User.Department.NameAr));

            CreateMap<ProjectViewModel, Project>()
                .ReverseMap()
                .ForMember(x => x.CustomerName, y => y.MapFrom(z => z.Customer.Name));

            CreateMap<PurchaseOrderSupplierOfferViewModel, PurchaseOrderSupplierOffer>()
                .ReverseMap()
                .ForMember(x => x.SupplierOfferVM, y => y.MapFrom(z => z.SupplierOffer));

            CreateMap<PurchaseOrderViewModel, PurchaseOrder>()
                .ForMember(x => x.FundDetails, y => y.MapFrom(z => z.FundDetailsVM))
                .ForMember(x => x.CustomerPo, y => y.MapFrom(z => z.CustomerPOVM))
                .ForMember(x => x.SupplierPos, y => y.MapFrom(z => z.SupplierPOVMs))
                .ReverseMap()
                .ForMember(x => x.QuotationNo, y => y.MapFrom(z => z.Quotation.Opportunity.TenderNumber))
                .ForMember(x => x.QuotationTitle, y => y.MapFrom(z => z.Quotation.Opportunity.Name))
                .ForMember(x => x.CustomerName, y => y.MapFrom(z => z.Quotation.Opportunity.Customer.Name))
                .ForMember(x => x.CustomerNameAr, y => y.MapFrom(z => z.Quotation.Opportunity.Customer.NameAr))
                .ForMember(x => x.CustomerVatNo, y => y.MapFrom(z => z.Quotation.Opportunity.Customer.LegalInfo.VatNumber))
                .ForMember(x => x.LastUpdatedByName, y => y.MapFrom(z => z.CreatedBy.Name))
                .ForMember(x => x.CustomerPOVM, y => y.MapFrom(z => z.CustomerPo))
                .ForMember(x => x.FundDetailsVM, y => y.MapFrom(z => z.FundDetails))
                .ForMember(x => x.AcceptedOfferVMs, y => y.MapFrom(z => z.AcceptedOffers))
                .ForMember(x => x.OpportunityId, y => y.MapFrom(z => z.Quotation.OpportunityId))
                .ForMember(x => x.SelectedVersionVM, y => y.MapFrom(z => z.SelectedVersion))
                .ForMember(x => x.CreatedDate, y => y.MapFrom(z => z.CreatedDate))
                .ForMember(x => x.DepartmentId, y => y.MapFrom(z => z.Quotation.Opportunity.Department.Id))
                .ForMember(x => x.DepartmentVM, y => y.MapFrom(z => z.Quotation.Opportunity.Department))
                .ForMember(x => x.SupplierPOVMs, y => y.MapFrom(z => z.SupplierPos));

            CreateMap<DepartmentDocumentViewModel, DepartmentDocument>()
                .ReverseMap()
                .ForMember(x => x.FilePath, y => y.MapFrom(z => z.Attachment.FilePath))
                .ForMember(x => x.TitleImage, y => y.MapFrom(z => z.Attachment.Title))
                .ForMember(x => x.IssueDate, y => y.MapFrom(z => z.IssueDate))
                .ForMember(x => x.ExpiryDate, y => y.MapFrom(z => z.ExpiryDate));

            CreateMap<PerformanceLGRequest, PendingRequestViewModel>()
                .ForMember(x => x.Controller, y => y.MapFrom(z => "Quotations"))
                .ForMember(x => x.ItemId, y => y.MapFrom(z => z.PerformanceLG.Quotation.Id))
                .ForMember(x => x.Source, y => y.MapFrom(z => PendingRequestSourceEnum.PerformanceLG))
                .ForMember(x => x.RequestedByName, y => y.MapFrom(z => z.RequestedBy.Name))
                .ForMember(x => x.RequestedByProfilePicture, y => y.MapFrom(z => z.RequestedBy.ProfilePicture))
                .ForMember(x => x.SentByName, y => y.MapFrom(z => z.SentBy != null ? z.SentBy.Name : null))
                .ForMember(x => x.SentByProfilePicture, y => y.MapFrom(z => z.SentBy != null ? z.SentBy.ProfilePicture : null))
                .ForMember(x => x.StatusByName, y => y.MapFrom(z => z.StatusBy != null ? z.StatusBy.Name : null))
                .ForMember(x => x.StatusByProfilePicture, y => y.MapFrom(z => z.StatusBy != null ? z.StatusBy.ProfilePicture : null))
                .ForMember(x => x.Deadline, y => y.MapFrom(z => z.PerformanceLG.Quotation.Opportunity.DueDate))
                .ForMember(x => x.AssignedToId, y => y.MapFrom(z => z.PerformanceLG.AssignedToId))
                .ForMember(x => x.AssignedToName, y => y.MapFrom(z => z.PerformanceLG.AssignedTo.Name))
                .ForMember(x => x.AssignedToProfilePicture, y => y.MapFrom(z => z.PerformanceLG.AssignedTo.ProfilePicture));

            CreateMap<FinalLGRequest, PendingRequestViewModel>()
                .ForMember(x => x.Controller, y => y.MapFrom(z => "Quotations"))
                .ForMember(x => x.ItemId, y => y.MapFrom(z => z.FinalLG.Quotation.Id))
                .ForMember(x => x.Source, y => y.MapFrom(z => PendingRequestSourceEnum.FinalLG))
                .ForMember(x => x.RequestedByName, y => y.MapFrom(z => z.RequestedBy.Name))
                .ForMember(x => x.RequestedByProfilePicture, y => y.MapFrom(z => z.RequestedBy.ProfilePicture))
                .ForMember(x => x.SentByName, y => y.MapFrom(z => z.SentBy != null ? z.SentBy.Name : null))
                .ForMember(x => x.SentByProfilePicture, y => y.MapFrom(z => z.SentBy != null ? z.SentBy.ProfilePicture : null))
                .ForMember(x => x.StatusByName, y => y.MapFrom(z => z.StatusBy != null ? z.StatusBy.Name : null))
                .ForMember(x => x.StatusByProfilePicture, y => y.MapFrom(z => z.StatusBy != null ? z.StatusBy.ProfilePicture : null))
                .ForMember(x => x.Deadline, y => y.MapFrom(z => z.FinalLG.Quotation.Opportunity.DueDate))
                .ForMember(x => x.AssignedToId, y => y.MapFrom(z => z.FinalLG.AssignedToId))
                .ForMember(x => x.AssignedToName, y => y.MapFrom(z => z.FinalLG.AssignedTo.Name))
                .ForMember(x => x.AssignedToProfilePicture, y => y.MapFrom(z => z.FinalLG.AssignedTo.ProfilePicture));

            CreateMap<DepartmentBankCurrencyViewModel, DepartmentBankCurrency>()
                .ReverseMap()
                .ForMember(x => x.CurrencyName, y => y.MapFrom(z => z.Currency.Name))
                .ForMember(x => x.CurrencyNameAr, y => y.MapFrom(z => z.Currency.NameAr))
                .ForMember(x => x.CurrencySymbol, y => y.MapFrom(z => z.Currency.Symbol));

            CreateMap<DepartmentBankViewModel, DepartmentBank>()
                .ForMember(x => x.Currencies, y => y.MapFrom(z => z.CurrencyVMs))
                .ReverseMap()
                .ForMember(x => x.CurrencyVMs, y => y.MapFrom(z => z.Currencies))
                .ForMember(x => x.BankId, y => y.MapFrom(z => z.Branch.BankId))
                .ForMember(x => x.BankName, y => y.MapFrom(z => z.Branch.Bank.Name))
                .ForMember(x => x.BankNameAr, y => y.MapFrom(z => z.Branch.Bank.NameAr))
                .ForMember(x => x.BranchName, y => y.MapFrom(z => z.Branch.Name))
                .ForMember(x => x.BranchNameAr, y => y.MapFrom(z => z.Branch.NameAr))
                .ForMember(x => x.BranchAddress, y => y.MapFrom(z => z.Branch.Address))
                .ForMember(x => x.SwiftCode, y => y.MapFrom(z => z.Branch.SwiftCode));

            CreateMap<LetterOfGuaranteeRequestViewModel, PerformanceLGRequest>()
                .ReverseMap()
                .ForMember(x => x.CustomerName, y => y.MapFrom(z => z.PerformanceLG.Quotation.Opportunity.Customer.Name))
                .ForMember(x => x.CustomerNameAr, y => y.MapFrom(z => z.PerformanceLG.Quotation.Opportunity.Customer.NameAr))
                .ForMember(x => x.LGVM, y => y.MapFrom(z => z.PerformanceLG));

            CreateMap<LetterOfGuaranteeRequestViewModel, FinalLGRequest>()
                .ReverseMap()
                .ForMember(x => x.CustomerName, y => y.MapFrom(z => z.FinalLG.Quotation.Opportunity.Customer.Name))
                .ForMember(x => x.CustomerNameAr, y => y.MapFrom(z => z.FinalLG.Quotation.Opportunity.Customer.NameAr))
                .ForMember(x => x.LGVM, y => y.MapFrom(z => z.FinalLG));

            CreateMap<LetterOfGuaranteeViewModel, PerformanceLG>()
                .ReverseMap()
                .ForMember(x => x.RequestVM, y => y.MapFrom(z => z.Request))
                .ForMember(x => x.IsRequested, y => y.MapFrom(z => z.Request != null))
                .ForMember(x => x.IsSent, y => y.MapFrom(z => z.Request != null && z.Request.IsSent))
                .ForMember(x => x.IsApproved, y => y.MapFrom(z => z.Request != null && z.Request.Status == RequestStatusEnum.Approved))
                .ForMember(x => x.IsRejected, y => y.MapFrom(z => z.Request != null && z.Request.Status == RequestStatusEnum.Rejected))
                .ForMember(x => x.IsPrinted, y => y.MapFrom(z => z.Request != null && z.Request.IsPrinted))
                .ForMember(x => x.AssignedToName, y => y.MapFrom(z => z.AssignedTo.Name))
                .ForMember(x => x.BankName, y => y.MapFrom(z => z.Bank.Name))
                .ForMember(x => x.BankNameAr, y => y.MapFrom(z => z.Bank.NameAr))
                .ForMember(x => x.BankBranchName, y => y.MapFrom(z => z.BankBranch.Name))
                .ForMember(x => x.BankBranchNameAr, y => y.MapFrom(z => z.BankBranch.NameAr))
                .ForMember(x => x.IssueBankName, y => y.MapFrom(z => z.IssueBank.Name))
                .ForMember(x => x.IssueBankNameAr, y => y.MapFrom(z => z.IssueBank.NameAr))
                .ForMember(x => x.ReceiveBankName, y => y.MapFrom(z => z.ReceiveBank.Name))
                .ForMember(x => x.ReceiveBankNameAr, y => y.MapFrom(z => z.ReceiveBank.NameAr));

            CreateMap<LetterOfGuaranteeViewModel, FinalLG>()
                .ReverseMap()
                .ForMember(x => x.RequestVM, y => y.MapFrom(z => z.Request))
                .ForMember(x => x.IsRequested, y => y.MapFrom(z => z.Request != null))
                .ForMember(x => x.IsSent, y => y.MapFrom(z => z.Request != null && z.Request.IsSent))
                .ForMember(x => x.IsApproved, y => y.MapFrom(z => z.Request != null && z.Request.Status == RequestStatusEnum.Approved))
                .ForMember(x => x.IsRejected, y => y.MapFrom(z => z.Request != null && z.Request.Status == RequestStatusEnum.Rejected))
                .ForMember(x => x.IsPrinted, y => y.MapFrom(z => z.Request != null && z.Request.IsPrinted))
                .ForMember(x => x.AssignedToName, y => y.MapFrom(z => z.AssignedTo.Name))
                .ForMember(x => x.BankName, y => y.MapFrom(z => z.Bank.Name))
                .ForMember(x => x.BankNameAr, y => y.MapFrom(z => z.Bank.NameAr));

            CreateMap<CustomerNegotiationViewModel, CustomerNegotiation>()
               .ReverseMap()
                .ForMember(x => x.FilePath, y => y.MapFrom(z => z.Attachment.FilePath))
                .ForMember(x => x.TitleImage, y => y.MapFrom(z => z.Attachment.Title));

            CreateMap<NegotiationResultViewModel, NegotiationResult>()
                .ReverseMap();

            CreateMap<OtherNegotiationResultViewModel, OtherNegotiationResult>()
                .ReverseMap();

            CreateMap<SupplierNegotiationViewModel, SupplierNegotiation>()
                .ReverseMap()
                .ForMember(x => x.SupplierLogoPath, y => y.MapFrom(z => z.Supplier.LogoAttachment.FilePath))
                .ForMember(x => x.FilePath, y => y.MapFrom(z => z.Attachment.FilePath))
                .ForMember(x => x.TitleImage, y => y.MapFrom(z => z.Attachment.Title));

            CreateMap<StickyNoteViewModel, StickyNote>()
                .ReverseMap()
                .ForMember(x => x.CreatedDate, y => y.MapFrom(z => z.CreatedDate));

            CreateMap<QuotationCompetitorViewModel, QuotationCompetitor>()
                .ReverseMap()
                .ForMember(x => x.CompetitorName, y => y.MapFrom(z => z.Competitor.Name))
                .ForMember(x => x.CompetitorNameAr, y => y.MapFrom(z => z.Competitor.NameAr))
                .ForMember(x => x.CompetitorImagePath, y => y.MapFrom(z => z.Competitor.LogoAttachment.FilePath));

            CreateMap<RejectReasonViewModel, RejectReason>()
                .ReverseMap();

            CreateMap<CreateSupplierViewModel, Supplier>()
              .ForMember(x => x.Departments, y => y.MapFrom(z => z.DepartmentVMs))
              .ForMember(x => x.Products, y => y.MapFrom(z => z.ProductVMs))
              .ReverseMap();

            CreateMap<CreateCustomerViewModel, Customer>()
                .ForMember(x => x.Departments, y => y.MapFrom(z => z.DepartmentVMs))
                .ReverseMap();

            CreateMap<VATValueViewModel, VATValue>()
                .ReverseMap()
                .ForMember(x => x.DisplayText, y => y.MapFrom(z => z.Title + " - " + z.Value.ToString()))
                .ForMember(x => x.DisplayValue, y => y.MapFrom(z => z.Value.ToString() + "%"));

            CreateMap<PaymentTypeViewModel, PaymentType>()
                .ReverseMap();

            CreateMap<VisitReasonViewModel, VisitReason>()
                .ReverseMap();

            CreateMap<CustomerConversationViewModel, CustomerConversation>()
               .ReverseMap()
                .ForMember(x => x.FilePath, y => y.MapFrom(z => z.Attachment.FilePath))
                .ForMember(x => x.TitleImage, y => y.MapFrom(z => z.Attachment.Title));

            CreateMap<SupplierConversationViewModel, SupplierConversation>()
                .ReverseMap()
                .ForMember(x => x.SupplierLogoPath, y => y.MapFrom(z => z.Supplier.LogoAttachment.FilePath))
                .ForMember(x => x.FilePath, y => y.MapFrom(z => z.Attachment.FilePath))
                .ForMember(x => x.TitleImage, y => y.MapFrom(z => z.Attachment.Title));

            CreateMap<CompetitorOfferProductOriginViewModel, CompetitorOfferProductOrigin>()
                .ReverseMap()
                .ForMember(x => x.CountryName, y => y.MapFrom(z => z.Country.Name))
                .ForMember(x => x.CountryNameAr, y => y.MapFrom(z => z.Country.NameAr));

            CreateMap<CompetitorOfferProductViewModel, CompetitorOfferProduct>()
                .ForMember(x => x.Origins, y => y.MapFrom(z => z.OriginVMs))
                .ReverseMap()
                .ForMember(x => x.OriginVMs, y => y.MapFrom(z => z.Origins))
                .ForMember(x => x.ProductVM, y => y.MapFrom(z => z.Product))
                .ForMember(x => x.Quantity, y => y.MapFrom(z => z.Product.Quantity));

            CreateMap<CompetitorOfferCertificateViewModel, CompetitorOfferCertificate>()
                .ReverseMap()
                .ForMember(x => x.CertificateName, y => y.MapFrom(z => z.Certificate.Name))
                .ForMember(x => x.CertificateNameAr, y => y.MapFrom(z => z.Certificate.NameAr));

            CreateMap<CompetitorOfferViewModel, CompetitorOffer>()
                .ForMember(x => x.Certificates, y => y.MapFrom(z => z.CertificateVMs))
                .ForMember(x => x.Products, y => y.MapFrom(z => z.ProductVMs))
                .ReverseMap()
                .ForMember(x => x.CompetitorName, y => y.MapFrom(z => z.Competitor.Name))
                .ForMember(x => x.CompetitorNameAr, y => y.MapFrom(z => z.Competitor.NameAr))
                .ForMember(x => x.CompetitorImagePath, y => y.MapFrom(z => z.Competitor.LogoAttachment.FilePath))
                .ForMember(x => x.CurrencyName, y => y.MapFrom(z => z.Currency.Name))
                .ForMember(x => x.CurrencyNameAr, y => y.MapFrom(z => z.Currency.NameAr))
                .ForMember(x => x.CurrencySymbol, y => y.MapFrom(z => z.Currency.Symbol))
                .ForMember(x => x.DeliveryPlaceName, y => y.MapFrom(z => z.DeliveryPlace.Name))
                .ForMember(x => x.DeliveryPlaceNameAr, y => y.MapFrom(z => z.DeliveryPlace.NameAr))
                .ForMember(x => x.GuaranteeTermName, y => y.MapFrom(z => z.GuaranteeTerm.Name))
                .ForMember(x => x.GuaranteeTermNameAr, y => y.MapFrom(z => z.GuaranteeTerm.NameAr))
                .ForMember(x => x.OriginDocumentName, y => y.MapFrom(z => z.OriginDocument.Name))
                .ForMember(x => x.OriginDocumentNameAr, y => y.MapFrom(z => z.OriginDocument.NameAr))
                .ForMember(x => x.ValidityName, y => y.MapFrom(z => z.Validity.Name))
                .ForMember(x => x.ValidityNameAr, y => y.MapFrom(z => z.Validity.NameAr))
                .ForMember(x => x.CertificateVMs, y => y.MapFrom(z => z.Certificates))
                .ForMember(x => x.ProductVMs, y => y.MapFrom(z => z.Products));

            CreateMap<FavouriteViewModel, Favourite>()
                .ReverseMap()
                .ForMember(x => x.FavouriteName, y => y.MapFrom(z => z.Type.Name))
                .ForMember(x => x.FavouriteEnum, y => y.MapFrom(z => z.Type.EnumValue));

            CreateMap<NotificationViewModel, Notification>()
                .ForMember(x => x.NotificationUsers, y => y.MapFrom(z => z.NotificationUserVMs))
                .ReverseMap()
                .ForMember(x => x.TypeStr, y => y.MapFrom(z => z.Type.ToString()));

            CreateMap<NotificationUserViewModel, NotificationUser>()
                .ReverseMap()
                .ForMember(x => x.CreatedByName, y => y.MapFrom(z => z.Notification.CreatedBy.Name))
                .ForMember(x => x.SendToName, y => y.MapFrom(z => z.SendTo.Name))
                .ForMember(x => x.Description, y => y.MapFrom(z => z.Notification.Details))
                .ForMember(x => x.NotificationType, y => y.MapFrom(z => z.Notification.Type))
                .ForMember(x => x.CreatedDate, y => y.MapFrom(z => z.Notification.CreatedDate));

            CreateMap<GuaranteeTermViewModel, GuaranteeTerm>()
                .ReverseMap();

            CreateMap<GeneralRequestViewModel, GeneralRequest>()
                .ReverseMap()
                .ForMember(x => x.RequestedByName, y => y.MapFrom(z => z.RequestedBy.Name))
                .ForMember(x => x.RequestFromName, y => y.MapFrom(z => z.RequestFrom.Name));

            CreateMap<DeliveryPlaceViewModel, DeliveryPlace>()
                .ReverseMap();

            CreateMap<OfferCertificateViewModel, OfferCertificate>()
                .ReverseMap();

            CreateMap<OfferValiditiesViewModel, OfferValidity>()
                .ReverseMap();

            CreateMap<OriginDocumentViewModel, OriginDocument>()
                .ReverseMap();

            CreateMap<EditProfileViewModel, ApplicationUser>()
                .ReverseMap()
                .ForMember(x => x.DefaultDepartmentName, y => y.MapFrom(z => z.Department.Name))
                .ForMember(x => x.DefaultDepartmentNameAr, y => y.MapFrom(z => z.Department.NameAr))
                .ForMember(x => x.EmployeeTypeName, y => y.MapFrom(z => z.EmployeeType.Name))
                .ForMember(x => x.EmployeeTypeName, y => y.MapFrom(z => z.EmployeeType.NameAr))
                .ForMember(x => x.DirectManagerName, y => y.MapFrom(z => z.DirectManager.Name))
                .ForMember(x => x.EmailAddress, y => y.MapFrom(z => z.Email))
                .ForMember(x => x.DepartmentVMs, y => y.MapFrom(z => z.UserDepartments));

            CreateMap<BidBondRequest, PendingRequestViewModel>()
                .ForMember(x => x.Controller, y => y.MapFrom(z => "Opportunities"))
                .ForMember(x => x.ItemId, y => y.MapFrom(z => z.BidBond.OpportunityId))
                .ForMember(x => x.Source, y => y.MapFrom(z => PendingRequestSourceEnum.OpportunityBidBond))
                .ForMember(x => x.RequestedByName, y => y.MapFrom(z => z.RequestedBy.Name))
                .ForMember(x => x.RequestedByProfilePicture, y => y.MapFrom(z => z.RequestedBy.ProfilePicture))
                .ForMember(x => x.SentByName, y => y.MapFrom(z => z.SentBy != null ? z.SentBy.Name : null))
                .ForMember(x => x.SentByProfilePicture, y => y.MapFrom(z => z.SentBy != null ? z.SentBy.ProfilePicture : null))
                .ForMember(x => x.StatusByName, y => y.MapFrom(z => z.StatusBy != null ? z.StatusBy.Name : null))
                .ForMember(x => x.StatusByProfilePicture, y => y.MapFrom(z => z.StatusBy != null ? z.StatusBy.ProfilePicture : null))
                .ForMember(x => x.Deadline, y => y.MapFrom(z => z.BidBond.Opportunity.DueDate))
                .ForMember(x => x.AssignedToId, y => y.MapFrom(z => z.BidBond.AssignedToId))
                .ForMember(x => x.AssignedToName, y => y.MapFrom(z => z.BidBond.AssignedTo.Name))
                .ForMember(x => x.AssignedToProfilePicture, y => y.MapFrom(z => z.BidBond.AssignedTo.ProfilePicture));

            CreateMap<BookTenderRequest, PendingRequestViewModel>()
               .ForMember(x => x.Controller, y => y.MapFrom(z => "Opportunities"))
               .ForMember(x => x.ItemId, y => y.MapFrom(z => z.BookTender.OpportunityId))
               .ForMember(x => x.Source, y => y.MapFrom(z => PendingRequestSourceEnum.OpportunityBookTender))
               .ForMember(x => x.RequestedByName, y => y.MapFrom(z => z.RequestedBy.Name))
               .ForMember(x => x.RequestedByProfilePicture, y => y.MapFrom(z => z.RequestedBy.ProfilePicture))
               .ForMember(x => x.SentByName, y => y.MapFrom(z => z.SentBy != null ? z.SentBy.Name : null))
               .ForMember(x => x.SentByProfilePicture, y => y.MapFrom(z => z.SentBy != null ? z.SentBy.ProfilePicture : null))
               .ForMember(x => x.StatusByName, y => y.MapFrom(z => z.StatusBy != null ? z.StatusBy.Name : null))
               .ForMember(x => x.StatusByProfilePicture, y => y.MapFrom(z => z.StatusBy != null ? z.StatusBy.ProfilePicture : null))
               .ForMember(x => x.Deadline, y => y.MapFrom(z => z.BookTender.Opportunity.DueDate))
               .ForMember(x => x.AssignedToId, y => y.MapFrom(z => z.BookTender.AssignedToId))
               .ForMember(x => x.AssignedToName, y => y.MapFrom(z => z.BookTender.AssignedTo.Name))
               .ForMember(x => x.AssignedToProfilePicture, y => y.MapFrom(z => z.BookTender.AssignedTo.ProfilePicture));

            CreateMap<GeneralRequest, PendingRequestViewModel>()
               .ForMember(x => x.IsSent, y => y.MapFrom(y => false))
               .ForMember(x => x.RequestedByProfilePicture, y => y.MapFrom(z => z.RequestedBy.ProfilePicture))
               .ForMember(x => x.RequestedByName, y => y.MapFrom(z => z.RequestedBy.Name))
               .ForMember(x => x.Source, y => y.MapFrom(z => PendingRequestSourceEnum.GeneralRequest))
               .ForMember(x => x.AssignedToId, y => y.MapFrom(z => z.RequestFromId))
               .ForMember(x => x.AssignedToName, y => y.MapFrom(z => z.RequestFrom.Name))
               .ForMember(x => x.AssignedToProfilePicture, y => y.MapFrom(z => z.RequestFrom.ProfilePicture))
               .ForMember(x => x.StatusByName, y => y.MapFrom(z => z.RequestFrom.Name))
               .ForMember(x => x.StatusByProfilePicture, y => y.MapFrom(z => z.RequestFrom.ProfilePicture));

            CreateMap<FinancialAccountViewModel, FinancialAccount>()
                .ReverseMap()
                .ForMember(x => x.DepartmentCode, y => y.MapFrom(z => z.Department.Code))
                .ForMember(x => x.ParentVM, y => y.MapFrom(z => z.Parent))
                .ForMember(x => x.ChildVMs, y => y.MapFrom(z => z.Childs));

            CreateMap<TechnicalSpecificationProductOriginViewModel, TechnicalSpecificationProductOrigin>()
                .ReverseMap()
                .ForMember(x => x.CountryName, y => y.MapFrom(z => z.Country.Name))
                .ForMember(x => x.CountryNameAr, y => y.MapFrom(z => z.Country.NameAr));

            CreateMap<CompanyDataViewModel, CompanyData>()
                .ReverseMap();

            CreateMap<ProductOriginViewModel, ProductOrigin>()
                .ReverseMap();

            CreateMap<OrganizationTypeViewModel, OrganizationType>()
                .ReverseMap();

            CreateMap<CurrencyViewModel, Currency>()
                .ReverseMap();

            CreateMap<ClassificationViewModel, Classification>()
                .ReverseMap();

            CreateMap<ContactViewModel, Contact>()
                .ReverseMap()
                .ForMember(x => x.TypeName, y => y.MapFrom(z => z.Type.Name))
                .ForMember(x => x.TypeNameAr, y => y.MapFrom(z => z.Type.NameAr))
                .ForMember(x => x.CountryCode2, y => y.MapFrom(z => z.PhoneCode.Code2))
                .ForMember(x => x.CountryPhoneCode, y => y.MapFrom(z => z.PhoneCode.PhoneCode));

            CreateMap<LegalInfoViewModel, LegalInfo>()
                .ReverseMap()
                .ForMember(x => x.AttachmentPath, y => y.MapFrom(z => z.Attachment.FilePath))
                .ForMember(x => x.AttachmentTitle, y => y.MapFrom(z => z.Attachment.Title));

            CreateMap<AddressViewModel, Address>()
                .ReverseMap()
                .ForMember(x => x.CityName, y => y.MapFrom(z => z.City.Name))
                .ForMember(x => x.CityNameAr, y => y.MapFrom(z => z.City.NameAr))
                .ForMember(x => x.CountryId, y => y.MapFrom(z => z.City.CountryId))
                .ForMember(x => x.CountryName, y => y.MapFrom(z => z.City.Country.Name))
                .ForMember(x => x.CountryNameAr, y => y.MapFrom(z => z.City.Country.NameAr));

            CreateMap<PartnerEmployeeViewModel, PartnerEmployee>()
                .ReverseMap()
                .ForMember(x => x.CountryCode2, y => y.MapFrom(z => z.PhoneCode.Code2))
                .ForMember(x => x.PhoneCode, y => y.MapFrom(z => z.PhoneCode.PhoneCode));

            CreateMap<DepartmentViewModel, Department>()
                .ReverseMap()
                .ForMember(x => x.ManagingDirectorName, y => y.MapFrom(z => z.ManagingDirector.Name))
                .ForMember(x => x.ManagingDirectorPhone, y => y.MapFrom(z => z.ManagingDirector.PhoneNumber))
                .ForMember(x => x.ManagingDirectorEmail, y => y.MapFrom(z => z.ManagingDirector.Email))
                .ForMember(x => x.SalesDirectorName, y => y.MapFrom(z => z.SalesDirector.Name))
                .ForMember(x => x.SalesDirectorPhone, y => y.MapFrom(z => z.SalesDirector.PhoneNumber))
                .ForMember(x => x.SalesDirectorEmail, y => y.MapFrom(z => z.SalesDirector.Email))
                .ForMember(x => x.OpportunitiesVMs, y => y.MapFrom(z => z.Opportunities))
                .ForMember(x => x.DepartmentDocumentVMs, y => y.MapFrom(z => z.DepartmentDocuments))
                .ForMember(x => x.DepartmentBankVMs, y => y.MapFrom(z => z.DepartmentBanks));

            CreateMap<CountryViewModel, Country>()
                .ReverseMap();

            CreateMap<CityViewModel, City>()
                .ReverseMap()
                .ForMember(x => x.CountryName, y => y.MapFrom(z => z.Country.Name))
                .ForMember(x => x.CountryNameAr, y => y.MapFrom(z => z.Country.NameAr));

            CreateMap<BankBranchViewModel, BankBranch>()
                .ReverseMap();

            CreateMap<BankViewModel, Bank>()
                .ForMember(x => x.Branches, y => y.MapFrom(z => z.BranchVMs))
                .ReverseMap()
                .ForMember(x => x.CountryName, y => y.MapFrom(z => z.Country.Name))
                .ForMember(x => x.CountryNameAr, y => y.MapFrom(z => z.Country.NameAr))
                .ForMember(x => x.CountryVM, y => y.MapFrom(z => z.Country))
                .ForMember(x => x.BranchVMs, y => y.MapFrom(z => z.Branches))
                .ForMember(x => x.BidBondMinFees, y => y.MapFrom(z => z.BankFees.BidBondMinFees))
                .ForMember(x => x.BidBondPercentage, y => y.MapFrom(z => z.BankFees.BidBondPercentage))
                .ForMember(x => x.BidBondCreditPercentage, y => y.MapFrom(z => z.BankFees.BidBondCreditPercentage))
                .ForMember(x => x.PerformanceLGMinFees, y => y.MapFrom(z => z.BankFees.PerformanceLGMinFees))
                .ForMember(x => x.PerformanceLGPercentage, y => y.MapFrom(z => z.BankFees.PerformanceLGPercentage))
                .ForMember(x => x.PerformanceLGCreditPercentage, y => y.MapFrom(z => z.BankFees.PerformanceLGCreditPercentage))
                .ForMember(x => x.FinalLGMinFees, y => y.MapFrom(z => z.BankFees.FinalLGMinFees))
                .ForMember(x => x.FinalLGPercentage, y => y.MapFrom(z => z.BankFees.FinalLGPercentage))
                .ForMember(x => x.FinalLGCreditPercentage, y => y.MapFrom(z => z.BankFees.FinalLGCreditPercentage))
                .ForMember(x => x.ChequeCollectionMinFees, y => y.MapFrom(z => z.BankFees.ChequeCollectionMinFees))
                .ForMember(x => x.ChequeCollectionPercentage, y => y.MapFrom(z => z.BankFees.ChequeCollectionPercentage))
                .ForMember(x => x.ChequeCollectionCreditPercentage, y => y.MapFrom(z => z.BankFees.ChequeCollectionCreditPercentage))
                .ForMember(x => x.TransferMinFees, y => y.MapFrom(z => z.BankFees.TransferMinFees))
                .ForMember(x => x.TransferPercentage, y => y.MapFrom(z => z.BankFees.TransferPercentage))
                .ForMember(x => x.TransferCreditPercentage, y => y.MapFrom(z => z.BankFees.TransferCreditPercentage))
                .ForMember(x => x.LCMinFees, y => y.MapFrom(z => z.BankFees.LCMinFees))
                .ForMember(x => x.LCPercentage, y => y.MapFrom(z => z.BankFees.LCPercentage))
                .ForMember(x => x.LCCreditPercentage, y => y.MapFrom(z => z.BankFees.LCCreditPercentage))
                .ForMember(x => x.Form4MinFees, y => y.MapFrom(z => z.BankFees.Form4MinFees))
                .ForMember(x => x.Form4Percentage, y => y.MapFrom(z => z.BankFees.Form4Percentage))
                .ForMember(x => x.Form4CreditPercentage, y => y.MapFrom(z => z.BankFees.Form4CreditPercentage))
                .ForMember(x => x.Form5MinFees, y => y.MapFrom(z => z.BankFees.Form5MinFees))
                .ForMember(x => x.Form5Percentage, y => y.MapFrom(z => z.BankFees.Form5Percentage))
                .ForMember(x => x.Form5CreditPercentage, y => y.MapFrom(z => z.BankFees.Form5CreditPercentage))
                .ForMember(x => x.ForginExchangeMinFees, y => y.MapFrom(z => z.BankFees.ForeignExchangeMinFees))
                .ForMember(x => x.ForginExchangePercentage, y => y.MapFrom(z => z.BankFees.ForeignExchangePercentage))
                .ForMember(x => x.ForginExchangeCreditPercentage, y => y.MapFrom(z => z.BankFees.ForeignExchangeCreditPercentage))
                .ForMember(x => x.BankFeesId, y => y.MapFrom(z => z.BankFees.Id));

            CreateMap<RoleViewModel, ApplicationRole>()
                .ReverseMap();

            CreateMap<UserViewModel, ApplicationUser>()
                .ReverseMap()
                .ForMember(x => x.EmployeeTypeName, y => y.MapFrom(z => z.EmployeeType.Name))
                .ForMember(x => x.EmployeeTypeNameAr, y => y.MapFrom(z => z.EmployeeType.NameAr))
                .ForMember(x => x.DepartmentName, y => y.MapFrom(z => z.Department != null ? z.Department.Name : ""))
                .ForMember(x => x.DepartmentName, y => y.MapFrom(z => z.Department != null ? z.Department.NameAr : ""))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.FirstName + " " + z.LastName));

            CreateMap<EditUserViewModel, ApplicationUser>()
                .ReverseMap()
                .ForMember(x => x.SupervisorIds, y => y.MapFrom(z => z.Supervisors.Select(s => s.SupervisorId).ToList()));

            CreateMap<CreateUserViewModel, ApplicationUser>()
                .ForMember(x => x.UserName, y => y.MapFrom(z => z.Email));

            CreateMap<BidBondRequestViewModel, BidBondRequest>()
                .ReverseMap()
                .ForMember(x => x.RequestedByName, y => y.MapFrom(z => z.RequestedBy.Name))
                .ForMember(x => x.SentByName, y => y.MapFrom(z => z.SentBy != null ? z.SentBy.Name : ""))
                .ForMember(x => x.StatusByName, y => y.MapFrom(z => z.StatusBy != null ? z.StatusBy.Name : ""))
                .ForMember(x => x.PrintedByName, y => y.MapFrom(z => z.PrintedBy != null ? z.PrintedBy.Name : ""))
                .ForMember(x => x.BidBondVM, y => y.MapFrom(z => z.BidBond))
                .ForMember(x => x.CustomerName, y => y.MapFrom(z => z.BidBond.Opportunity.Customer.Name))
                .ForMember(x => x.CustomerNameAr, y => y.MapFrom(z => z.BidBond.Opportunity.Customer.NameAr));

            CreateMap<BidBondViewModel, BidBond>()
                .ReverseMap()
                .ForMember(x => x.OpportunityVM, y => y.MapFrom(z => z.Opportunity))
                .ForMember(x => x.RequestVM, y => y.MapFrom(z => z.Request))
                .ForMember(x => x.IsRequested, y => y.MapFrom(z => z.Request != null))
                .ForMember(x => x.IsSent, y => y.MapFrom(z => z.Request != null && z.Request.IsSent))
                .ForMember(x => x.IsApproved, y => y.MapFrom(z => z.Request != null && z.Request.Status == RequestStatusEnum.Approved))
                .ForMember(x => x.IsRejected, y => y.MapFrom(z => z.Request != null && z.Request.Status == RequestStatusEnum.Rejected))
                .ForMember(x => x.IsPrinted, y => y.MapFrom(z => z.Request != null && z.Request.IsPrinted))
                .ForMember(x => x.AssignedToName, y => y.MapFrom(z => z.AssignedTo != null ? z.AssignedTo.Name : ""))
                .ForMember(x => x.BidBondOn_BankName, y => y.MapFrom(z => z.BidBondOn_Bank != null ? z.BidBondOn_Bank.Name : ""))
                .ForMember(x => x.BidBondOn_BankNameAr, y => y.MapFrom(z => z.BidBondOn_Bank != null ? z.BidBondOn_Bank.NameAr : ""))
                .ForMember(x => x.BidBondAttachmentPath, y => y.MapFrom(z => z.BidBondAttachment != null ? z.BidBondAttachment.FilePath : ""))
                .ForMember(x => x.BidBondAttachmentTitle, y => y.MapFrom(z => z.BidBondAttachment != null ? z.BidBondAttachment.Title : ""))
                .ForMember(x => x.BidBondAttachmentUploadedByName, y => y.MapFrom(z => z.BidBondAttachment != null ? z.BidBondAttachment.UploadedBy.Name : ""));

            CreateMap<BookTenderRequestViewModel, BookTenderRequest>()
                .ReverseMap()
                .ForMember(x => x.RequestedByName, y => y.MapFrom(z => z.RequestedBy.Name))
                .ForMember(x => x.SentByName, y => y.MapFrom(z => z.SentBy != null ? z.SentBy.Name : ""))
                .ForMember(x => x.StatusByName, y => y.MapFrom(z => z.StatusBy != null ? z.StatusBy.Name : ""))
                .ForMember(x => x.PrintedByName, y => y.MapFrom(z => z.PrintedBy != null ? z.PrintedBy.Name : ""))
                 .ForMember(x => x.BookTenderVM, y => y.MapFrom(z => z.BookTender))
                .ForMember(x => x.CustomerName, y => y.MapFrom(z => z.BookTender.Opportunity.Customer.Name))
                .ForMember(x => x.CustomerNameAr, y => y.MapFrom(z => z.BookTender.Opportunity.Customer.NameAr))
                .ForMember(x => x.Address, y => y.MapFrom(z => z.BookTender.Opportunity.Address))
                .ForMember(x => x.TenderDescription, y => y.MapFrom(z => z.BookTender.Opportunity.Name + (z.BookTender.Opportunity.TenderNumber != null ? " #" + z.BookTender.Opportunity.TenderNumber : "")));

            CreateMap<BookTenderViewModel, BookTender>()
                .ReverseMap()
                .ForMember(x => x.RequestVM, y => y.MapFrom(z => z.Request))
                .ForMember(x => x.IsRequested, y => y.MapFrom(z => z.Request != null))
                .ForMember(x => x.IsSent, y => y.MapFrom(z => z.Request != null && z.Request.IsSent))
                .ForMember(x => x.IsApproved, y => y.MapFrom(z => z.Request != null && z.Request.Status == RequestStatusEnum.Approved))
                .ForMember(x => x.IsRejected, y => y.MapFrom(z => z.Request != null && z.Request.Status == RequestStatusEnum.Rejected))
                .ForMember(x => x.IsPrinted, y => y.MapFrom(z => z.Request != null && z.Request.IsPrinted))
                .ForMember(x => x.AssignedToName, y => y.MapFrom(z => z.AssignedTo != null ? z.AssignedTo.Name : ""))
                .ForMember(x => x.ReceiptAttachmentPath, y => y.MapFrom(z => z.ReceiptAttachment != null ? z.ReceiptAttachment.FilePath : ""))
                .ForMember(x => x.ReceiptAttachmentTitle, y => y.MapFrom(z => z.ReceiptAttachment != null ? z.ReceiptAttachment.Title : ""))
                .ForMember(x => x.ReceiptAttachmentUploadedByName, y => y.MapFrom(z => z.ReceiptAttachment != null ? z.ReceiptAttachment.UploadedBy.Name : ""));

            CreateMap<ProductCategoryViewModel, ProductCategory>()
                .ReverseMap()
                .ForMember(x => x.DepartmentName, y => y.MapFrom(z => z.Department.Name))
                .ForMember(x => x.DepartmentNameAr, y => y.MapFrom(z => z.Department.NameAr))
                .ForMember(x => x.DepartmentCode, y => y.MapFrom(z => z.Department.Code))
                .ForMember(x => x.ParentName, y => y.MapFrom(z => z.Parent.Name))
                .ForMember(x => x.ParentNameAr, y => y.MapFrom(z => z.Parent.NameAr))
                .ForMember(x => x.ChildVMs, y => y.MapFrom(z => z.Childs))
                .ForMember(x => x.ProductVMs, y => y.MapFrom(z => z.Products))
                .ForMember(x => x.ParentVM, y => y.MapFrom(z => z.Parent))
                .ForMember(x => x.DepartmentVM, y => y.MapFrom(z => z.Department));

            CreateMap<ProductViewModel, Product>()
                .ReverseMap()
                .ForMember(x => x.IsService, y => y.MapFrom(z => z.Category.IsService))
                .ForMember(x => x.DepartmentId, y => y.MapFrom(z => z.Category.DepartmentId))
                .ForMember(x => x.DepartmentName, y => y.MapFrom(z => z.Category.Department.Name))
                .ForMember(x => x.DepartmentNameAr, y => y.MapFrom(z => z.Category.Department.NameAr))
                .ForMember(x => x.CategoryVM, y => y.MapFrom(z => z.Category))
                .ForMember(x => x.CategoryName, y => y.MapFrom(z => z.Category.Name))
                .ForMember(x => x.CategoryNameAr, y => y.MapFrom(z => z.Category.NameAr));

            CreateMap<ProductsViewModel, SupplierProduct>()
                .ReverseMap()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Product.Name))
                .ForMember(x => x.NameAr, y => y.MapFrom(z => z.Product.NameAr));

            CreateMap<SupplierProductsViewModel, Supplier>()
                .ReverseMap()
                .ForMember(x => x.ProductVMs, y => y.MapFrom(z => z.Products));

            CreateMap<OpportunityAttachmentViewModel, OpportunityAttachment>()
                .ReverseMap()
                .ForMember(x => x.AttachmentPath, y => y.MapFrom(z => z.Attachment.FilePath))
                .ForMember(x => x.AttachmentTitle, y => y.MapFrom(z => z.Attachment.Title))
                .ForMember(x => x.AttachmentUploadedByName, y => y.MapFrom(z => z.Attachment.UploadedBy.Name));

            CreateMap<SupplierContactViewModel, SupplierContact>()
                .ForMember(x => x.Contact, y => y.MapFrom(z => z.ContactVM))
                .ReverseMap()
                .ForMember(x => x.ContactVM, y => y.MapFrom(z => z.Contact));

            CreateMap<SupplierDepartmentViewModel, SupplierDepartment>()
                .ReverseMap()
                .ForMember(x => x.DepartmentName, y => y.MapFrom(z => z.Department.Name))
                .ForMember(x => x.DepartmentNameAr, y => y.MapFrom(z => z.Department.NameAr));

            CreateMap<SupplierEmployeeViewModel, SupplierEmployee>()
                .ForMember(x => x.Employee, y => y.MapFrom(z => z.EmployeeVM))
                .ReverseMap()
                .ForMember(x => x.EmployeeVM, y => y.MapFrom(z => z.Employee));

            CreateMap<SupplierFileViewModel, SupplierFile>()
                .ReverseMap()
                .ForMember(x => x.AttachmentPath, y => y.MapFrom(z => z.Attachment.FilePath))
                .ForMember(x => x.AttachmentTitle, y => y.MapFrom(z => z.Attachment.Title));

            CreateMap<SupplierClassificationViewModel, SupplierClassification>()
                .ReverseMap()
                .ForMember(x => x.ClassificationName, y => y.MapFrom(z => z.Classification.Name))
                .ForMember(x => x.ClassificationNameAr, y => y.MapFrom(z => z.Classification.NameAr));

            CreateMap<CertificateViewModel, Certificate>()
                .ReverseMap()
                .ForMember(x => x.AttachmentPath, y => y.MapFrom(z => z.Attachment.FilePath))
                .ForMember(x => x.AttachmentTitle, y => y.MapFrom(z => z.Attachment.Title));

            CreateMap<SupplierCertificateViewModel, SupplierCertificate>()
                .ForMember(x => x.Certificate, y => y.MapFrom(z => z.CertificateVM))
                .ReverseMap()
                .ForMember(x => x.CertificateVM, y => y.MapFrom(z => z.Certificate));

            CreateMap<SuppliersTreeViewModel, SupplierProduct>()
                .ReverseMap()
                .ForMember(x => x.SupplierId, y => y.MapFrom(z => z.Supplier.Id))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Supplier.Name))
                .ForMember(x => x.NameAr, y => y.MapFrom(z => z.Supplier.NameAr))
                .ForMember(x => x.CountryName, y => y.MapFrom(z => z.Supplier.Address != null ? z.Supplier.Address.City.Country.Name : ""))
                .ForMember(x => x.CountryNameAr, y => y.MapFrom(z => z.Supplier.Address != null ? z.Supplier.Address.City.Country.NameAr : ""));

            CreateMap<ProductSuppliersTreeViewModel, Product>()
                .ReverseMap()
                .ForMember(x => x.ProductId, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Suppliers, y => y.MapFrom(z => z.Suppliers));

            CreateMap<SupplierProductViewModel, SupplierProduct>()
                .ReverseMap()
                .ForMember(x => x.ProductName, y => y.MapFrom(z => z.Product.Name))
                .ForMember(x => x.ProductNameAr, y => y.MapFrom(z => z.Product.NameAr))
                .ForMember(x => x.ProductVM, y => y.MapFrom(z => z.Product));

            CreateMap<SupplierInfoViewModel, Supplier>()
                .ReverseMap();

            CreateMap<SupplierBankCurrencyViewModel, SupplierBankCurrency>()
               .ReverseMap()
               .ForMember(x => x.CurrencyName, y => y.MapFrom(z => z.Currency.Name))
               .ForMember(x => x.CurrencyNameAr, y => y.MapFrom(z => z.Currency.NameAr));

            CreateMap<SupplierBankViewModel, SupplierBank>()
                .ForMember(x => x.Currencies, y => y.MapFrom(z => z.CurrencyVMs))
                .ReverseMap()
                .ForMember(x => x.CurrencyVMs, y => y.MapFrom(z => z.Currencies))
                .ForMember(x => x.BankId, y => y.MapFrom(z => z.Branch.BankId))
                .ForMember(x => x.BankName, y => y.MapFrom(z => z.Branch.Bank.Name))
                .ForMember(x => x.BankNameAr, y => y.MapFrom(z => z.Branch.Bank.NameAr))
                .ForMember(x => x.BranchName, y => y.MapFrom(z => z.Branch.Name))
                .ForMember(x => x.BranchNameAr, y => y.MapFrom(z => z.Branch.NameAr))
                .ForMember(x => x.BranchAddress, y => y.MapFrom(z => z.Branch.Address))
                .ForMember(x => x.SwiftCode, y => y.MapFrom(z => z.Branch.SwiftCode));

            CreateMap<SupplierViewModel, Supplier>()
                .ForMember(x => x.LegalInfo, y => y.MapFrom(z => z.LegalInfoVM))
                .ForMember(x => x.Departments, y => y.MapFrom(z => z.DepartmentVMs))
                .ForMember(x => x.Classifications, y => y.MapFrom(z => z.ClassificationVMs))
                .ForMember(x => x.Contacts, y => y.MapFrom(z => z.ContactVMs))
                .ForMember(x => x.Employees, y => y.MapFrom(z => z.EmployeeVMs))
                .ForMember(x => x.Address, y => y.MapFrom(z => z.AddressVM))
                .ForMember(x => x.Certificates, y => y.MapFrom(z => z.CertificateVMs))
                .ForMember(x => x.Products, y => y.MapFrom(z => z.ProductVMs))
                .ReverseMap()
                .ForMember(x => x.LegalInfoVM, y => y.MapFrom(z => z.LegalInfo))
                .ForMember(x => x.LogoAttachmentPath, y => y.MapFrom(z => z.LogoAttachment.FilePath))
                .ForMember(x => x.DepartmentIds, y => y.MapFrom(z => z.Departments.Select(q => q.DepartmentId)))
                .ForMember(x => x.ActivityTypeName, y => y.MapFrom(z => z.ActivityType.Name))
                .ForMember(x => x.ActivityTypeNameAr, y => y.MapFrom(z => z.ActivityType.NameAr))
                .ForMember(x => x.OrganizationTypeName, y => y.MapFrom(z => z.OrganizationType.Name))
                .ForMember(x => x.OrganizationTypeNameAr, y => y.MapFrom(z => z.OrganizationType.NameAr))
                .ForMember(x => x.IsSubCompany, y => y.MapFrom(z => z.ParentId != null))
                .ForMember(x => x.AddressVM, y => y.MapFrom(z => z.Address))
                .ForMember(x => x.DepartmentVMs, y => y.MapFrom(z => z.Departments))
                .ForMember(x => x.ClassificationVMs, y => y.MapFrom(z => z.Classifications))
                .ForMember(x => x.ClassificationIds, y => y.MapFrom(z => z.Classifications.Select(q => q.ClassificationId)))
                .ForMember(x => x.ContactVMs, y => y.MapFrom(z => z.Contacts))
                .ForMember(x => x.EmployeeVMs, y => y.MapFrom(z => z.Employees))
                .ForMember(x => x.FileVMs, y => y.MapFrom(z => z.Files))
                .ForMember(x => x.ParentName, y => y.MapFrom(z => z.Parent.Name))
                .ForMember(x => x.ParentNameAr, y => y.MapFrom(z => z.Parent.NameAr))
                .ForMember(x => x.ChildVMs, y => y.MapFrom(z => z.Childs))
                .ForMember(x => x.CertificateVMs, y => y.MapFrom(z => z.Certificates))
                .ForMember(x => x.ProductVMs, y => y.MapFrom(z => z.Products))
                .ForMember(x => x.BankVMs, y => y.MapFrom(z => z.Banks))
                .ForMember(x => x.CompetitorName, y => y.MapFrom(z => z.CompetitorId != null ? z.Competitor.Name : ""))
                .ForMember(x => x.CompetitorNameAr, y => y.MapFrom(z => z.CompetitorId != null ? z.Competitor.NameAr : ""));

            CreateMap<TechnicalSpecificationAttachmentViewModel, TechnicalSpecificationAttachment>()
                .ReverseMap()
                .ForMember(x => x.AttachmentPath, y => y.MapFrom(z => z.Attachment.FilePath))
                .ForMember(x => x.Title, y => y.MapFrom(z => z.Attachment.Title))
                .ForMember(x => x.AttachmentUploadedByName, y => y.MapFrom(z => z.Attachment.UploadedBy.Name));

            CreateMap<TechnicalSpecificationProductViewModel, TechnicalSpecificationProduct>()
                .ForMember(x => x.RequestedOrigins, y => y.MapFrom(z => z.RequestedOriginVMs))
                .ReverseMap()
                .ForMember(x => x.ProductName, y => y.MapFrom(z => z.Product.Name))
                .ForMember(x => x.ProductNameAr, y => y.MapFrom(z => z.Product.NameAr))
                .ForMember(x => x.ProductSelectStr, y => y.MapFrom(z => z.Product.Name + " - " + z.Description))
                .ForMember(x => x.ProductSelectStrAr, y => y.MapFrom(z => z.Product.NameAr + " - " + z.Description))
                .ForMember(x => x.AttachmentPath, y => y.MapFrom(z => z.Attachment != null ? z.Attachment.FilePath : ""))
                .ForMember(x => x.AttachmentTitle, y => y.MapFrom(z => z.Attachment != null ? z.Attachment.Title : ""))
                .ForMember(x => x.ProductVM, y => y.MapFrom(z => z.Product))
                .ForMember(x => x.ProductOriginName, y => y.MapFrom(z => z.ProductOrigin.Name))
                .ForMember(x => x.ProductOriginNameAr, y => y.MapFrom(z => z.ProductOrigin.NameAr))
                .ForMember(x => x.RequestedOriginVMs, y => y.MapFrom(z => z.RequestedOrigins));

            CreateMap<TechnicalSpecificationViewModel, TechnicalSpecification>()
                .ForMember(x => x.Products, y => y.MapFrom(z => z.ProductVMs))
                .ForMember(x => x.Attachments, y => y.MapFrom(z => z.AttachmentFiles))
                .ReverseMap()
                .ForMember(x => x.ProductVMs, y => y.MapFrom(z => z.Products))
                .ForMember(x => x.AttachmentFiles, y => y.MapFrom(z => z.Attachments));

            CreateMap<RequestOfferProductViewModel, RequestOfferProduct>()
                .ReverseMap()
                .ForMember(x => x.ProductVM, y => y.MapFrom(z => z.Product));

            CreateMap<RequestOfferViewModel, RequestOffer>()
                .ForMember(x => x.Products, y => y.MapFrom(z => z.ProductVMs))
                .ReverseMap()
                .ForMember(x => x.SupplierName, y => y.MapFrom(z => z.Supplier.Name))
                .ForMember(x => x.SupplierNameAr, y => y.MapFrom(z => z.Supplier.NameAr))
                .ForMember(x => x.SupplierVM, y => y.MapFrom(z => z.Supplier))
                .ForMember(x => x.ProductVMs, y => y.MapFrom(z => z.Products));

            CreateMap<SupplierOfferProductViewModel, SupplierOfferProduct>()
                .ReverseMap()
                .ForMember(x => x.AttachmentPath, y => y.MapFrom(z => z.Attachment.FilePath))
                .ForMember(x => x.AttachmentTitle, y => y.MapFrom(z => z.Attachment.Title))
                .ForMember(x => x.ProductVM, y => y.MapFrom(z => z.TechnicalSpecificationProduct))
                .ForMember(x => x.ProductIndex, y => y.MapFrom(z => (int)z.TechnicalSpecificationProduct.Index))
                .ForMember(x => x.SupplierName, y => y.MapFrom(z => z.Offer.Supplier.Name))
                .ForMember(x => x.SupplierNameAr, y => y.MapFrom(z => z.Offer.Supplier.NameAr))
                .ForMember(x => x.ExchangeRate, y => y.MapFrom(z => z.Offer.ExchangeRate));

            CreateMap<SupplierOfferAttachmentViewModel, SupplierOfferAttachment>()
                .ReverseMap()
                .ForMember(x => x.AttachmentPath, y => y.MapFrom(z => z.Attachment.FilePath))
                .ForMember(x => x.AttachmentTitle, y => y.MapFrom(z => z.Attachment.Title));

            CreateMap<SupplierOfferViewModel, SupplierOffer>()
                .ForMember(x => x.Products, y => y.MapFrom(z => z.ProductVMs))
                .ForMember(x => x.Attachments, y => y.MapFrom(z => z.AttachmentVMs))
                .ReverseMap()
                .ForMember(x => x.CurrencyName, y => y.MapFrom(z => z.Currency.Name))
                .ForMember(x => x.CurrencyNameAr, y => y.MapFrom(z => z.Currency.NameAr))
                .ForMember(x => x.CurrencySymbol, y => y.MapFrom(z => z.Currency.Symbol))
                .ForMember(x => x.DeliveryTypeName, y => y.MapFrom(z => z.DeliveryType.Name))
                .ForMember(x => x.DeliveryTypeNameAr, y => y.MapFrom(z => z.DeliveryType.NameAr))
                .ForMember(x => x.PaymentTypeName, y => y.MapFrom(z => z.PaymentType.Name))
                .ForMember(x => x.PaymentTypeNameAr, y => y.MapFrom(z => z.PaymentType.NameAr))
                .ForMember(x => x.SupplierName, y => y.MapFrom(z => z.Supplier.Name))
                .ForMember(x => x.SupplierNameAr, y => y.MapFrom(z => z.Supplier.NameAr))
                .ForMember(x => x.SupplierLogoAttachmentPath, y => y.MapFrom(z => z.Supplier.LogoAttachment.FilePath))
                .ForMember(x => x.ProductVMs, y => y.MapFrom(z => z.Products))
                .ForMember(x => x.AttachmentVMs, y => y.MapFrom(z => z.Attachments));

            CreateMap<OpportunityInfoViewModel, Opportunity>()
                .ReverseMap()
                .ForMember(x => x.CustomerName, y => y.MapFrom(z => z.Customer.Name))
                .ForMember(x => x.CustomerNameAr, y => y.MapFrom(z => z.Customer.NameAr));

            CreateMap<OpportunityViewModel, Opportunity>()
                .ForMember(x => x.BidBond, y => y.MapFrom(z => z.BidBondVM))
                .ForMember(x => x.BookTender, y => y.MapFrom(z => z.BookTenderVM))
                .ForMember(x => x.TechnicalSpecification, y => y.MapFrom(z => z.TechnicalSpecificationVM))
                .ForMember(x => x.RequestOffers, y => y.MapFrom(z => z.RequestOfferVMs))
                .ForMember(x => x.SupplierOffers, y => y.MapFrom(z => z.SupplierOfferVMs))
                .ReverseMap()
                .ForMember(x => x.CreatedByName, y => y.MapFrom(z => z.CreatedBy.Name))
                .ForMember(x => x.TypeName, y => y.MapFrom(z => z.Type.Name))
                .ForMember(x => x.CustomerLogoPath, y => y.MapFrom(z => z.Customer.LogoAttachment.FilePath))
                .ForMember(x => x.TypeNameAr, y => y.MapFrom(z => z.Type.NameAr))
                .ForMember(x => x.CustomerName, y => y.MapFrom(z => z.Customer.Name))
                .ForMember(x => x.CustomerNameAr, y => y.MapFrom(z => z.Customer.NameAr))
                .ForMember(x => x.SalesAgentName, y => y.MapFrom(z => z.SalesAgent.Name))
                .ForMember(x => x.ProjectManagerName, y => y.MapFrom(z => z.ProjectManager != null ? z.ProjectManager.Name : ""))
                .ForMember(x => x.GuestName, y => y.MapFrom(z => z.Guest != null ? z.Guest.Name : ""))
                .ForMember(x => x.StatusEnum, y => y.MapFrom(z => (OpportunityStatusEnum)z.Status.EnumValue))
                .ForMember(x => x.CountryName, y => y.MapFrom(z => z.Country != null ? z.Country.Name : ""))
                .ForMember(x => x.CountryNameAr, y => y.MapFrom(z => z.Country != null ? z.Country.NameAr : ""))
                .ForMember(x => x.DepartmentName, y => y.MapFrom(z => z.Department.Name))
                .ForMember(x => x.DepartmentNameAr, y => y.MapFrom(z => z.Department.NameAr))
                .ForMember(x => x.DepartmentCode, y => y.MapFrom(z => z.Department.Code))
                .ForMember(x => x.BidBondVM, y => y.MapFrom(z => z.BidBond))
                .ForMember(x => x.BookTenderVM, y => y.MapFrom(z => z.BookTender))
                .ForMember(x => x.IsBidBond, y => y.MapFrom(z => z.BidBond.Id != 0))
                .ForMember(x => x.IsBookTender, y => y.MapFrom(z => z.BookTender.Id != 0))
                .ForMember(x => x.TechnicalSpecificationVM, y => y.MapFrom(z => z.TechnicalSpecification))
                .ForMember(x => x.RequestOfferVMs, y => y.MapFrom(z => z.RequestOffers))
                .ForMember(x => x.SupplierOfferVMs, y => y.MapFrom(z => z.SupplierOffers));

            CreateMap<CustomerDepartmentViewModel, CustomerDepartment>()
                .ReverseMap()
                .ForMember(x => x.DepartmentName, y => y.MapFrom(z => z.Department.Name))
                .ForMember(x => x.DepartmentNameAr, y => y.MapFrom(z => z.Department.NameAr));

            CreateMap<CustomerAgentViewModel, CustomerAgent>()
                .ReverseMap()
                .ForMember(x => x.SalesAgentName, y => y.MapFrom(z => z.SalesAgent.Name));

            CreateMap<CustomerEmployeeViewModel, CustomerEmployee>()
                .ForMember(x => x.Employee, y => y.MapFrom(z => z.EmployeeVM))
                .ReverseMap()
                .ForMember(x => x.EmployeeVM, y => y.MapFrom(z => z.Employee));

            CreateMap<CustomerContactViewModel, CustomerContact>()
                .ForMember(x => x.Contact, y => y.MapFrom(z => z.ContactVM))
                .ReverseMap()
                .ForMember(x => x.ContactVM, y => y.MapFrom(z => z.Contact));

            CreateMap<CustomerVendorRegistrationViewModel, CustomerVendorRegistration>()
                .ReverseMap()
                .ForMember(x => x.AttachmentPath, y => y.MapFrom(z => z.Attachment.FilePath))
                .ForMember(x => x.AttachmentTitle, y => y.MapFrom(z => z.Attachment.Title));

            CreateMap<CustomerFileViewModel, CustomerFile>()
                .ReverseMap()
                .ForMember(x => x.AttachmentPath, y => y.MapFrom(z => z.Attachment.FilePath))
                .ForMember(x => x.AttachmentTitle, y => y.MapFrom(z => z.Attachment.Title));

            CreateMap<CustomerBankCurrencyViewModel, CustomerBankCurrency>()
                .ReverseMap()
                .ForMember(x => x.CurrencyName, y => y.MapFrom(z => z.Currency.Name))
                .ForMember(x => x.CurrencyNameAr, y => y.MapFrom(z => z.Currency.NameAr));

            CreateMap<CustomerBankViewModel, CustomerBank>()
                .ForMember(x => x.Currencies, y => y.MapFrom(z => z.CurrencyVMs))
                .ReverseMap()
                .ForMember(x => x.CurrencyVMs, y => y.MapFrom(z => z.Currencies))
                .ForMember(x => x.BankId, y => y.MapFrom(z => z.Branch.BankId))
                .ForMember(x => x.BankName, y => y.MapFrom(z => z.Branch.Bank.Name))
                .ForMember(x => x.BankNameAr, y => y.MapFrom(z => z.Branch.Bank.NameAr))
                .ForMember(x => x.BranchName, y => y.MapFrom(z => z.Branch.Name))
                .ForMember(x => x.BranchNameAr, y => y.MapFrom(z => z.Branch.NameAr))
                .ForMember(x => x.BranchAddress, y => y.MapFrom(z => z.Branch.Address))
                .ForMember(x => x.SwiftCode, y => y.MapFrom(z => z.Branch.SwiftCode));

            CreateMap<CustomerViewModel, Customer>()
                .ForMember(x => x.LegalInfo, y => y.MapFrom(z => z.LegalInfoVM))
                .ForMember(x => x.Address, y => y.MapFrom(z => z.AddressVM))
                .ForMember(x => x.Departments, y => y.MapFrom(z => z.DepartmentVMs))
                .ForMember(x => x.SalesAgents, y => y.MapFrom(z => z.SalesAgentVMs))
                .ForMember(x => x.Contacts, y => y.MapFrom(z => z.ContactVMs))
                .ForMember(x => x.VendorRegistrations, y => y.MapFrom(z => z.VendorRegistrationVMs))
                .ForMember(x => x.Employees, y => y.MapFrom(z => z.EmployeeVMs))
                .ReverseMap()
                .ForMember(x => x.CreatedByName, y => y.MapFrom(z => z.CreatedBy.Name))
                .ForMember(x => x.LogoAttachmentPath, y => y.MapFrom(z => z.LogoAttachment.FilePath))
                .ForMember(x => x.DepartmentIds, y => y.MapFrom(z => z.Departments.Select(q => q.DepartmentId)))
                .ForMember(x => x.SalesAgentIds, y => y.MapFrom(z => z.SalesAgents.Select(q => q.SalesAgentId)))
                .ForMember(x => x.LegalInfoVM, y => y.MapFrom(z => z.LegalInfo))
                .ForMember(x => x.AddressVM, y => y.MapFrom(z => z.Address))
                .ForMember(x => x.DepartmentVMs, y => y.MapFrom(z => z.Departments))
                .ForMember(x => x.SalesAgentVMs, y => y.MapFrom(z => z.SalesAgents))
                .ForMember(x => x.IsSubCompany, y => y.MapFrom(z => z.ParentId != null))
                .ForMember(x => x.ContactVMs, y => y.MapFrom(z => z.Contacts))
                .ForMember(x => x.VendorRegistrationVMs, y => y.MapFrom(z => z.VendorRegistrations))
                .ForMember(x => x.FileVMs, y => y.MapFrom(z => z.Files))
                .ForMember(x => x.EmployeeVMs, y => y.MapFrom(z => z.Employees))
                .ForMember(x => x.ActivityTypeName, y => y.MapFrom(z => z.ActivityType.Name))
                .ForMember(x => x.ActivityTypeNameAr, y => y.MapFrom(z => z.ActivityType.NameAr))
                .ForMember(x => x.OrganizationTypeName, y => y.MapFrom(z => z.OrganizationType.Name))
                .ForMember(x => x.OrganizationTypeNameAr, y => y.MapFrom(z => z.OrganizationType.NameAr))
                .ForMember(x => x.ParentName, y => y.MapFrom(z => z.Parent.Name))
                .ForMember(x => x.ParentNameAr, y => y.MapFrom(z => z.Parent.NameAr))
                .ForMember(x => x.ChildVMs, y => y.MapFrom(z => z.Childs))
                .ForMember(x => x.BankVMs, y => y.MapFrom(z => z.Banks))
                .ForMember(x => x.VisitVMs, y => y.MapFrom(z => z.Visits));

            CreateMap<CompetitorDepartmentViewModel, CompetitorDepartment>()
                .ReverseMap()
                .ForMember(x => x.DepartmentName, y => y.MapFrom(z => z.Department.Name))
                .ForMember(x => x.DepartmentNameAr, y => y.MapFrom(z => z.Department.NameAr));

            CreateMap<CompetitorContactViewModel, CompetitorContact>()
                .ForMember(x => x.Contact, y => y.MapFrom(z => z.ContactVM))
                .ReverseMap()
                .ForMember(x => x.ContactVM, y => y.MapFrom(z => z.Contact));

            CreateMap<CompetitorFileViewModel, CompetitorFile>()
                .ReverseMap()
                .ForMember(x => x.AttachmentPath, y => y.MapFrom(z => z.Attachment.FilePath))
                .ForMember(x => x.AttachmentTitle, y => y.MapFrom(z => z.Attachment.Title));

            CreateMap<CompetitorEmployeeViewModel, CompetitorEmployee>()
                .ForMember(x => x.Employee, y => y.MapFrom(z => z.EmployeeVM))
                .ReverseMap()
                .ForMember(x => x.EmployeeVM, y => y.MapFrom(z => z.Employee));

            CreateMap<CompetitorProductViewModel, CompetitorProduct>()
                .ReverseMap()
                .ForMember(x => x.ProductName, y => y.MapFrom(z => z.Product.Name))
                .ForMember(x => x.ProductNameAr, y => y.MapFrom(z => z.Product.NameAr))
                .ForMember(x => x.ProductVM, y => y.MapFrom(z => z.Product));

            CreateMap<CompetitorBankCurrencyViewModel, CompetitorBankCurrency>()
               .ReverseMap()
               .ForMember(x => x.CurrencyName, y => y.MapFrom(z => z.Currency.Name))
               .ForMember(x => x.CurrencyNameAr, y => y.MapFrom(z => z.Currency.NameAr));

            CreateMap<CompetitorBankViewModel, CompetitorBank>()
                .ForMember(x => x.Currencies, y => y.MapFrom(z => z.CurrencyVMs))
                .ReverseMap()
                .ForMember(x => x.CurrencyVMs, y => y.MapFrom(z => z.Currencies))
                .ForMember(x => x.BankId, y => y.MapFrom(z => z.Branch.BankId))
                .ForMember(x => x.BankName, y => y.MapFrom(z => z.Branch.Bank.Name))
                .ForMember(x => x.BankNameAr, y => y.MapFrom(z => z.Branch.Bank.NameAr))
                .ForMember(x => x.BranchName, y => y.MapFrom(z => z.Branch.Name))
                .ForMember(x => x.BranchNameAr, y => y.MapFrom(z => z.Branch.NameAr))
                .ForMember(x => x.BranchAddress, y => y.MapFrom(z => z.Branch.Address))
                .ForMember(x => x.SwiftCode, y => y.MapFrom(z => z.Branch.SwiftCode));

            CreateMap<CompetitorViewModel, Competitor>()
                .ForMember(x => x.Products, y => y.MapFrom(z => z.ProductVMs))
                .ForMember(x => x.LegalInfo, y => y.MapFrom(z => z.LegalInfoVM))
                .ForMember(x => x.Departments, y => y.MapFrom(z => z.DepartmentVMs))
                .ForMember(x => x.Contacts, y => y.MapFrom(z => z.ContactVMs))
                .ForMember(x => x.Employees, y => y.MapFrom(z => z.EmployeeVMs))
                .ForMember(x => x.Address, y => y.MapFrom(z => z.AddressVM))
                .ForMember(x => x.Products, y => y.MapFrom(z => z.ProductVMs))
                .ReverseMap()
                .ForMember(x => x.LegalInfoVM, y => y.MapFrom(z => z.LegalInfo))
                .ForMember(x => x.LogoAttachmentPath, y => y.MapFrom(z => z.LogoAttachment.FilePath))
                .ForMember(x => x.DepartmentIds, y => y.MapFrom(z => z.Departments.Select(q => q.DepartmentId)))
                .ForMember(x => x.ActivityTypeName, y => y.MapFrom(z => z.ActivityType.Name))
                .ForMember(x => x.ActivityTypeNameAr, y => y.MapFrom(z => z.ActivityType.NameAr))
                .ForMember(x => x.OrganizationTypeName, y => y.MapFrom(z => z.OrganizationType.Name))
                .ForMember(x => x.OrganizationTypeNameAr, y => y.MapFrom(z => z.OrganizationType.NameAr))
                .ForMember(x => x.IsSubCompany, y => y.MapFrom(z => z.ParentId != null))
                .ForMember(x => x.AddressVM, y => y.MapFrom(z => z.Address))
                .ForMember(x => x.DepartmentVMs, y => y.MapFrom(z => z.Departments))
                .ForMember(x => x.ContactVMs, y => y.MapFrom(z => z.Contacts))
                .ForMember(x => x.EmployeeVMs, y => y.MapFrom(z => z.Employees))
                .ForMember(x => x.FileVMs, y => y.MapFrom(z => z.Files))
                .ForMember(x => x.ParentName, y => y.MapFrom(z => z.Parent.Name))
                .ForMember(x => x.ParentNameAr, y => y.MapFrom(z => z.Parent.NameAr))
                .ForMember(x => x.ChildVMs, y => y.MapFrom(z => z.Childs))
                .ForMember(x => x.ProductVMs, y => y.MapFrom(z => z.Products))
                .ForMember(x => x.CreatedByName, y => y.MapFrom(z => z.CreatedBy.Name))
                .ForMember(x => x.BankVMs, y => y.MapFrom(z => z.Banks));

            CreateMap<VisitViewModel, Visit>()
                .ReverseMap()
                .ForMember(x => x.CustomerName, y => y.MapFrom(z => z.Customer.Name))
                .ForMember(x => x.CustomerNameAr, y => y.MapFrom(z => z.Customer.NameAr))
                .ForMember(x => x.CustomerDepartmentIds, y => y.MapFrom(z => z.Customer.Departments.Select(d => d.DepartmentId)))
                .ForMember(x => x.ReasonName, y => y.MapFrom(z => z.Reason.Name))
                .ForMember(x => x.ReasonNameAr, y => y.MapFrom(z => z.Reason.NameAr))
                .ForMember(x => x.SalesAgentName, y => y.MapFrom(z => z.SalesAgent.Name));

            CreateMap<TaskViewModel, Tasks>()
                .ReverseMap()
                .ForMember(x => x.CreatedByName, y => y.MapFrom(z => z.CreatedBy.Name))
                .ForMember(x => x.CreatedByProfilePicture, y => y.MapFrom(z => z.CreatedBy.ProfilePicture))
                .ForMember(x => x.CreatedByDepartmentId, y => y.MapFrom(z => z.CreatedBy.DepartmentId))
                .ForMember(x => x.AssignedToName, y => y.MapFrom(z => z.AssignedTo.Name))
                .ForMember(x => x.AssignedToProfilePicture, y => y.MapFrom(z => z.AssignedTo.ProfilePicture))
                .ForMember(x => x.AssignedToDepartmentId, y => y.MapFrom(z => z.AssignedTo.DepartmentId));

            CreateMap<LogisticDepartmentViewModel, LogisticDepartment>()
               .ReverseMap()
               .ForMember(x => x.DepartmentName, y => y.MapFrom(z => z.Department.Name))
               .ForMember(x => x.DepartmentNameAr, y => y.MapFrom(z => z.Department.NameAr));

            CreateMap<LogisticContactViewModel, LogisticContact>()
                .ForMember(x => x.Contact, y => y.MapFrom(z => z.ContactVM))
                .ReverseMap()
                .ForMember(x => x.ContactVM, y => y.MapFrom(z => z.Contact));

            CreateMap<LogisticFileViewModel, LogisticFile>()
                .ReverseMap()
                .ForMember(x => x.AttachmentPath, y => y.MapFrom(z => z.Attachment.FilePath))
                .ForMember(x => x.AttachmentTitle, y => y.MapFrom(z => z.Attachment.Title));

            CreateMap<LogisticEmployeeViewModel, LogisticEmployee>()
                .ForMember(x => x.Employee, y => y.MapFrom(z => z.EmployeeVM))
                .ReverseMap()
                .ForMember(x => x.EmployeeVM, y => y.MapFrom(z => z.Employee));

            CreateMap<LogisticBankCurrencyViewModel, LogisticBankCurrency>()
              .ReverseMap()
              .ForMember(x => x.CurrencyName, y => y.MapFrom(z => z.Currency.Name))
              .ForMember(x => x.CurrencyNameAr, y => y.MapFrom(z => z.Currency.NameAr));

            CreateMap<LogisticBankViewModel, LogisticBank>()
                .ForMember(x => x.Currencies, y => y.MapFrom(z => z.CurrencyVMs))
                .ReverseMap()
                .ForMember(x => x.CurrencyVMs, y => y.MapFrom(z => z.Currencies))
                .ForMember(x => x.BankId, y => y.MapFrom(z => z.Branch.BankId))
                .ForMember(x => x.BankName, y => y.MapFrom(z => z.Branch.Bank.Name))
                .ForMember(x => x.BankNameAr, y => y.MapFrom(z => z.Branch.Bank.NameAr))
                .ForMember(x => x.BranchName, y => y.MapFrom(z => z.Branch.Name))
                .ForMember(x => x.BranchNameAr, y => y.MapFrom(z => z.Branch.NameAr))
                .ForMember(x => x.BranchAddress, y => y.MapFrom(z => z.Branch.Address))
                .ForMember(x => x.SwiftCode, y => y.MapFrom(z => z.Branch.SwiftCode));

            CreateMap<LogisticViewModel, Logistic>()
                .ForMember(x => x.LegalInfo, y => y.MapFrom(z => z.LegalInfoVM))
                .ForMember(x => x.Departments, y => y.MapFrom(z => z.DepartmentVMs))
                .ForMember(x => x.Contacts, y => y.MapFrom(z => z.ContactVMs))
                .ForMember(x => x.Employees, y => y.MapFrom(z => z.EmployeeVMs))
                .ForMember(x => x.Address, y => y.MapFrom(z => z.AddressVM))
                .ReverseMap()
                .ForMember(x => x.LegalInfoVM, y => y.MapFrom(z => z.LegalInfo))
                .ForMember(x => x.LogoAttachmentPath, y => y.MapFrom(z => z.LogoAttachment.FilePath))
                .ForMember(x => x.DepartmentIds, y => y.MapFrom(z => z.Departments.Select(q => q.DepartmentId)))
                .ForMember(x => x.ActivityTypeName, y => y.MapFrom(z => z.ActivityType.Name))
                .ForMember(x => x.ActivityTypeNameAr, y => y.MapFrom(z => z.ActivityType.NameAr))
                .ForMember(x => x.OrganizationTypeName, y => y.MapFrom(z => z.OrganizationType.Name))
                .ForMember(x => x.OrganizationTypeNameAr, y => y.MapFrom(z => z.OrganizationType.NameAr))
                .ForMember(x => x.IsSubCompany, y => y.MapFrom(z => z.ParentId != null))
                .ForMember(x => x.AddressVM, y => y.MapFrom(z => z.Address))
                .ForMember(x => x.DepartmentVMs, y => y.MapFrom(z => z.Departments))
                .ForMember(x => x.ContactVMs, y => y.MapFrom(z => z.Contacts))
                .ForMember(x => x.EmployeeVMs, y => y.MapFrom(z => z.Employees))
                .ForMember(x => x.FileVMs, y => y.MapFrom(z => z.Files))
                .ForMember(x => x.ParentName, y => y.MapFrom(z => z.Parent.Name))
                .ForMember(x => x.ParentNameAr, y => y.MapFrom(z => z.Parent.NameAr))
                .ForMember(x => x.ChildVMs, y => y.MapFrom(z => z.Childs))
                .ForMember(x => x.CreatedByName, y => y.MapFrom(z => z.CreatedBy.Name))
                .ForMember(x => x.BankVMs, y => y.MapFrom(z => z.Banks));

            CreateMap<UserDepartmentViewModel, UserDepartment>()
                .ReverseMap()
                .ForMember(x => x.DepartmentName, y => y.MapFrom(z => z.Department.Name))
                .ForMember(x => x.DepartmentNameAr, y => y.MapFrom(z => z.Department.NameAr));

            CreateMap<UserRoleViewModel, ApplicationUserRole>()
                .ReverseMap()
                .ForMember(x => x.RoleName, y => y.MapFrom(z => z.Role.Name))
                .ForMember(x => x.RoleNameAr, y => y.MapFrom(z => z.Role.NameAr));

            CreateMap<UserInfoViewModel, ApplicationUser>()
                .ReverseMap()
                .ForMember(x => x.DefaultDepartmentName, y => y.MapFrom(z => z.Department.Name))
                .ForMember(x => x.DefaultDepartmentNameAr, y => y.MapFrom(z => z.Department.NameAr))
                .ForMember(x => x.EmployeeTypeName, y => y.MapFrom(z => z.EmployeeType.Name))
                .ForMember(x => x.EmployeeTypeNameAr, y => y.MapFrom(z => z.EmployeeType.NameAr))
                .ForMember(x => x.DirectManagerName, y => y.MapFrom(z => z.DirectManager.Name))
                .ForMember(x => x.DepartmentVMs, y => y.MapFrom(z => z.UserDepartments))
                .ForMember(x => x.RoleVMs, y => y.MapFrom(z => z.UserRoles));

            CreateMap<QuotationOfferProductOriginViewModel, QuotationOfferProductOrigin>()
                .ReverseMap()
                .ForMember(x => x.CountryName, y => y.MapFrom(z => z.Country.Name))
                .ForMember(x => x.CountryNameAr, y => y.MapFrom(z => z.Country.NameAr));

            CreateMap<QuotationOfferProductViewModel, QuotationOfferProduct>()
                .ForMember(x => x.SelectedOrigins, y => y.MapFrom(z => z.SelectedOriginVMs))
                .ReverseMap()
                .ForMember(x => x.ProductName, y => y.MapFrom(z => z.Product.TechnicalSpecificationProduct.Product.Name))
                .ForMember(x => x.ProductNameAr, y => y.MapFrom(z => z.Product.TechnicalSpecificationProduct.Product.NameAr))
                .ForMember(x => x.ProductVM, y => y.MapFrom(z => z.Product))
                .ForMember(x => x.SelectedOriginVMs, y => y.MapFrom(z => z.SelectedOrigins));


            CreateMap<QuotationOfferCertificateViewModel, QuotationOfferCertificate>()
                .ReverseMap()
                .ForMember(x => x.CertificateName, y => y.MapFrom(z => z.Certificate.Name))
                .ForMember(x => x.CertificateNameAr, y => y.MapFrom(z => z.Certificate.NameAr));

            CreateMap<QuotationOfferVersionViewModel, QuotationOfferVersion>()
                .ForMember(x => x.Products, y => y.MapFrom(z => z.ProductVMs))
                .ForMember(x => x.Certificates, y => y.MapFrom(z => z.CertificateVMs))
                .ReverseMap()
                .ForMember(x => x.CertificateVMs, y => y.MapFrom(z => z.Certificates))
                .ForMember(x => x.ProductVMs, y => y.MapFrom(z => z.Products))
                .ForMember(x => x.CreatedByName, y => y.MapFrom(z => z.CreatedBy.Name))
                .ForMember(x => x.CurrencyName, y => y.MapFrom(z => z.Currency.Name))
                .ForMember(x => x.CurrencyNameAr, y => y.MapFrom(z => z.Currency.NameAr))
                .ForMember(x => x.CurrencySymbol, y => y.MapFrom(z => z.Currency.Symbol))
                .ForMember(x => x.ValidityName, y => y.MapFrom(z => z.Validity.Name))
                .ForMember(x => x.ValidityNameAr, y => y.MapFrom(z => z.Validity.NameAr))
                .ForMember(x => x.DeliveryPlaceName, y => y.MapFrom(z => z.DeliveryPlace.Name))
                .ForMember(x => x.DeliveryPlaceNameAr, y => y.MapFrom(z => z.DeliveryPlace.NameAr))
                .ForMember(x => x.OriginDocumentName, y => y.MapFrom(z => z.OriginDocument.Name))
                .ForMember(x => x.OriginDocumentNameAr, y => y.MapFrom(z => z.OriginDocument.NameAr))
                .ForMember(x => x.GuaranteeTermName, y => y.MapFrom(z => z.GuaranteeTerm.Name))
                .ForMember(x => x.GuaranteeTermNameAr, y => y.MapFrom(z => z.GuaranteeTerm.NameAr))
                .ForMember(x => x.OfferVM, y => y.MapFrom(z => z.Offer))
                .ForMember(x => x.AlternateVMs, y => y.MapFrom(z => z.AlternateVersions));

            CreateMap<QuotationOfferViewModel, QuotationOffer>()
                .ForMember(x => x.Versions, y => y.MapFrom(z => z.VersionVMs))
                .ReverseMap()
                .ForMember(x => x.VersionVMs, y => y.MapFrom(z => z.Versions));

            CreateMap<QuotationCurrencyViewModel, QuotationCurrency>()
                .ReverseMap()
                .ForMember(x => x.CurrencyName, y => y.MapFrom(z => z.Currency.Name))
                .ForMember(x => x.CurrencyNameAr, y => y.MapFrom(z => z.Currency.NameAr));

            CreateMap<QuotationViewModel, Quotation>()
                .ReverseMap()
                .ForMember(x => x.AdminName, y => y.MapFrom(z => z.Admin.Name))
                .ForMember(x => x.CreatedByName, y => y.MapFrom(z => z.CreatedBy.Name))
                .ForMember(x => x.OpportunityName, y => y.MapFrom(z => z.Opportunity.Name))
                .ForMember(x => x.StatusName, y => y.MapFrom(z => z.Status.Name))
                .ForMember(x => x.StatusNameAr, y => y.MapFrom(z => z.Status.NameAr))
                .ForMember(x => x.RejectReasonName, y => y.MapFrom(z => z.RejectReason == null ? "" : z.RejectReason.Name))
                .ForMember(x => x.RejectReasonNameAr, y => y.MapFrom(z => z.RejectReason == null ? "" : z.RejectReason.NameAr))
                .ForMember(x => x.OpportunityVM, y => y.MapFrom(z => z.Opportunity))
                .ForMember(x => x.CurrencyVMs, y => y.MapFrom(z => z.Currencies))
                .ForMember(x => x.OfferVMs, y => y.MapFrom(z => z.Offers))
                .ForMember(x => x.CompetitorOfferVMs, y => y.MapFrom(z => z.CompetitorOffers))
                .ForMember(x => x.CompetitorVMs, y => y.MapFrom(z => z.Competitors))
                .ForMember(x => x.CustomerConversationVMs, y => y.MapFrom(z => z.CustomerConversations))
                .ForMember(x => x.SupplierConversationVMs, y => y.MapFrom(z => z.SupplierConversations))
                .ForMember(x => x.SupplierNegtiationVMs, y => y.MapFrom(z => z.SupplierNegotiations))
                .ForMember(x => x.CustomerNegtiationVMs, y => y.MapFrom(z => z.CustomerNegotiations))
                .ForMember(x => x.NegotiationResultsVM, y => y.MapFrom(z => z.NegotiationResults))
                .ForMember(x => x.OtherNegotiationResultVMs, y => y.MapFrom(z => z.OtherNegotiationResults))
                .ForMember(x => x.PerformanceLGVM, y => y.MapFrom(z => z.PerformanceLG))
                .ForMember(x => x.FinalLGVM, y => y.MapFrom(z => z.FinalLG));
        }
    }
}
