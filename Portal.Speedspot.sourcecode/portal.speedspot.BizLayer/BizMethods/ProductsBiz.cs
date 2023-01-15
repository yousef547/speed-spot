using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class ProductsBiz : BizBaseT<Product>
    {
        private static IProductsRepository _repository;
        public ProductsBiz(IProductsRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public override async Task<IList<Product>> GetAllAsync()
        {
            return await Repository.GetAllAsync(x => x.Category.Department);
        }

        public async Task<IList<Product>> GetAllByUserAsync(int userDepartmentId, List<int> userDepartmentIds)
        {
            return await Repository.GetAllAsync(p => p.Category.DepartmentId == userDepartmentId || userDepartmentIds.Contains(p.Category.DepartmentId),
                x => x.Category.Department);
        }

        public override async Task<IList<Product>> GetAllArchivedAsync()
        {
            return await Repository.GetAllAsync(t => t.IsArchived == true, x => x.Category.Department);
        }

        public async Task<IList<Product>> GetAllArchivedByUserAsync(int userDepartmentId, List<int> userDepartmentIds)
        {
            return await Repository.GetAllAsync(p => p.IsArchived && (p.Category.DepartmentId == userDepartmentId || userDepartmentIds.Contains(p.Category.DepartmentId)),
                x => x.Category.Department);
        }

        public async Task<IList<Product>> GetByCategoryId(int categoryId)
        {
            return await Repository.GetAllAsync(p => p.CategoryId == categoryId);
        }

        public override async Task<Product> GetByIdAsync(int id)
        {
            return await Repository.GetFirstOrDefaultAsync(p => p.Id == id, x => x.Category.Department);
        }

        public async Task<IList<Product>> GetWithSuppliers(List<int> departmentIds, List<int> productIds)
        {
            var products = await _repository.GetWithSuppliers(departmentIds, productIds);

            return products;
        }

        public async Task<string> GetNewAutoCode(int categoryId)
        {
            var siblings = await Repository.GetAllAsync(c => c.CategoryId == categoryId);
            if (siblings.Count > 0)
            {
                string lastCode = (await Repository.GetAllAsync(c => c.CategoryId == categoryId)).OrderByDescending(o => o.AutoCode).FirstOrDefault().AutoCode;
                int codeInt = int.Parse(lastCode) + 1;
                return codeInt.ToString("D2");
            }
            return 1.ToString("D2");
        }
    }
}
