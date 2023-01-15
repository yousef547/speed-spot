using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using portal.speedspot.Models.BasesAndAbstracts;
using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.Concretes.Identity;
using portal.speedspot.Models.ViewModels;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static portal.speedspot.Models.Constants.ApplicationEnums;
using static portal.speedspot.Models.Constants.Permissions;

namespace portal.speedspot.DALRepositories
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual async Task<int> SaveChangesAsync(string userId = null)
        {
            OnBeforeSaveChanges(userId);
            var result = await base.SaveChangesAsync();
            return result;
        }

        private void OnBeforeSaveChanges(string userId)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                var auditEntry = new AuditEntry(entry)
                {
                    TableName = entry.Entity.GetType().Name,
                    UserId = userId
                };
                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }

            foreach (var auditEntry in auditEntries)
            {
                AuditLogs.Add(auditEntry.ToAudit());
            }
        }

        public DbSet<Audit> AuditLogs { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankBranch> BankBranches { get; set; }
        public DbSet<BidBond> BidBonds { get; set; }
        public DbSet<BookTender> BookTenders { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DeliveryType> DeliveryTypes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }
        public DbSet<OpportunityAttachment> OpportunityAttachments { get; set; }
        public DbSet<OpportunityStatus> OpportunityStatuses { get; set; }
        public DbSet<OpportunityType> OpportunityTypes { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<RequestOffer> RequestOffers { get; set; }
        public DbSet<RequestOfferProduct> RequestOfferProducts { get; set; }
        public DbSet<TechnicalSpecification> TechnicalSpecifications { get; set; }
        public DbSet<TechnicalSpecificationAttachment> TechnicalSpecificationAttachments { get; set; }
        public DbSet<TechnicalSpecificationProduct> TechnicalSpecificationProducts { get; set; }
        //public DbSet<ToDoTask> ToDoTasks { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<FavouriteType> FavouriteTypes { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<UserDepartment> UserDepartments { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<CustomerDepartment> CustomerDepartments { get; set; }
        public DbSet<CustomerContact> CustomerContacts { get; set; }
        public DbSet<CustomerEmployee> CustomerEmployees { get; set; }
        public DbSet<CustomerFile> CustomerFiles { get; set; }
        public DbSet<CustomerVendorRegistration> CustomerVendorRegistrations { get; set; }
        public DbSet<LegalInfo> LegalInfos { get; set; }
        public DbSet<OrganizationType> OrganizationTypes { get; set; }
        public DbSet<PartnerEmployee> PartnerEmployees { get; set; }
        public DbSet<CustomerAgent> CustomerAgents { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierOffer> SupplierOffers { get; set; }
        public DbSet<SupplierOfferProduct> SupplierOfferProducts { get; set; }
        public DbSet<SupplierOfferAttachment> SupplierOfferAttachments { get; set; }
        public DbSet<SupplierContact> SupplierContacts { get; set; }
        public DbSet<SupplierDepartment> SupplierDepartments { get; set; }
        public DbSet<SupplierEmployee> SupplierEmployees { get; set; }
        public DbSet<SupplierFile> SupplierFiles { get; set; }
        public DbSet<Classification> Classifications { get; set; }
        public DbSet<SupplierClassification> SupplierClassifications { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<SupplierCertificate> SupplierCertificates { get; set; }
        public DbSet<SupplierProduct> SupplierProducts { get; set; }
        public DbSet<Competitor> Competitors { get; set; }
        public DbSet<CompetitorDepartment> CompetitorDepartments { get; set; }
        public DbSet<CompetitorContact> CompetitorContacts { get; set; }
        public DbSet<CompetitorEmployee> CompetitorEmployees { get; set; }
        public DbSet<CompetitorFile> CompetitorFiles { get; set; }
        public DbSet<CompetitorProduct> CompetitorProducts { get; set; }
        public DbSet<BidBondRequest> BidBondRequests { get; set; }
        public DbSet<BookTenderRequest> BookTenderRequests { get; set; }
        public DbSet<ProductOrigin> ProductOrigins { get; set; }
        public DbSet<CustomerBank> CustomerBanks { get; set; }
        public DbSet<CustomerBankCurrency> CustomerBankCurrencies { get; set; }
        public DbSet<SupplierBank> SupplierBanks { get; set; }
        public DbSet<SupplierBankCurrency> SupplierBankCurrencies { get; set; }
        public DbSet<CompetitorBank> CompetitorBanks { get; set; }
        public DbSet<CompetitorBankCurrency> CompetitorBankCurrencies { get; set; }
        public DbSet<VisitReason> VisitReasons { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Logistic> Logistics { get; set; }
        public DbSet<LogisticBank> LogisticBanks { get; set; }
        public DbSet<LogisticBankCurrency> LogisticBankCurrencies { get; set; }
        public DbSet<LogisticContact> LogisticContacts { get; set; }
        public DbSet<LogisticDepartment> LogisticDepartments { get; set; }
        public DbSet<LogisticEmployee> LogisticEmployees { get; set; }
        public DbSet<LogisticFile> LogisticFiles { get; set; }
        public DbSet<CompanyData> CompanyData { get; set; }
        public DbSet<FinancialAccount> FinancialAccounts { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TechnicalSpecificationProductOrigin> TechnicalSpecificationProductOrigins { get; set; }
        public DbSet<OfferValidity> OfferValidities { get; set; }
        public DbSet<DeliveryPlace> DeliveryPlaces { get; set; }
        public DbSet<OriginDocument> OriginDocuments { get; set; }
        public DbSet<OfferCertificate> OfferCertificates { get; set; }
        public DbSet<QuotationStatus> QuotationStatuses { get; set; }
        public DbSet<GeneralRequest> GeneralRequests { get; set; }
        public DbSet<QuotationCurrency> QuotationCurrencies { get; set; }
        public DbSet<QuotationOfferCertificate> QuotationOfferCertificates { get; set; }
        public DbSet<QuotationOfferProductOrigin> QuotationOfferProductOrigins { get; set; }
        public DbSet<QuotationOfferProduct> QuotationOfferProducts { get; set; }
        public DbSet<QuotationOffer> QuotationOffers { get; set; }
        public DbSet<QuotationOfferVersion> QuotationOfferVersions { get; set; }
        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationUser> NotificationUsers { get; set; }
        public DbSet<GuaranteeTerm> GuaranteeTerms { get; set; }
        public DbSet<CompetitorOffer> CompetitorOffers { get; set; }
        public DbSet<CompetitorOfferProduct> CompetitorOfferProducts { get; set; }
        public DbSet<CompetitorOfferProductOrigin> CompetitorOfferProductOrigins { get; set; }
        public DbSet<CompetitorOfferCertificate> CompetitorOfferCertificates { get; set; }
        public DbSet<CustomerConversation> CustomerConversations { get; set; }
        public DbSet<SupplierConversation> SupplierConversations { get; set; }
        public DbSet<VATValue> VATValues { get; set; }
        public DbSet<RejectReason> RejectReasons { get; set; }
        public DbSet<QuotationCompetitor> QuotationCompetitors { get; set; }
        public DbSet<StickyNote> StickyNotes { get; set; }
        public DbSet<SupplierNegotiation> SupplierNegotiations { get; set; }
        public DbSet<CustomerNegotiation> CustomerNegotiations { get; set; }
        public DbSet<NegotiationResult> NegotiationResults { get; set; }
        public DbSet<OtherNegotiationResult> OtherNegotiationResults { get; set; }
        //public DbSet<LetterOfGuarantee> LetterOfGuarantees { get; set; }
        public DbSet<PerformanceLG> PerformanceLGs { get; set; }
        public DbSet<FinalLG> FinalLGs { get; set; }
        public DbSet<PerformanceLGRequest> PerformanceLGRequests { get; set; }
        public DbSet<FinalLGRequest> FinalLGRequests { get; set; }
        public DbSet<DepartmentBank> DepartmentBanks { get; set; }
        public DbSet<DepartmentBankCurrency> DepartmentBankCurrencies { get; set; }
        public DbSet<DepartmentDocument> DepartmentDocuments { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderSupplierOffer> PurchaseOrderSupplierOffers { get; set; }
        public DbSet<UserSupervisor> UserSupervisors { get; set; }
        public DbSet<BankFees> BankFees { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }
        public DbSet<CustomerPo> CustomerPos { get; set; }
        public DbSet<CustomerPoAttachment> CustomerPoAttachments { get; set; }
        public DbSet<SupplierPo> SupplierPos { get; set; }
        public DbSet<SupplierPoOffer> SupplierPoOffers { get; set; }
        public DbSet<AlternateVersion> AlternateVersions { get; set; }
        public DbSet<AlternateVersionCertificate> AlternateVersionCertificates { get; set; }
        public DbSet<AlternateVersionProduct> AlternateVersionProducts { get; set; }
        public DbSet<AlternateVersionProductOrigin> AlternateVersionProductOrigins { get; set; }
        public DbSet<PaymentRequest> PaymentRequests { get; set; }
        public DbSet<PaymentRequestDirection> PaymentRequestDirections { get; set; }
        public DbSet<FundDetails> FundDetails { get; set; }
        public DbSet<SupplierPayment> SupplierPayments { get; set; }
        public DbSet<PaymentDetails> PaymentDetails { get; set; }
        public DbSet<Treasury> Treasuries { get; set; }
        public DbSet<DepartmentSettings> DepartmentSettings { get; set; }
        public DbSet<TreasuryTransaction> TreasuryTransactions { get; set; }
        public DbSet<FinancialAccountTransaction> FinancialAccountTransactions { get; set; }
        public DbSet<PaymentTerm> PaymentTerms { get; set; }
        public DbSet<PerformaInvoice> PerformaInvoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });
        }
    }
}
