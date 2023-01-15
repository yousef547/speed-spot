using Microsoft.EntityFrameworkCore;

using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Migrations;
using portal.speedspot.Models.Concretes;

using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

using static portal.speedspot.Models.Constants.Permissions;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class PaymentRequestsBiz : BizBaseT<PaymentRequest>
    {
        public PaymentRequestsBiz(IPaymentRequestsRepository repository) : base(repository)
        {

        }

        public async Task<int> GetNewSerialNo(int? id, int year, int departmentId)
        {
            var paymentRequests = await Repository.GetAllAsync(o => o.Id != id && o.DepartmentId == departmentId && o.DateCreated.Year == year);
            paymentRequests = paymentRequests.OrderByDescending(x => x.SerialNo).ToList();
            if (paymentRequests.Count == 0)
            {
                return 1;
            }
            return paymentRequests.FirstOrDefault().SerialNo + 1;
        }

        public override async Task<IList<PaymentRequest>> GetAllUnarchivedAsync()
        {
            return await Repository.DbContext.PaymentRequests
                 .Where(p => p.IsArchived == false)
                 .Include(x => x.Department)
                 .Include(x => x.CreatedByUser)
                 .Include(x => x.Project)
                 .Include(x => x.Directions).ThenInclude(y => y.Account)
                 .Include(x => x.Currency)
                 .Include(x => x.PaymentDetails).ThenInclude(y => y.Treasury.Currency)
                 .Include(x => x.CashAttachment)
                 .Include(x => x.ReceiptAttachment)
                 .Include(x => x.TransferAttachment)
                 .ToListAsync();
        }

        public async Task<IList<PaymentRequest>> GetAllUnarchivedByUserAsync(string userId, int userDepartmentId, List<int> userDepartmentIds)
        {
            return await Repository.DbContext.PaymentRequests
                 .Where(p => p.IsArchived == false &&
                 (p.CreatedByUserId == userId ||
                 p.DepartmentId == userDepartmentId ||
                 userDepartmentIds.Contains(p.DepartmentId)))
                 .Include(x => x.Department)
                 .Include(x => x.CreatedByUser)
                 .Include(x => x.Project)
                 .Include(x => x.Directions).ThenInclude(y => y.Account)
                 .Include(x => x.Currency)
                 .Include(x => x.PaymentDetails).ThenInclude(y => y.Treasury.Currency)
                 .Include(x => x.CashAttachment)
                 .Include(x => x.ReceiptAttachment)
                 .Include(x => x.TransferAttachment)
                 .ToListAsync();
        }

        public override async Task<PaymentRequest> GetByIdAsync(int id)
        {
            return await Repository.DbContext.PaymentRequests
                 .Include(x => x.Directions).ThenInclude(y => y.Account)
                 .Include(x => x.Currency)
                 .Include(x => x.PaymentDetails).ThenInclude(y => y.Treasury.Bank)
                 .Include(x => x.PaymentDetails).ThenInclude(y => y.ReceiveBank)
                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> AddPaymentDetails(PaymentDetails paymentDetails)
        {
            Repository.DbContext.PaymentDetails.Add(paymentDetails);
            return await Repository.DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> EditPaymentDetails(PaymentDetails paymentDetails)
        {
            Repository.DbContext.Update(paymentDetails);
            return await Repository.DbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> ApproveRequestAsync(int id)
        {
            var request = await Repository.GetFirstOrDefaultAsync(r => r.Id == id);
            request.Status = Models.Constants.ApplicationEnums.RequestStatusEnum.Approved;
            return await Repository.UpdateAsync(request);
        }

        public async Task<bool> RejectRequestAsync(int id, string reason)
        {
            var request = await Repository.GetFirstOrDefaultAsync(r => r.Id == id);
            request.Status = Models.Constants.ApplicationEnums.RequestStatusEnum.Rejected;
            request.RejectReason = reason;
            return await Repository.UpdateAsync(request);
        }
    }
}
