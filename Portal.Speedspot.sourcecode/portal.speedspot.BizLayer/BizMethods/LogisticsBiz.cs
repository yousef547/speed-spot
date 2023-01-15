using Microsoft.EntityFrameworkCore;

using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class LogisticsBiz : BizBaseT<Logistic>
    {
        private static ILogisticsRepository _repository;
        public LogisticsBiz(ILogisticsRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public void DeleteCompetitorDepartments(int id)
        {
            var departments = Repository.DbContext.LogisticDepartments.Where(d => d.LogisticId == id);
            Repository.DbContext.LogisticDepartments.RemoveRange(departments);
        }

        public override async Task<Logistic> GetByIdAsync(int id)
        {
            return await _repository.GetFullItemById(id);
        }
        public async Task<Logistic> GetNameByIdAsync(int id)
        {
            return await _repository.FindByIdAsync(id);
        }
        public async Task<IList<Logistic>> GetAllExceptAsync(int? id = null)
        {
            var filteredLogistics = await Repository.GetAllAsync(c => id == null || c.Id != id);
            return filteredLogistics;
        }

        public async Task<IList<Logistic>> GetAllExceptByUserAsync(string userId, int userDepartmentId, List<int> userDepartmentIds, int? id = null)
        {
            var filteredLogistics = await Repository.GetAllAsync(c => (id == null || c.Id != id) &&
           (c.Departments.Select(d => d.DepartmentId).Contains(userDepartmentId) ||
           c.Departments.Any(d => userDepartmentIds.Contains(d.DepartmentId)) ||
           c.CreatedById == userId),
           x => x.Departments);
            return filteredLogistics;
        }

        public async Task<IList<Logistic>> GetParents()
        {
            return await _repository.GetFullParents();
        }

        public async Task<IList<Logistic>> GetParentsByUserAsync(string userId, int userDepartmentId, List<int> userDepartmentIds)
        {
            return (await _repository.GetFullParents()).Where(c =>
            c.Departments.Select(d => d.DepartmentId).Contains(userDepartmentId) ||
            c.Departments.Any(d => userDepartmentIds.Contains(d.DepartmentId))).ToList();
        }

        public async Task<bool> UpdateBeneficiaryInfoAsync(int id, string BeneficiaryName, string BeneficiaryAddress, string userId)
        {
            var logistic = await Repository.FindByIdAsync(id);
            logistic.BeneficiaryName = BeneficiaryName;
            logistic.BeneficiaryAddress = BeneficiaryAddress;

            return await Repository.UpdateAsync(logistic, userId);
        }

        public async Task<IList<Logistic>> GetChildsAsync(int id)
        {
            return await Repository.DbContext.Logistics
                .Where(l => l.ParentId == id)
                .Include(x => x.CreatedBy)
                .Include(x => x.ActivityType)
                .ToListAsync();
        }
    }
}
