using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class FinancialAccountsBiz : BizBaseT<FinancialAccount>
    {
        public FinancialAccountsBiz(IFinancialAccountsRepository repository) : base(repository)
        {

        }

        public async Task<IList<FinancialAccount>> GetParentsByDepartmentId(int departmentId, FinancialAccountType? type)
        {
            return await Repository.GetAllAsync(a => a.ParentId == null && !a.IsArchived && a.DepartmentId == departmentId &&
            a.Type == (type != null ? type : a.Type));
        }

        public async Task<IList<FinancialAccount>> GetAllByDepartmentId(int departmentId)
        {
            return await Repository.GetAllAsync(a => !a.IsArchived && a.DepartmentId == departmentId);
        }

        public async Task<IList<FinancialAccount>> GetChildren()
        {
            return await Repository.GetAllAsync(a => a.Childs.Count == 0 && !a.IsArchived);
        }

        public async Task<string> GetNewParentAutoCode(int departmentId, FinancialAccountType? type)
        {
            var siblings = await Repository.GetAllAsync(a => a.DepartmentId == departmentId && a.ParentId == null && a.Type == type);
            if (siblings.Count > 0)
            {
                string lastCode = (await Repository.GetAllAsync(a => a.DepartmentId == departmentId && a.ParentId == null && a.Type == type))
                    .OrderByDescending(o => o.AutoCode).
                    FirstOrDefault()
                    .AutoCode;
                int codeInt = int.Parse(lastCode) + 1;
                return codeInt.ToString("D2");
            }
            return 1.ToString("D2");
        }

        public async Task<string> GenerateNewParentCode(int departmentId, FinancialAccountType? type)
        {
            string autoCode = await GetNewParentAutoCode(departmentId, type);
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

        public async override Task<FinancialAccount> GetByIdAsync(int id)
        {
            return await Repository.GetFirstOrDefaultAsync(a => a.Id == id, x => x.Department);
        }
    }
}
