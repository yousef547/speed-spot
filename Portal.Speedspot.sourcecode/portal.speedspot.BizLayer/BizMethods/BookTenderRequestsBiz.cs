using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static portal.speedspot.Models.Constants.ApplicationEnums;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class BookTenderRequestsBiz : BizBaseT<BookTenderRequest>
    {
        public BookTenderRequestsBiz(IBookTenderRequestsRepository repository) : base(repository)
        {

        }

        public override async Task<BookTenderRequest> GetByIdAsync(int id)
        {
            return await Repository.GetFirstOrDefaultAsync(r => r.Id == id,
                x => x.BookTender.Opportunity.Customer,
                x => x.BookTender.AssignedTo);
        }

        public async Task<IList<BookTenderRequest>> GetPendingOnAccountantAsync(List<int> departmentIds, int departmentId)
        {
            return await Repository.GetAllAsync(r => !r.IsArchived &&
                (departmentIds.Contains(r.DepartmentId) || r.DepartmentId == departmentId) &&
                !r.IsPrinted &&
                (!r.IsSent && r.Status == RequestStatusEnum.Pending) || (r.IsSent && r.Status == RequestStatusEnum.Approved) &&
                (r.Status == RequestStatusEnum.Pending || r.BookTender.Opportunity.DueDate >= DateTime.UtcNow.AddDays(-30)),
                x => x.RequestedBy, x => x.SentBy, x => x.StatusBy, x => x.BookTender.Opportunity);
        }

        public async Task<IList<BookTenderRequest>> GetPendingOnManagerAsync(List<int> departmentIds, int departmentId)
        {
            return await Repository.GetAllAsync(r => !r.IsArchived &&
                (departmentIds.Contains(r.DepartmentId) || r.DepartmentId == departmentId) &&
                !r.IsPrinted && r.IsSent &&
                (r.Status == RequestStatusEnum.Pending || r.BookTender.Opportunity.DueDate >= DateTime.UtcNow.AddDays(-30)),
                x => x.RequestedBy, x => x.SentBy, x => x.StatusBy, x => x.BookTender.Opportunity);
        }

        public override async Task<IList<BookTenderRequest>> GetAllUnarchivedAsync()
        {
            return await Repository.GetAllAsync(r => !r.IsArchived, x => x.RequestedBy, x => x.BookTender.Opportunity);
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
        public async Task<IList<BookTenderRequest>> GetMyRequestsAsync(string userId)
        {
            return await Repository.GetAllAsync(r => !r.IsArchived &&
                r.RequestedById == userId &&
                (r.Status == RequestStatusEnum.Pending || r.BookTender.Opportunity.DueDate >= DateTime.UtcNow.AddDays(-30)),
                 x => x.RequestedBy, x => x.SentBy, x => x.StatusBy, x => x.BookTender.AssignedTo, x => x.BookTender.Opportunity);
        }


    }
}
