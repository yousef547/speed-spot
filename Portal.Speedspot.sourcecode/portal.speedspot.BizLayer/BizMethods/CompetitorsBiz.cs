using Microsoft.EntityFrameworkCore;

using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class CompetitorsBiz : BizBaseT<Competitor>
    {
        private static ICompetitorsRepository _repository;
        public CompetitorsBiz(ICompetitorsRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public void DeleteCompetitorDepartments(int id)
        {
            var departments = Repository.DbContext.CompetitorDepartments.Where(d => d.CompetitorId == id);
            Repository.DbContext.CompetitorDepartments.RemoveRange(departments);
        }

        public void DeleteCompetitorProducts(int id)
        {
            var products = Repository.DbContext.CompetitorProducts.Where(c => c.CompetitorId == id);
            Repository.DbContext.CompetitorProducts.RemoveRange(products);
        }

        public override async Task<Competitor> GetByIdAsync(int id)
        {
            return await _repository.GetFullItemById(id);
        }
        public async Task<Competitor> GetNameByIdAsync(int id)
        {
            return await _repository.FindByIdAsync(id);
        }
        public async Task<IList<Competitor>> GetAllExceptAsync(int? id = null)
        {
            var filteredCompetitors = await Repository.GetAllAsync(s => id == null || s.Id != id);
            return filteredCompetitors;
        }

        public async Task<IList<Competitor>> GetAllExceptByUserAsync(string userId, int userDepartmentId, List<int> userDepartmentIds, int? id = null)
        {
            var filteredCompetitors = await Repository.GetAllAsync(s => (id == null || s.Id != id) &&
           (s.Departments.Select(d => d.DepartmentId).Contains(userDepartmentId) ||
           s.Departments.Any(d => userDepartmentIds.Contains(d.DepartmentId))),
           x => x.Departments);
            return filteredCompetitors;
        }

        public override async Task<IList<Competitor>> GetAllAsync()
        {
            return await Repository.GetAllAsync(x => x.CreatedBy);
        }

        public async Task<IList<Competitor>> GetParents()
        {
            return await _repository.GetFullParents();
        }

        public async Task<IList<Competitor>> GetParentsByUserAsync(string userId, int userDepartmentId, List<int> userDepartmentIds)
        {
            return (await _repository.GetFullParents()).Where(c =>
            c.Departments.Select(d => d.DepartmentId).Contains(userDepartmentId) ||
            c.Departments.Any(d => userDepartmentIds.Contains(d.DepartmentId))).ToList();
        }

        public async Task<bool> UpdateBeneficiaryInfoAsync(int id, string BeneficiaryName, string BeneficiaryAddress, string userId)
        {
            var competitor = await Repository.FindByIdAsync(id);
            competitor.BeneficiaryName = BeneficiaryName;
            competitor.BeneficiaryAddress = BeneficiaryAddress;

            return await Repository.UpdateAsync(competitor, userId);
        }

        public async Task<IList<Competitor>> GetChildsAsync(int id)
        {
            return await Repository.DbContext.Competitors
                .Where(c => c.ParentId == id)
                .ToListAsync();
        }
    }
}
