
using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class TreasuryTransactionsBiz : BizBaseT<TreasuryTransaction>
    {
        public TreasuryTransactionsBiz(ITreasuryTransactionsRepository repository) : base(repository)
        {

        }
    }
}
