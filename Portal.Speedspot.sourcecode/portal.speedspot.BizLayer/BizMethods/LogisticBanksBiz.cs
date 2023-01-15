using Microsoft.EntityFrameworkCore;
using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class LogisticBanksBiz : BizBaseT<LogisticBank>
    {
        private static ILogisticBanksRepository _repository;

        public LogisticBanksBiz(ILogisticBanksRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IList<LogisticBank>> GetByLogisticIdAsync(int logisticId)
        {
            return await _repository.GetFullByLogisticIdAsync(logisticId);
        }

        public async Task<IList<LogisticBankCurrency>> GetCurrencies(int id)
        {
            return await Repository.DbContext.LogisticBankCurrencies.Where(c => c.LogisticBankId == id).ToListAsync();
        }

        public async Task<LogisticBankCurrency> GetCurrencyDetails(int currencyId)
        {
            return await Repository.DbContext.LogisticBankCurrencies.Where(c => c.Id == currencyId).FirstOrDefaultAsync();
        }

        public override async Task<LogisticBank> GetByIdAsync(int id)
        {
            return await Repository.GetFirstOrDefaultAsync(cb => cb.Id == id, x => x.Branch.Bank);
        }

        public override async Task<bool> UpdateAsync(LogisticBank entity, string userId = null)
        {
            Repository.DbContext.LogisticBankCurrencies.RemoveRange(Repository.DbContext.LogisticBankCurrencies.Where(c => c.LogisticBankId == entity.Id));
            return await base.UpdateAsync(entity, userId);
        }
    }
}
