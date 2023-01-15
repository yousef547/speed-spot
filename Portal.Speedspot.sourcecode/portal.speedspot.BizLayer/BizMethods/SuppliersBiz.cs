using Microsoft.EntityFrameworkCore;

using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class SuppliersBiz : BizBaseT<Supplier>
    {
        private static ISuppliersRepository _repository;
        public SuppliersBiz(ISuppliersRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public void DeleteSupplierDepartments(int id)
        {
            var departments = Repository.DbContext.SupplierDepartments.Where(d => d.SupplierId == id);
            Repository.DbContext.SupplierDepartments.RemoveRange(departments);
        }

        public void DeleteSupplierClassifications(int id)
        {
            var classifications = Repository.DbContext.SupplierClassifications.Where(c => c.SupplierId == id);
            Repository.DbContext.SupplierClassifications.RemoveRange(classifications);
        }

        public void DeleteSupplierProducts(int id)
        {
            var products = Repository.DbContext.SupplierProducts.Where(c => c.SupplierId == id);
            Repository.DbContext.SupplierProducts.RemoveRange(products);
        }

        public override async Task<Supplier> GetByIdAsync(int id)
        {
            return await _repository.GetFullItemById(id);
        }
        public async Task<Supplier> GetNameByIdAsync(int id)
        {
            return await _repository.FindByIdAsync(id);
        }
        public async Task<Supplier> GetInfoByIdAsync(int id)
        {
            return await _repository.GetFirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IList<Supplier>> GetSelectedWithProductsAsync(List<int> supplierIds, List<int> productIds)
        {
            var suppliers = await _repository.GetSelectedWithProducts(supplierIds, productIds);
            return suppliers;
        }

        public async Task<Supplier> GetByIdWithProductsAsync(int supplierId, List<int> productIds)
        {
            var supplier = await _repository.GetByIdWithProducts(supplierId, productIds);
            return supplier;
        }

        public async Task<IList<Supplier>> GetAllExceptAsync(int? id = null)
        {
            var filteredSuppliers = await Repository.GetAllAsync(s => id == null || s.Id != id);
            return filteredSuppliers;
        }

        public async Task<IList<Supplier>> GetAllExceptByUserAsync(string userId, int userDepartmentId, List<int> userDepartmentIds, int? id = null)
        {
            var filteredSuppliers = await Repository.GetAllAsync(s => (id == null || s.Id != id) &&
           (s.Departments.Select(d => d.DepartmentId).Contains(userDepartmentId) ||
           s.Departments.Any(d => userDepartmentIds.Contains(d.DepartmentId))),
           x => x.Departments);
            return filteredSuppliers;
        }

        public async Task<IList<Supplier>> GetParents()
        {
            return await _repository.GetFullParents();
        }

        public async Task<IList<Supplier>> GetParentsByUserAsync(string userId, int userDepartmentId, List<int> userDepartmentIds)
        {
            return (await _repository.GetFullParents()).Where(c =>
            c.Departments.Select(d => d.DepartmentId).Contains(userDepartmentId) ||
            c.Departments.Any(d => userDepartmentIds.Contains(d.DepartmentId))).ToList();
        }

        public async Task<bool> UpdateBeneficiaryInfoAsync(int id, string BeneficiaryName, string BeneficiaryAddress, string userId)
        {
            var supplier = await Repository.FindByIdAsync(id);
            supplier.BeneficiaryName = BeneficiaryName;
            supplier.BeneficiaryAddress = BeneficiaryAddress;

            return await Repository.UpdateAsync(supplier, userId);
        }

        public async Task<IList<Supplier>> GetChildsAsync(int id)
        {
            return await Repository.DbContext.Suppliers
                .Where(s => s.ParentId == id)
                .Include(x => x.Products)
                .ThenInclude(y => y.Product)
                .Include(x => x.Address.City.Country)
                .ToListAsync();
        }
    }
}
