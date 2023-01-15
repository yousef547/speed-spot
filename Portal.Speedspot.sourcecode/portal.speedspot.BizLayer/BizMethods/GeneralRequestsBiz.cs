using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class GeneralRequestsBiz : BizBaseT<GeneralRequest>
    {
        public GeneralRequestsBiz(IGeneralRequestsRepository repository) : base(repository)
        {

        }

        public async Task<IList<GeneralRequest>> GetUserRequestsAsync(string userId)
        {
            return await Repository.GetAllAsync(r => !r.IsArchived &&
                ((r.Status == RequestStatusEnum.Pending) ||
                 (r.Deadline >= DateTime.UtcNow.AddDays(-30))) &&
                r.RequestedById == userId,
                x => x.RequestFrom);
        }

        public async Task<IList<GeneralRequest>> GetPendingOnUserAsync(string userId)
        {
            return await Repository.GetAllAsync(r => !r.IsArchived &&
                ((r.Status == RequestStatusEnum.Pending) ||
                (r.Deadline >= DateTime.UtcNow.AddDays(-30))) &&
                r.RequestFromId == userId,
                x => x.RequestFrom,
                x => x.RequestedBy);
        }

        override public async Task<GeneralRequest> GetByIdAsync(int id)
        {
            return await Repository.GetFirstOrDefaultAsync(x => x.Id == id,
                y => y.RequestFrom,
                y => y.RequestedBy);
        }

        public async Task<bool> ApproveAsync(int id, string userId)
        {
            var request = await Repository.GetFirstOrDefaultAsync(r => r.Id == id);
            request.Status = RequestStatusEnum.Approved;
            request.StatusDate = DateTime.UtcNow;
            return await Repository.UpdateAsync(request, userId);
        }

        public async Task<bool> RejectAsync(int id, string userId)
        {
            var request = await Repository.GetFirstOrDefaultAsync(x => x.Id == id);
            request.Status = RequestStatusEnum.Rejected;
            request.StatusDate = DateTime.UtcNow;
            return await Repository.UpdateAsync(request, userId);
        }

        public async Task<bool> UndoAsync(int id, string userId)
        {
            var request = await Repository.GetFirstOrDefaultAsync(x => x.Id == id);
            request.Status = RequestStatusEnum.Pending;
            request.StatusDate = DateTime.UtcNow;
            return await Repository.UpdateAsync(request, userId);
        }
    }
}
