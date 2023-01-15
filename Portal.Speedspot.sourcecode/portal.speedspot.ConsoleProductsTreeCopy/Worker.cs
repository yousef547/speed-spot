using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using portal.speedspot.BizLayer.BizMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace portal.speedspot.ConsoleProductsTreeCopy
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ProductCategoriesBiz _productCategoriesBiz;
        private readonly DepartmentsBiz _departmentsBiz;
        private readonly ProductsBiz _productsBiz;

        public Worker(ILogger<Worker> logger, ProductCategoriesBiz productCategoriesBiz, DepartmentsBiz departmentsBiz, ProductsBiz productsBiz)
        {
            _logger = logger;
            _productCategoriesBiz = productCategoriesBiz;
            _departmentsBiz = departmentsBiz;
            _productsBiz = productsBiz;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
        Start:
            Console.WriteLine("Please type the number you want to execute its function then press Enter.");
            Console.WriteLine("1- Copy Products Tree from a Department to another");
            Console.WriteLine("2- Auto generate Products & Categories code");
            string functionNumber = Console.ReadLine();
            bool result = int.TryParse(functionNumber, out int functionNo);
            if (!result)
            {
                Console.WriteLine("The entered function number is not valid");
                goto Start;
            }
            switch (functionNo)
            {
                case 1:
                    goto FunctionOne;
                case 2:
                    goto FunctionTwo;
            }

        FunctionOne:
            {
                Console.WriteLine("Please enter the source department id");
                string departmentFrom = Console.ReadLine();
                result = int.TryParse(departmentFrom, out int departmentFromId);
                if (!result)
                {
                    Console.WriteLine("The entered department id is not valid");
                    goto FunctionOne;
                }

                var sourceDepartment = await _departmentsBiz.GetByIdAsync(departmentFromId);
                if (sourceDepartment == null)
                {
                    Console.WriteLine("The entered department id is not valid");
                    goto FunctionOne;
                }

                var categories = await _productCategoriesBiz.GetParentsByDepartmentId(departmentFromId);

            TargetDepartment:
                Console.WriteLine("Please enter the target department id");
                string departmentTo = Console.ReadLine();
                result = int.TryParse(departmentTo, out int departmentToId);
                if (!result)
                {
                    Console.WriteLine("The entered department id is not valid");
                    goto TargetDepartment;
                }
                var targetDepartment = await _departmentsBiz.GetByIdAsync(departmentToId);
                if (targetDepartment == null)
                {
                    Console.WriteLine("The entered department id is not valid");
                    goto TargetDepartment;
                }

                Console.WriteLine("Please wait...");

                int categoryCount = 0;
                int productCount = 0;
                foreach (var category in categories)
                {
                    var newCategory = new Models.Concretes.ProductCategory
                    {
                        Code = category.Code,
                        DepartmentId = departmentToId,
                        IsService = category.IsService,
                        Name = category.Name,
                        NameAr = category.NameAr,
                        ParentId = null
                    };

                    bool resultAdd = await _productCategoriesBiz.AddAsync(newCategory);
                    if (resultAdd)
                    {
                        categoryCount++;
                        productCount += await InsertProducts(category.Products, newCategory.Id);
                        Tuple<int, int> counts = await InsertChilds(category.Childs, newCategory.Id, departmentToId);
                        productCount += counts.Item1;
                        categoryCount += counts.Item2;
                    }
                }

                Console.WriteLine($"Categories count: {categoryCount}");
                Console.WriteLine($"Products count: {productCount}");
                goto Start;
            }

        FunctionTwo:
            {
                Console.WriteLine("Please enter the department id");
                string departmentIdStr = Console.ReadLine();
                result = int.TryParse(departmentIdStr, out int departmentId);
                if (!result)
                {
                    Console.WriteLine("The entered department id is not valid");
                    goto FunctionTwo;
                }
                Console.WriteLine("Please wait...");
                var categories = await _productCategoriesBiz.GetParentsByDepartmentId(departmentId);
                for (int i = 0; i < categories.Count; i++)
                {
                    var category = categories[i];
                    category.AutoCode = (i + 1).ToString("D2");
                    await GenerateChildsCode(category.Childs);
                    await GenerateProductsCode(category.Products);
                    await _productCategoriesBiz.UpdateAsync(category);
                }
                Console.WriteLine("Operation Done");
                goto Start;
            }
        }

        public async Task<Tuple<int, int>> InsertChilds(IList<Models.Concretes.ProductCategory> childs, int parentId, int departmentId)
        {
            int categoryCount = 0;
            int productCount = 0;
            foreach (var category in childs)
            {
                var newCategory = new Models.Concretes.ProductCategory
                {
                    Code = category.Code,
                    DepartmentId = departmentId,
                    IsService = false,
                    Name = category.Name,
                    NameAr = category.NameAr,
                    ParentId = parentId
                };

                bool resultAdd = await _productCategoriesBiz.AddAsync(newCategory);
                if (resultAdd)
                {
                    categoryCount++;
                    productCount += await InsertProducts(category.Products, newCategory.Id);
                    Tuple<int, int> counts = await InsertChilds(category.Childs, newCategory.Id, departmentId);
                    productCount += counts.Item1;
                    categoryCount += counts.Item2;
                }
            }

            return new Tuple<int, int>(productCount, categoryCount);
        }

        public async Task<int> InsertProducts(IList<Models.Concretes.Product> products, int categoryId)
        {
            int count = 0;
            foreach (var product in products)
            {
                var newProduct = new Models.Concretes.Product
                {
                    CategoryId = categoryId,
                    Code = product.Code,
                    Name = product.Name,
                    NameAr = product.NameAr
                };

                bool resultAdd = await _productsBiz.AddAsync(newProduct);
                if (resultAdd)
                {
                    count++;
                }
            }

            return count;
        }

        public async Task GenerateChildsCode(IList<Models.Concretes.ProductCategory> childs)
        {
            for (int i = 0; i < childs.Count; i++)
            {
                var category = childs[i];
                category.AutoCode = (i + 1).ToString("D2");
                await GenerateChildsCode(category.Childs);
                await GenerateProductsCode(category.Products);
                await _productCategoriesBiz.UpdateAsync(category);
            }
        }

        public async Task GenerateProductsCode(IList<Models.Concretes.Product> products)
        {
            for (int i = 0; i < products.Count; i++)
            {
                var product = products[i];
                product.AutoCode = (i + 1).ToString("D2");
                await _productsBiz.UpdateAsync(product);
            }
        }
    }
}
