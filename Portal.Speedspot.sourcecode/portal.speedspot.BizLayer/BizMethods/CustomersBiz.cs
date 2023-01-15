using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class CustomersBiz : BizBaseT<Customer>
    {
        private static ICustomersRepository _repository;
        public CustomersBiz(ICustomersRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public void DeleteCustomerDepartments(int id)
        {
            var departments = Repository.DbContext.CustomerDepartments.Where(d => d.CustomerId == id);
            Repository.DbContext.CustomerDepartments.RemoveRange(departments);
        }

        public void DeleteCustomerAgents(int id)
        {
            var salesAgents = Repository.DbContext.CustomerAgents.Where(sa => sa.CustomerId == id);
            Repository.DbContext.CustomerAgents.RemoveRange(salesAgents);
        }

        public override async Task<Customer> GetByIdAsync(int id)
        {
            return await _repository.GetFullItemById(id);
        }
        public async Task<Customer> GetInfoByIdAsync(int id)
        {
            return await _repository.FindByIdAsync(id);
        }
        public async Task<IList<Customer>> GetParents()
        {
            return await Repository.GetAllAsync(c => c.ParentId == null, x => x.Childs, x => x.CreatedBy);
        }

        public async Task<IList<Customer>> GetParentsByUserAsync(string userId, int userDepartmentId, List<int> userDepartmentIds)
        {
            return await Repository.GetAllAsync(c => c.ParentId == null &&
            (c.Departments.Select(d => d.DepartmentId).Contains(userDepartmentId) ||
            c.Departments.Any(d => userDepartmentIds.Contains(d.DepartmentId)) ||
            c.CreatedById == userId) ||
            c.SalesAgents.Any(x => x.SalesAgentId == userId),
            x => x.Departments,
            x => x.Childs,
            x => x.CreatedBy,
            x => x.SalesAgents);
        }

        public async Task<IList<Customer>> GetAllUnarchivedByUserAsync(string userId, int userDepartmentId, List<int> userDepartmentIds)
        {
            var filteredCustomers = await Repository.GetAllAsync(c => c.IsArchived == false &&
            (c.Departments.Select(d => d.DepartmentId).Contains(userDepartmentId) ||
            c.Departments.Any(d => userDepartmentIds.Contains(d.DepartmentId)) ||
            c.CreatedById == userId) ||
            c.SalesAgents.Any(x => x.SalesAgentId == userId),
            x => x.Departments,
            x => x.SalesAgents);
            return filteredCustomers;
        }

        public async Task<IList<Customer>> GetAllExceptAsync(int? id = null)
        {
            var filteredCustomers = await Repository.GetAllAsync(c => id == null || c.Id != id);
            return filteredCustomers;
        }

        public async Task<IList<Customer>> GetAllExceptByUserAsync(string userId, int userDepartmentId, List<int> userDepartmentIds, int? id = null)
        {
            var filteredCustomers = await Repository.GetAllAsync(c => (id == null || c.Id != id) &&
           (c.Departments.Select(d => d.DepartmentId).Contains(userDepartmentId) ||
           c.Departments.Any(d => userDepartmentIds.Contains(d.DepartmentId)) ||
           c.CreatedById == userId) ||
            c.SalesAgents.Any(x => x.SalesAgentId == userId),
           x => x.Departments,
           x => x.SalesAgents);
            return filteredCustomers;
        }

        public async Task<bool> UpdateBeneficiaryInfoAsync(int id, string BeneficiaryName, string BeneficiaryAddress, string userId)
        {
            var customer = await Repository.FindByIdAsync(id);
            customer.BeneficiaryName = BeneficiaryName;
            customer.BeneficiaryAddress = BeneficiaryAddress;

            return await Repository.UpdateAsync(customer, userId);
        }

        public async Task<IList<Customer>> GetChildsAsync(int id)
        {
            return await Repository.GetAllAsync(c => c.ParentId == id,x=>x.CreatedBy);
        }
    }
}
