using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class FinalLGRequestsBiz : BizBaseT<FinalLGRequest>
    {
        public FinalLGRequestsBiz(IFinalLGRequestsRepository repository) : base(repository)
        {

        }

        public override async Task<FinalLGRequest> GetByIdAsync(int id)
        {
            return await Repository.GetFirstOrDefaultAsync(r => r.Id == id,
                x => x.FinalLG.Quotation.Opportunity.Customer,
                x => x.FinalLG.AssignedTo,
                x => x.FinalLG.Bank);
        }

        public async Task<IList<FinalLGRequest>> GetPendingOnAccountantAsync(List<int> departmentIds, int departmentId)
        {
            return await Repository.GetAllAsync(r => !r.IsArchived &&
                (departmentIds.Contains(r.DepartmentId) || r.DepartmentId == departmentId) &&
                !r.IsPrinted &&
                (!r.IsSent && r.Status == RequestStatusEnum.Pending) || (r.IsSent && r.Status == RequestStatusEnum.Approved) &&
                (r.Status == RequestStatusEnum.Pending || r.FinalLG.Quotation.Opportunity.DueDate >= DateTime.UtcNow.AddDays(-30)),
                x => x.RequestedBy, x => x.SentBy, x => x.StatusBy, x => x.FinalLG.Quotation.Opportunity);
        }

        public async Task<IList<FinalLGRequest>> GetPendingOnManagerAsync(List<int> departmentIds, int departmentId)
        {
            return await Repository.GetAllAsync(r => !r.IsArchived &&
                (departmentIds.Contains(r.DepartmentId) || r.DepartmentId == departmentId) &&
                !r.IsPrinted && r.IsSent &&
                (r.Status == RequestStatusEnum.Pending || r.FinalLG.Quotation.Opportunity.DueDate >= DateTime.UtcNow.AddDays(-30)),
                x => x.RequestedBy, x => x.SentBy, x => x.StatusBy, x => x.FinalLG.Quotation.Opportunity);
        }

        public override async Task<IList<FinalLGRequest>> GetAllUnarchivedAsync()
        {
            return await Repository.GetAllAsync(r => !r.IsArchived, x => x.RequestedBy, x => x.FinalLG.Quotation.Opportunity);
        }

        public async Task<bool> Approve(int id, string userId)
        {
            var request = await Repository.GetFirstOrDefaultAsync(r => r.Id == id);
            request.Status = RequestStatusEnum.Approved;
            request.StatusById = userId;
            request.StatusDate = DateTime.UtcNow;
            return await Repository.UpdateAsync(request, userId);
        }

        public async Task<bool> Reject(int id, string userId)
        {
            var request = await Repository.GetFirstOrDefaultAsync(x => x.Id == id);
            request.Status = RequestStatusEnum.Rejected;
            request.StatusById = userId;
            request.StatusDate = DateTime.UtcNow;
            return await Repository.UpdateAsync(request, userId);
        }

        public async Task<bool> Undo(int id, string userId)
        {
            var request = await Repository.GetFirstOrDefaultAsync(x => x.Id == id);
            request.Status = RequestStatusEnum.Pending;
            request.StatusById = userId;
            request.StatusDate = DateTime.UtcNow;
            return await Repository.UpdateAsync(request, userId);
        }

        public async Task<IList<FinalLGRequest>> GetMyRequestsAsync(string userId)
        {
            return await Repository.GetAllAsync(r => !r.IsArchived &&
                r.RequestedById == userId &&
                (r.Status == RequestStatusEnum.Pending || r.FinalLG.Quotation.Opportunity.DueDate >= DateTime.UtcNow.AddDays(-30)),
                x => x.RequestedBy, x => x.SentBy, x => x.StatusBy, x => x.FinalLG.AssignedTo, x => x.FinalLG.Quotation.Opportunity);
        }
    }
}
