using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class FinancialAccountTransactionsBiz : BizBaseT<FinancialAccountTransaction>
    {
        public FinancialAccountTransactionsBiz(IFinancialAccountTransactionsRepository repository) : base(repository)
        {

        }

        public async Task<IList<FinancialAccountTransaction>> GetByAccountIdAsync(int financialAccountId)
        {
            List<FinancialAccountTransaction> transactions = new();
            var accountTransactions = await Repository.GetAllAsync(t => t.FinancialAccountId == financialAccountId, x => x.FinancialAccount, x => x.Currency);
            transactions.AddRange(accountTransactions);

            var financialAccount = await Repository.DbContext.FinancialAccounts.FirstOrDefaultAsync(a => a.Id == financialAccountId);
            if (financialAccount.Childs.Count > 0)
            {
                foreach (var child in financialAccount.Childs)
                {
                    transactions.AddRange(await GetByAccountIdAsync(child.Id));
                }
            }
            return transactions;
        }
    }
}
