using Microsoft.EntityFrameworkCore;
using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class SupplierBanksBiz : BizBaseT<SupplierBank>
    {
        private static ISupplierBanksRepository _repository;
        public SupplierBanksBiz(ISupplierBanksRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IList<SupplierBank>> GetBySupplierIdAsync(int supplierId)
        {
            return await _repository.GetFullBySupplierIdAsync(supplierId);
        }

        public async Task<IList<SupplierBankCurrency>> GetCurrencies(int id)
        {
            return await Repository.DbContext.SupplierBankCurrencies.Where(c => c.SupplierBankId == id).ToListAsync();
        }

        public async Task<SupplierBankCurrency> GetCurrencyDetails(int currencyId)
        {
            return await Repository.DbContext.SupplierBankCurrencies.Where(c => c.Id == currencyId).FirstOrDefaultAsync();
        }

        public override async Task<SupplierBank> GetByIdAsync(int id)
        {
            return await Repository.GetFirstOrDefaultAsync(cb => cb.Id == id, x => x.Branch.Bank);
        }

        public override async Task<bool> UpdateAsync(SupplierBank entity, string userId = null)
        {
            Repository.DbContext.SupplierBankCurrencies.RemoveRange(Repository.DbContext.SupplierBankCurrencies.Where(c => c.SupplierBankId == entity.Id));
            return await base.UpdateAsync(entity, userId);
        }
    }
}
