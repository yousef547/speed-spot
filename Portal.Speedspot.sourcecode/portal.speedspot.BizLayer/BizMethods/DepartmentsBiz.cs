using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class DepartmentsBiz : BizBaseT<Department>
    {
        private readonly IDepartmentsRepository _repository;
        public DepartmentsBiz(IDepartmentsRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IList<Department>> GetAllUnarchivedByUserAsync(int userDepartmentId, List<int> userDepartmentIds)
        {
            return (await Repository.GetAllAsync(d => d.IsArchived == false &&
            (d.Id == userDepartmentId || userDepartmentIds.Contains(d.Id)))).ToList();
        }

        public override async Task<Department> GetByIdAsync(int id)
        {
            return await _repository.GetFullItemByIdAsync(id);
        }

        public override Task<IList<Department>> GetAllAsync()
        {
            return Repository.GetAllAsync(x => x.Opportunities);
        }

        public async Task<bool> UpdateBeneficiaryInfoAsync(int id, string BeneficiaryName, string BeneficiaryAddress, string userId)
        {
            var department = await Repository.FindByIdAsync(id);
            department.BeneficiaryName = BeneficiaryName;
            department.BeneficiaryAddress = BeneficiaryAddress;

            return await Repository.UpdateAsync(department, userId);
        }
    }
}
