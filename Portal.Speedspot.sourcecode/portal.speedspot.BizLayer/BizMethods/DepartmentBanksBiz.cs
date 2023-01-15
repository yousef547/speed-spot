using Microsoft.EntityFrameworkCore;
using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class DepartmentBanksBiz : BizBaseT<DepartmentBank>
    {
        private static IDepartmentBanksRepository _repository;

        public DepartmentBanksBiz(IDepartmentBanksRepository repositery) : base(repositery)
        {
            _repository = repositery;
        }

        public async Task<IList<DepartmentBank>> GetByDepartmentIdAsync(int departmentId)
        {
            return await _repository.GetFullByDepartmentIdAsync(departmentId);
        }

        public async Task<IList<DepartmentBankCurrency>> GetCurrencies(int id)
        {
            return await Repository.DbContext.DepartmentBankCurrencies.Include(x => x.Currency).Where(c => c.DepartmentBankId == id).ToListAsync();
        }

        public async Task<DepartmentBankCurrency> GetCurrencyDetails(int currencyId)
        {
            return await _repository.DbContext.DepartmentBankCurrencies.Where(c => c.Id == currencyId).FirstOrDefaultAsync();
        }

        public override async Task<DepartmentBank> GetByIdAsync(int id)
        {
            return await Repository.GetFirstOrDefaultAsync(cb => cb.Id == id, x => x.Branch.Bank);
        }

        public override async Task<bool> UpdateAsync(DepartmentBank entity, string userId = null)
        {
            Repository.DbContext.DepartmentBankCurrencies.RemoveRange(Repository.DbContext.DepartmentBankCurrencies.Where(c => c.DepartmentBankId == entity.Id));
            return await base.UpdateAsync(entity, userId);
        }
    }
}
