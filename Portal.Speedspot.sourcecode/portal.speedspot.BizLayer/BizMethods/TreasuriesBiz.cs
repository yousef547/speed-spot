using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class TreasuriesBiz : BizBaseT<Treasury>
    {
        public TreasuriesBiz(ITreasuriesRepository repository) : base(repository)
        {

        }

        public async Task<IList<Treasury>> GetUnarchivedAsync(int departmentId)
        {
            return await Repository.GetAllAsync(y => y.DepartmentId == departmentId,
                x => x.Department,
                x => x.Currency,
                x => x.Bank,
                x => x.Transactions);
        }

        public override async Task<Treasury> GetByIdAsync(int id)
        {
            return await Repository.GetFirstOrDefaultAsync(t => t.Id == id,
                x => x.Department,
                x => x.Currency,
                x => x.Transactions);
        }
    }
}
