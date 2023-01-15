using Microsoft.EntityFrameworkCore;
using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class BanksBiz : BizBaseT<Bank>
    {
        public BanksBiz(IBanksRepository repository) : base(repository)
        {

        }

        public override async Task<IList<Bank>> GetAllAsync()
        {
            return await Repository.GetAllAsync(x => x.Country);
        }

        public override async Task<Bank> GetByIdAsync(int id)
        {
            return await Repository.GetFirstOrDefaultAsync(b => b.Id == id, x => x.Branches, z=>z.BankFees);
        }

        public override async Task<bool> UpdateAsync(Bank entity, string userId = null)
        {
            //Repository.DbContext.BankBranches.RemoveRange(Repository.DbContext.BankBranches.Where(br => br.BankId == entity.Id));
            return await base.UpdateAsync(entity, userId);
        }

        public async Task<IList<BankBranch>> GetBranchesAsync(int id)
        {
            return await Repository.DbContext.BankBranches.Where(bb => bb.BankId == id).ToListAsync();
        }

        public async Task<BankBranch> GetBranchAsync(int branchId)
        {
            return await Repository.DbContext.BankBranches.FirstOrDefaultAsync(b => b.Id == branchId);
        }

        public override async Task<IList<Bank>> GetAllUnarchivedAsync()
        {
            return await Repository.GetAllAsync(b => !b.IsArchived, x => x.Country, x => x.Branches);
        }

        public async Task<bool> AddBranchAsync(BankBranch branch, string userId = null)
        {
            await Repository.DbContext.BankBranches.AddAsync(branch);
            return await Repository.DbContext.SaveChangesAsync(userId) > 0;
        }

        public async Task<bool> ArchiveBranchAsync(int branchId, string userId = null)
        {
            var branch = await Repository.DbContext.BankBranches.FirstOrDefaultAsync(b => b.Id == branchId);
            branch.IsArchived = true;
            Repository.DbContext.Update(branch);
            return await Repository.DbContext.SaveChangesAsync(userId) > 0;
        }

        public async Task<bool> UpdateBranchAsync(BankBranch branch, string userId = null)
        {
            Repository.DbContext.Update(branch);
            return await Repository.DbContext.SaveChangesAsync(userId) > 0;
        }

        public override async Task<IList<Bank>> GetAllArchivedAsync()
        {
            return await Repository.GetAllAsync(b => b.IsArchived, x => x.Country);
        }
    }
}
