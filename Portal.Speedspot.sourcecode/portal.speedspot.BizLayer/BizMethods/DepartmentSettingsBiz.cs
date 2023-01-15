using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class DepartmentSettingsBiz : BizBaseT<DepartmentSettings>
    {
        public DepartmentSettingsBiz(IDepartmentSettingsRepository repository) : base(repository)
        {

        }

        public async Task<DepartmentSettings> GetByDepartmentIdAsync(int departmentId)
        {
            return await Repository
                .GetFirstOrDefaultAsync(s => s.DepartmentId == departmentId,
                x => x.Department);
        }
    }
}
