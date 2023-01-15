using portal.speedspot.BizLayer.IdentityModule;
using portal.speedspot.DALRepositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using portal.speedspot.Models.Concretes.Identity;
using portal.speedspot.WebUI.Helpers;
using AutoMapper;
using portal.speedspot.WebUI.Infrastracture;
using System;
using System.Globalization;
using System.Collections.Generic;
using portal.speedspot.Resources.Resources;
using System.Reflection;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Repositories;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Helpers;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Http;
using portal.speedspot.WebUI.Hubs;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Localization;
using portal.speedspot.BizLayer.Services;

namespace portal.speedspot.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        private IServiceProvider ServiceProvider { get; set; }

        private IStringLocalizer _localizer = null;
        private IStringLocalizer Localizer
          => _localizer ??= ServiceProvider.GetService<IStringLocalizerFactory>()
                   .Create(nameof(SharedResources), new AssemblyName(typeof(SharedResources).Assembly.FullName).Name);

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Helper Services
            services.AddSingleton<LocalizationService>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<EmailSender>();
            services.AddScoped<ArabicLanguageHelper>();

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Custom Validations
            services.AddSingleton<IValidationAttributeAdapterProvider, CustomValidationAttributeAdapterProvider>();

            // Basic Configurations
            services.AddMvc(options => options.EnableEndpointRouting = false)
                .AddViewLocalization()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var assemblyName = new AssemblyName(typeof(SharedResources).GetTypeInfo().Assembly.FullName);
                        return factory.Create(nameof(SharedResources), assemblyName.Name);
                    };
                });
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var arabicCulture = new CultureInfo("ar-EG");
                arabicCulture.NumberFormat.NumberDecimalSeparator = ".";
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    arabicCulture
                };

                options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
            });

            services.AddControllersWithViews(options =>
            {
                static string f1(string f, string a1) => string.Format(f, a1);
                //static string f2(string f, string a1, string a2) => string.Format(f, a1, a2);

                var mp = options.ModelBindingMessageProvider;
                mp.SetValueMustBeANumberAccessor((x) => f1(Localizer["ValueMustBeANumber"], x));
            });
            //.AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            services.AddRazorPages();

            services.AddScoped<ApplicationUserManager>();
            services.AddScoped<ApplicationRoleManager>();

            // Repositories And Businesses
            services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));

            services.AddScoped<IAuditLogsRepository, AuditLogsRepository>();
            services.AddScoped<AuditLogsBiz>();

            services.AddScoped<IAttachmentsRepository, AttachmentsRepository>();
            services.AddScoped<AttachmentsBiz>();

            services.AddScoped<IBanksRepository, BanksRepository>();
            services.AddScoped<BanksBiz>();

            services.AddScoped<IBidBondsRepository, BidBondsRepository>();
            services.AddScoped<BidBondsBiz>();

            services.AddScoped<IBookTendersRepository, BookTendersRepository>();
            services.AddScoped<BookTendersBiz>();

            services.AddScoped<ICountriesRepository, CountriesRepository>();
            services.AddScoped<CountriesBiz>();

            services.AddScoped<ICitiesRepository, CitiesRepository>();
            services.AddScoped<CitiesBiz>();

            services.AddScoped<ICustomersRepository, CustomersRepository>();
            services.AddScoped<CustomersBiz>();

            services.AddScoped<IDepartmentsRepository, DepartmentsRepository>();
            services.AddScoped<DepartmentsBiz>();

            services.AddScoped<IEmployeeTypesRepository, EmployeeTypesRepository>();
            services.AddScoped<EmployeeTypesBiz>();

            services.AddScoped<IOpportunityStatusesRepository, OpportunityStatusesRepository>();
            services.AddScoped<OpportunityStatusesBiz>();

            services.AddScoped<IOpportunityTypesRepository, OpportunityTypesRepository>();
            services.AddScoped<OpportunityTypesBiz>();

            services.AddScoped<IOpportunitiesRepository, OpportunitiesRepository>();
            services.AddScoped<OpportunitiesBiz>();

            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<ProductsBiz>();

            services.AddScoped<ITechnicalSpecificationsRepository, TechnicalSpecificationsRepository>();
            services.AddScoped<TechnicalSpecificationsBiz>();

            services.AddScoped<ITechnicalSpecificationAttachmentsRepository, TechnicalSpecificationAttachmentsRepository>();
            services.AddScoped<TechnicalSpecificationAttachmentsBiz>();

            services.AddScoped<ITechnicalSpecificationProductsRepository, TechnicalSpecificationProductsRepository>();
            services.AddScoped<TechnicalSpecificationProductsBiz>();

            services.AddScoped<ISuppliersRepository, SuppliersRepository>();
            services.AddScoped<SuppliersBiz>();

            services.AddScoped<IRequestOffersRepository, RequestOffersRepository>();
            services.AddScoped<RequestOffersBiz>();

            services.AddScoped<ICurrenciesRepository, CurrenciesRepository>();
            services.AddScoped<CurrenciesBiz>();

            services.AddScoped<IDeliveryTypesRepository, DeliveryTypesRepository>();
            services.AddScoped<DeliveryTypesBiz>();

            services.AddScoped<ISupplierOffersRepository, SupplierOffersRepository>();
            services.AddScoped<SupplierOffersBiz>();

            services.AddScoped<IPaymentTypesRepository, PaymentTypesRepository>();
            services.AddScoped<PaymentTypesBiz>();

            //services.AddScoped<IToDoTasksRepository, ToDoTasksRepository>();
            //services.AddScoped<ToDoTasksBiz>();

            services.AddScoped<IQuotationStatusesRepository, QuotationStatusesRepository>();
            services.AddScoped<QuotationStatusesBiz>();

            services.AddScoped<IQuotationsRepository, QuotationsRepository>();
            services.AddScoped<QuotationsBiz>();

            services.AddScoped<IFavouriteTypesRepository, FavouriteTypesRepository>();
            services.AddScoped<FavouriteTypesBiz>();

            services.AddScoped<IFavouritesRepository, FavouritesRepository>();
            services.AddScoped<FavouritesBiz>();

            services.AddScoped<IProductCategoriesRepository, ProductCategoriesRepository>();
            services.AddScoped<ProductCategoriesBiz>();

            services.AddScoped<IUserDepartmentsRepository, UserDepartmentsRepository>();
            services.AddScoped<UserDepartmentsBiz>();

            services.AddScoped<IActivityTypesRepository, ActivityTypesRepository>();
            services.AddScoped<ActivityTypesBiz>();

            services.AddScoped<IOrganizationTypesRepository, OrganizationTypesRepository>();
            services.AddScoped<OrganizationTypesBiz>();

            services.AddScoped<IContactTypesRepository, ContactTypesRepository>();
            services.AddScoped<ContactTypesBiz>();

            services.AddScoped<IContactsRepository, ContactsRepository>();
            services.AddScoped<ContactsBiz>();

            services.AddScoped<IPartnerEmployeesRepository, PartnerEmployeesRepository>();
            services.AddScoped<PartnerEmployeesBiz>();

            services.AddScoped<IClassificationsRepository, ClassificationsRepository>();
            services.AddScoped<ClassificationsBiz>();

            services.AddScoped<ICertificatesRepository, CertificatesRepository>();
            services.AddScoped<CertificatesBiz>();

            services.AddScoped<ISupplierProductsRepository, SupplierProductsRepository>();
            services.AddScoped<SupplierProductsBiz>();

            services.AddScoped<ISupplierFilesRepository, SupplierFilesRepository>();
            services.AddScoped<SupplierFilesBiz>();

            services.AddScoped<ICompetitorsRepository, CompetitorsRepository>();
            services.AddScoped<CompetitorsBiz>();

            services.AddScoped<ICompetitorProductsRepository, CompetitorProductsRepository>();
            services.AddScoped<CompetitorProductsBiz>();

            services.AddScoped<ICompetitorFilesRepository, CompetitorFilesRepository>();
            services.AddScoped<CompetitorFilesBiz>();

            services.AddScoped<IBidBondRequestsRepository, BidBondRequestsRepository>();
            services.AddScoped<BidBondRequestsBiz>();

            services.AddScoped<IBookTenderRequestsRepository, BookTenderRequestsRepository>();
            services.AddScoped<BookTenderRequestsBiz>();

            services.AddScoped<IProductOriginsRepository, ProductOriginsRepository>();
            services.AddScoped<ProductOriginsBiz>();

            services.AddScoped<ISupplierOfferAttachmentsRepository, SupplierOfferAttachmentsRepository>();
            services.AddScoped<SupplierOfferAttachmentsBiz>();

            services.AddScoped<ICustomerBanksRepository, CustomerBanksRepository>();
            services.AddScoped<CustomerBanksBiz>();

            services.AddScoped<ISupplierBanksRepository, SupplierBanksRepository>();
            services.AddScoped<SupplierBanksBiz>();

            services.AddScoped<IVisitReasonsRepository, VisitReasonsRepository>();
            services.AddScoped<VisitReasonsBiz>();

            services.AddScoped<IVisitsRepository, VisitsRepository>();
            services.AddScoped<VisitsBiz>();

            services.AddScoped<ITasksRepository, TasksRepository>();
            services.AddScoped<TasksBiz>();

            services.AddScoped<ICompetitorBanksRepository, CompetitorBanksRepository>();
            services.AddScoped<CompetitorBanksBiz>();

            services.AddScoped<ILogisticsRepository, LogisticsRepository>();
            services.AddScoped<LogisticsBiz>();

            services.AddScoped<ILogisticBanksRepository, LogisticBanksRepository>();
            services.AddScoped<LogisticBanksBiz>();

            services.AddScoped<ICompanyDataRepository, CompanyDataRepository>();
            services.AddScoped<CompanyDataBiz>();

            services.AddScoped<IFinancialAccountsRepository, FinancialAccountsRepository>();
            services.AddScoped<FinancialAccountsBiz>();

            services.AddScoped<IProjectsRepository, ProjectsRepository>();
            services.AddScoped<ProjectsBiz>();

            services.AddScoped<IQuotationCurrenciesRepository, QuotationCurrenciesRepository>();
            services.AddScoped<QuotationCurrenciesBiz>();

            services.AddScoped<IGeneralRequestsRepository, GeneralRequestsRepository>();
            services.AddScoped<GeneralRequestsBiz>();

            services.AddScoped<IOfferValiditiesRepository, OfferValiditiesRepository>();
            services.AddScoped<OfferValiditiesBiz>();

            services.AddScoped<IDeliveryPlacesRepository, DeliveryPlacesRepository>();
            services.AddScoped<DeliveryPlacesBiz>();

            services.AddScoped<IOriginDocumentsRepository, OriginDocumentsRepository>();
            services.AddScoped<OriginDocumentsBiz>();

            services.AddScoped<IOfferCertificatesRepository, OfferCertificatesRepository>();
            services.AddScoped<OfferCertificatesBiz>();

            services.AddScoped<IQuotationOffersRepository, QuotationOffersRepository>();
            services.AddScoped<QuotationOffersBiz>();

            services.AddScoped<IQuotationOfferVersionsRepository, QuotationOfferVersionsRepository>();
            services.AddScoped<QuotationOfferVersionsBiz>();

            services.AddScoped<IGuaranteeTermsRepository, GuaranteeTermsRepository>();
            services.AddScoped<GuaranteeTermsBiz>();

            services.AddScoped<INotificationsRepository, NotificationsRepository>();
            services.AddScoped<NotificationsBiz>();

            services.AddScoped<INotificationUsersRepository, NotificationUsersRepository>();
            services.AddScoped<NotificationUsersBiz>();

            services.AddScoped<ICompetitorOffersRepository, CompetitorOffersRepository>();
            services.AddScoped<CompetitorOffersBiz>();

            services.AddScoped<ISupplierConversationsRepository, SupplierConversationsRepository>();
            services.AddScoped<SupplierConversationsBiz>();

            services.AddScoped<ICustomerConversationsRepository, CustomerConversationsRepository>();
            services.AddScoped<CustomerConversationsBiz>();

            services.AddScoped<IVATValuesRepository, VATValuesRepository>();
            services.AddScoped<VATValuesBiz>();

            services.AddScoped<IRejectReasonsRepository, RejectReasonsRepository>();
            services.AddScoped<RejectReasonsBiz>();

            services.AddScoped<IQuotationCompetitorsRepository, QuotationCompetitorsRepository>();
            services.AddScoped<QuotationCompetitorsBiz>();

            services.AddScoped<IStickyNotesRepository, StickyNotesRepository>();
            services.AddScoped<StickyNotesBiz>();

            services.AddScoped<ISupplierNegotiationsRepository, SupplierNegotiationsRepositery>();
            services.AddScoped<SupplierNegotiationBiz>();

            services.AddScoped<ICustomerNegotiationsRepository, CustomerNegotiationsRepository>();
            services.AddScoped<CustomerNegotiationBiz>();

            services.AddScoped<INegotiationResultsRepository, NegotiationResultsRepository>();
            services.AddScoped<NegotiationResultsBiz>();

            services.AddScoped<IOtherNegotiationResultsRepository, OtherNegotiationResultsRepository>();
            services.AddScoped<OtherNegotiationResultsBiz>();

            services.AddScoped<IPerformanceLGsRepository, PerformanceLGsRepository>();
            services.AddScoped<PerformanceLGsBiz>();

            services.AddScoped<IPerformanceLGRequestsRepository, PerformanceLGRequestsRepository>();
            services.AddScoped<PerformanceLGRequestsBiz>();

            services.AddScoped<IFinalLGsRepository, FinalLGsRepository>();
            services.AddScoped<FinalLGsBiz>();

            services.AddScoped<IFinalLGRequestsRepository, FinalLGRequestsRepository>();
            services.AddScoped<FinalLGRequestsBiz>();

            services.AddScoped<IDepartmentBanksRepository, DepartmentBanksRepository>();
            services.AddScoped<DepartmentBanksBiz>();

            services.AddScoped<IDepartmentDocumentRepositery, DepartmentDocumentRepositery>();
            services.AddScoped<DepartmentDocumentBiz>();

            services.AddScoped<IPurchaseOrdersRepository, PurchaseOrdersRepository>();
            services.AddScoped<PurchaseOrdersBiz>();

            services.AddScoped<IUserSupervisorsRepository, UserSupervisorsRepository>();
            services.AddScoped<UserSupervisorsBiz>();

            services.AddScoped<IUserActivitiesRepository, UserActivitiesRepository>();
            services.AddScoped<UserActivitiesBiz>();

            services.AddScoped<IAlternateVersionsRepository, AlternateVersionsRepository>();
            services.AddScoped<AlternateVersionsBiz>();

            services.AddScoped<ICustomerPoRepositery, CustomerPosRepositery>();
            services.AddScoped<CustomerPoBiz>();

            services.AddScoped<ISupplierPoRepositery, SupplierPosRepositery>();
            services.AddScoped<SupplierPoBiz>();

            services.AddScoped<IFundDetailsRepositery, FundDetailsRepositery>();
            services.AddScoped<FundDetailsBiz>();

            services.AddScoped<IPaymentRequestsRepository, PaymentRequestsRepository>();
            services.AddScoped<PaymentRequestsBiz>();

            services.AddScoped<ITreasuriesRepository, TreasuriesRepository>();
            services.AddScoped<TreasuriesBiz>();

            services.AddScoped<IDepartmentSettingsRepository, DepartmentSettingsRepository>();
            services.AddScoped<DepartmentSettingsBiz>();

            services.AddScoped<IFinancialAccountTransactionsRepository, FinancialAccountTransactionsRepository>();
            services.AddScoped<FinancialAccountTransactionsBiz>();

            services.AddScoped<ITreasuryTransactionsRepository, TreasuryTransactionsRepository>();
            services.AddScoped<TreasuryTransactionsBiz>();

            services.AddScoped<IPaymentTermsRepository, PaymentTermsRepository>();
            services.AddScoped<PaymentTermsBiz>();

            services.AddScoped<IPerformaInvoicesRepository, PerformaInvoicesRepository>();
            services.AddScoped<PerformaInvoicesBiz>();

            //////////////////////////////////////////////////////////////////
            services.AddSingleton<IConnectionManager, ConnectionManager>();
            services.AddScoped<NotificationHub>();
            services.AddScoped<IHubNotificationHelper, HubNotificationHelper>();

            // Add main DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure();
                    });
            });

            // Add a DbContext to store your Database Keys
            services.AddDbContext<UserKeysContext>(options => options.UseSqlServer(Configuration["DataProtection:SqlServerConnectionString"]));

            // using Microsoft.AspNetCore.DataProtection;
            services.AddDataProtection()
                .SetApplicationName("SpeedSpot")
                .SetDefaultKeyLifetime(TimeSpan.FromDays(14))
                .PersistKeysToDbContext<UserKeysContext>();

            // For Identity  
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddRoles<ApplicationRole>()
                .AddDefaultTokenProviders();


            services.Configure<IdentityOptions>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 1;

                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = true;

                    options.Lockout.AllowedForNewUsers = true;
                    options.Lockout.MaxFailedAccessAttempts = 3;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.Expiration = TimeSpan.FromDays(1);
                    options.Cookie.MaxAge = TimeSpan.FromDays(1);
                    options.ExpireTimeSpan = TimeSpan.FromDays(1);
                    options.SlidingExpiration = true;
                });

            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });

            services.AddScoped<NumberJustificationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ServiceProvider = app.ApplicationServices;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //    name: "areaRoute",
                //    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<NotificationHub>("/notificationHub");
            });
        }
    }
}
