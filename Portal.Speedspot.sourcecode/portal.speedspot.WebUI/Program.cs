using portal.speedspot.BizLayer.IdentityModule;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using portal.speedspot.WebUI.Seeds;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.DALRepositories;
using Microsoft.EntityFrameworkCore;

namespace portal.speedspot.WebUI
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                var isDevelopment = environment == Environments.Development;
                if (!isDevelopment)
                {
                    db.Database.Migrate();
                }

                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("app");
                try
                {
                    ApplicationUserManager userManager = services.GetRequiredService<ApplicationUserManager>();
                    ApplicationRoleManager roleManager = services.GetRequiredService<ApplicationRoleManager>();
                    EmployeeTypesBiz employeeTypesBiz = services.GetRequiredService<EmployeeTypesBiz>();
                    OpportunityStatusesBiz opportunityStatusesBiz = services.GetRequiredService<OpportunityStatusesBiz>();
                    OpportunityTypesBiz opportunityTypesBiz = services.GetRequiredService<OpportunityTypesBiz>();
                    DepartmentsBiz departmentsBiz = services.GetRequiredService<DepartmentsBiz>();
                    QuotationStatusesBiz quotationStatusesBiz = services.GetRequiredService<QuotationStatusesBiz>();
                    FavouriteTypesBiz favouriteTypesBiz = services.GetRequiredService<FavouriteTypesBiz>();
                    CustomersBiz customersBiz = services.GetRequiredService<CustomersBiz>();
                    ProductsBiz productsBiz = services.GetRequiredService<ProductsBiz>();
                    SuppliersBiz suppliersBiz = services.GetRequiredService<SuppliersBiz>();
                    BanksBiz banksBiz = services.GetRequiredService<BanksBiz>();
                    CountriesBiz countriesBiz = services.GetRequiredService<CountriesBiz>();
                    CurrenciesBiz currenciesBiz = services.GetRequiredService<CurrenciesBiz>();
                    DeliveryTypesBiz deliveryTypesBiz = services.GetRequiredService<DeliveryTypesBiz>();
                    PaymentTypesBiz paymentTypesBiz = services.GetRequiredService<PaymentTypesBiz>();
                    ProductCategoriesBiz productCategoriesBiz = services.GetRequiredService<ProductCategoriesBiz>();
                    ActivityTypesBiz activityTypesBiz = services.GetRequiredService<ActivityTypesBiz>();
                    OrganizationTypesBiz organizationTypesBiz = services.GetRequiredService<OrganizationTypesBiz>();
                    ContactTypesBiz contactTypesBiz = services.GetRequiredService<ContactTypesBiz>();
                    ClassificationsBiz classificationsBiz = services.GetRequiredService<ClassificationsBiz>();
                    VisitReasonsBiz visitReasonsBiz = services.GetRequiredService<VisitReasonsBiz>();
                    CompanyDataBiz companyDataBiz = services.GetRequiredService<CompanyDataBiz>();
                    OfferValiditiesBiz offerValiditiesBiz = services.GetRequiredService<OfferValiditiesBiz>();
                    DeliveryPlacesBiz deliveryPlacesBiz = services.GetRequiredService<DeliveryPlacesBiz>();
                    OriginDocumentsBiz originDocumentsBiz = services.GetRequiredService<OriginDocumentsBiz>();
                    OfferCertificatesBiz offerCertificatesBiz = services.GetRequiredService<OfferCertificatesBiz>();
                    GuaranteeTermsBiz guaranteeTermsBiz = services.GetRequiredService<GuaranteeTermsBiz>();
                    VATValuesBiz vatValuesBiz = services.GetRequiredService<VATValuesBiz>();

                    await DefaultData.SeedDefaultData(userManager, roleManager, employeeTypesBiz,
                        opportunityTypesBiz, opportunityStatusesBiz, departmentsBiz,
                        quotationStatusesBiz, favouriteTypesBiz, customersBiz,
                        productsBiz, suppliersBiz, banksBiz,
                        countriesBiz, currenciesBiz, deliveryTypesBiz,
                        paymentTypesBiz, productCategoriesBiz, activityTypesBiz,
                        organizationTypesBiz, contactTypesBiz, classificationsBiz,
                        visitReasonsBiz, companyDataBiz, offerValiditiesBiz,
                        deliveryPlacesBiz, originDocumentsBiz, offerCertificatesBiz,
                        guaranteeTermsBiz, vatValuesBiz);
                    logger.LogInformation("Finished Seeding Default Data");
                    logger.LogInformation("Application Starting");
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "An error occurred seeding the DB");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
