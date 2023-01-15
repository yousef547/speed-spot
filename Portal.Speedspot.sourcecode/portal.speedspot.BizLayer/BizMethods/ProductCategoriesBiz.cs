using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class ProductCategoriesBiz : BizBaseT<ProductCategory>
    {
        public ProductCategoriesBiz(IProductCategoriesRepository repository) : base(repository)
        {

        }

        public override async Task<IList<ProductCategory>> GetAllAsync()
        {
            return await Repository.GetAllAsync(x => x.Department);
        }

        public async Task<IList<ProductCategory>> GetCategoriesWithChilds()
        {
            return await Repository.GetAllAsync(c => c.IsArchived == false,
                x => x.Childs.Where(c => c.IsArchived == false),
                x => x.Products.Where(p => p.IsArchived == false));
        }

        public async Task<IList<ProductCategory>> GetCategoriesWithChilds(List<int> departmentIds, bool isService)
        {
            return await Repository.GetAllAsync(c => c.IsArchived == false &&
            c.IsService == isService &&
            departmentIds.Contains(c.DepartmentId),
            x => x.Childs.Where(c => c.IsArchived == false),
            x => x.Products.Where(p => p.IsArchived == false),
            x => x.Department);
        }

        public async Task<IList<ProductCategory>> GetByDepartmentId(int departmentId, bool isService)
        {
            return await Repository.GetAllAsync(c => c.DepartmentId == departmentId && c.IsService == isService);
        }

        public async Task<IList<ProductCategory>> GetAllByUserAsync(int userDepartmentId, List<int> userDepartmentIds)
        {
            return (await Repository.GetAllAsync(d => d.DepartmentId == userDepartmentId || userDepartmentIds.Contains(d.DepartmentId),
            x => x.Department))
            .ToList();
        }

        public override async Task<IList<ProductCategory>> GetAllArchivedAsync()
        {
            return await Repository.GetAllAsync(t => t.IsArchived == true, x => x.Department);
        }

        public async Task<IList<ProductCategory>> GetAllArchivedByUserAsync(int userDepartmentId, List<int> userDepartmentIds)
        {
            return (await Repository.GetAllAsync(d => d.IsArchived && (d.DepartmentId == userDepartmentId || userDepartmentIds.Contains(d.DepartmentId)),
            x => x.Department))
            .ToList();
        }

        public async Task<IList<ProductCategory>> GetParentsByDepartmentId(int departmentId)
        {
            return await Repository.GetAllAsync(c => c.IsArchived == false && c.DepartmentId == departmentId && c.ParentId == null);
        }

        public async Task<IList<ProductCategory>> GetParentsByDepartmentId(int departmentId, bool isService)
        {
            return await Repository.GetAllAsync(c => c.IsArchived == false &&
            c.DepartmentId == departmentId &&
            c.ParentId == null &&
            c.IsService == isService,
                x => x.Department);
        }

        public override async Task<ProductCategory> GetByIdAsync(int id)
        {
            return await Repository.GetFirstOrDefaultAsync(c => c.Id == id, x => x.Department);
        }

        public async Task<string> GetNewParentAutoCode(int departmentId)
        {
            var siblings = await Repository.GetAllAsync(c => c.DepartmentId == departmentId && c.ParentId == null);
            if (siblings.Count > 0)
            {
                string lastCode = (await Repository.GetAllAsync(c => c.DepartmentId == departmentId && c.ParentId == null)).OrderByDescending(o => o.AutoCode).FirstOrDefault().AutoCode;
                int codeInt = int.Parse(lastCode) + 1;
                return codeInt.ToString("D2");
            }
            return 1.ToString("D2");
        }

        public async Task<string> GenerateNewParentCode(int departmentId)
        {
            string autoCode = await GetNewParentAutoCode(departmentId);
            string departmentCode = Repository.DbContext.Departments.FirstOrDefault(d => d.Id == departmentId).Code;
            return departmentCode + autoCode;
        }

        public async Task<string> GetNewAutoCode(int parentId)
        {
            var siblings = await Repository.GetAllAsync(c => c.ParentId == parentId);
            if (siblings.Count > 0)
            {
                string lastCode = (await Repository.GetAllAsync(c => c.ParentId == parentId)).OrderByDescending(o => o.AutoCode).FirstOrDefault().AutoCode;
                int codeInt = int.Parse(lastCode) + 1;
                return codeInt.ToString("D2");
            }
            return 1.ToString("D2");
        }
    }
}
