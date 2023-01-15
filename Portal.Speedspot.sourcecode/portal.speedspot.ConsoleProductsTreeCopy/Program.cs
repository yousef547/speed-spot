using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using portal.speedspot.BizLayer.BizMethods;
using portal.speedspot.DALRepositories;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.DALRepositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.ConsoleProductsTreeCopy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;

                    services.AddSingleton(typeof(IRepository<>), typeof(RepositoryBase<>));

                    services.AddSingleton<IProductsRepository, ProductsRepository>();
                    services.AddSingleton<ProductsBiz>();

                    services.AddSingleton<IProductCategoriesRepository, ProductCategoriesRepository>();
                    services.AddSingleton<ProductCategoriesBiz>();

                    services.AddSingleton<IDepartmentsRepository, DepartmentsRepository>();
                    services.AddSingleton<DepartmentsBiz>();

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                            sqlServerOptionsAction: sqlOptions =>
                            {
                                sqlOptions.EnableRetryOnFailure();
                            });
                    }, ServiceLifetime.Singleton);

                    services.AddHostedService<Worker>();
                });
    }
}
