using Microsoft.EntityFrameworkCore;
using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class CustomerBanksBiz : BizBaseT<CustomerBank>
    {
        private static ICustomerBanksRepository _repository;
        public CustomerBanksBiz(ICustomerBanksRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IList<CustomerBank>> GetByCustomerIdAsync(int customerId)
        {
            return await _repository.GetFullByCustomerIdAsync(customerId);
        }

        public async Task<IList<CustomerBankCurrency>> GetCurrencies(int id)
        {
            return await Repository.DbContext.CustomerBankCurrencies.Where(c => c.CustomerBankId == id).ToListAsync();
        }

        public async Task<CustomerBankCurrency> GetCurrencyDetails(int currencyId)
        {
            return await Repository.DbContext.CustomerBankCurrencies.Where(c => c.Id == currencyId).FirstOrDefaultAsync();
        }

        public override async Task<CustomerBank> GetByIdAsync(int id)
        {
            return await Repository.GetFirstOrDefaultAsync(cb => cb.Id == id, x => x.Branch.Bank);
        }

        public override async Task<bool> UpdateAsync(CustomerBank entity, string userId = null)
        {
            Repository.DbContext.CustomerBankCurrencies.RemoveRange(Repository.DbContext.CustomerBankCurrencies.Where(c => c.CustomerBankId == entity.Id));
            return await base.UpdateAsync(entity, userId);
        }
    }
}
