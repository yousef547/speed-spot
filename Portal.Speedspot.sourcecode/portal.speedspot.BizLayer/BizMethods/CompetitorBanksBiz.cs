using Microsoft.EntityFrameworkCore;
using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class CompetitorBanksBiz : BizBaseT<CompetitorBank>
    {
        private static ICompetitorBanksRepository _repository;
        public CompetitorBanksBiz(ICompetitorBanksRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IList<CompetitorBank>> GetByCompetitorIdAsync(int competitorId)
        {
            return await _repository.GetFullByCompetitorIdAsync(competitorId);
        }

        public async Task<IList<CompetitorBankCurrency>> GetCurrencies(int id)
        {
            return await Repository.DbContext.CompetitorBankCurrencies.Where(c => c.CompetitorBankId == id).ToListAsync();
        }

        public async Task<CompetitorBankCurrency> GetCurrencyDetails(int currencyId)
        {
            return await Repository.DbContext.CompetitorBankCurrencies.Where(c => c.Id == currencyId).FirstOrDefaultAsync();
        }

        public override async Task<CompetitorBank> GetByIdAsync(int id)
        {
            return await Repository.GetFirstOrDefaultAsync(cb => cb.Id == id, x => x.Branch.Bank);
        }

        public override async Task<bool> UpdateAsync(CompetitorBank entity, string userId = null)
        {
            Repository.DbContext.CompetitorBankCurrencies.RemoveRange(Repository.DbContext.CompetitorBankCurrencies.Where(c => c.CompetitorBankId == entity.Id));
            return await base.UpdateAsync(entity, userId);
        }
    }
}
