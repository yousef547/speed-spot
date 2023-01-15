using Microsoft.EntityFrameworkCore;

using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.Constants;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class QuotationsBiz : BizBaseT<Quotation>
    {
        private static IQuotationsRepository _repository;
        public QuotationsBiz(IQuotationsRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IList<Quotation>> GetAllUnarchivedAsync(int? statusId = null)
        {
            var quotations = await Repository.GetAllAsync(q => q.IsArchived == false && q.StatusId == (statusId == null ? q.StatusId : statusId),
                x => x.Admin,
                x => x.CreatedBy,
                x => x.Status,
                x => x.Opportunity.SupplierOffers,
                x => x.Opportunity.Type,
                x => x.Offers);

            return quotations.OrderBy(o => o.DueDate).ToList();
        }

        public async Task<IList<Quotation>> GetAllUnarchivedExceptAsync(int statusId)
        {
            var quotations = await Repository.GetAllAsync(q => q.IsArchived == false && q.StatusId != statusId,
                x => x.Admin,
                x => x.CreatedBy,
                x => x.Status,
                x => x.Opportunity.SupplierOffers,
                x => x.Opportunity.Type,
                x => x.Offers);

            return quotations.OrderBy(o => o.DueDate).ToList();
        }

        public async Task<IList<Quotation>> GetAllUnarchivedExceptAsync(List<int> opportunitiesId)
        {
            var quotations = await Repository.GetAllAsync(q => q.IsArchived == false &&
                opportunitiesId.Contains(q.OpportunityId),
                x => x.Admin,
                x => x.CreatedBy,
                x => x.Status,
                x => x.Opportunity.SupplierOffers,
                x => x.Opportunity.Type,
                x => x.Offers);

            return quotations.OrderBy(o => o.DueDate).ToList();
        }

        public async Task<IList<Quotation>> GetAllUnarchivedByUserAsync(string userId, int userDepartmentId, int? statusId = null)
        {
            return (await Repository
                .GetAllAsync(q => q.IsArchived == false && q.StatusId == (statusId == null ? q.StatusId : statusId) &&
                (q.CreatedById == userId ||
                q.AdminId == userId ||
                q.Opportunity.DepartmentId == userDepartmentId),
                x => x.Admin,
                x => x.CreatedBy,
                x => x.Status,
                x => x.Opportunity.SupplierOffers,
                x => x.Opportunity.Type,
                x => x.Offers))
                .OrderBy(o => o.DueDate).ToList();
        }

        public async Task<IList<Quotation>> GetAllUnarchivedByUserExceptAsync(string userId, int userDepartmentId, int statusId)
        {
            return (await Repository
               .GetAllAsync(q => q.IsArchived == false && q.StatusId != statusId &&
               (q.CreatedById == userId ||
               q.AdminId == userId ||
               q.Opportunity.DepartmentId == userDepartmentId),
               x => x.Admin,
               x => x.CreatedBy,
               x => x.Status,
               x => x.Opportunity.SupplierOffers,
                x => x.Opportunity.Type,
               x => x.Offers))
               .OrderBy(o => o.DueDate).ToList();
        }

        public async Task<IList<Quotation>> GetAllUnarchivedByUserExceptAsync(string userId, int userDepartmentId, List<int> opportunitiesId)
        {
            return (await Repository
               .GetAllAsync(q => q.IsArchived == false &&
               opportunitiesId.Contains(q.OpportunityId) &&
               (q.CreatedById == userId ||
               q.AdminId == userId ||
               q.Opportunity.DepartmentId == userDepartmentId),
               x => x.Admin,
               x => x.CreatedBy,
               x => x.Status,
               x => x.Opportunity.SupplierOffers,
                x => x.Opportunity.Type,
               x => x.Offers))
               .OrderBy(o => o.DueDate).ToList();
        }

        public async override Task<Quotation> GetByIdAsync(int id)
        {
            return await _repository.GetFullItemById(id);
        }

        public async Task<Quotation> GetNameByIdAsync(int id)
        {
            return await _repository.GetFirstOrDefaultAsync(y => y.Id == id, x => x.Opportunity);
        }

        public async Task<bool> ChangeSelectedVersion(int id, int versionId)
        {
            var quotationVersions = await Repository.DbContext.QuotationOfferVersions.Where(v => v.Offer.QuotationId == id).ToListAsync();
            quotationVersions.ForEach(v => v.IsSelected = false);

            var version = await Repository.DbContext.QuotationOfferVersions.FirstOrDefaultAsync(v => v.Id == versionId);
            version.IsSelected = true;

            return await Repository.DbContext.SaveChangesAsync() > 0;
        }

        public async Task<Quotation> GetInfoByIdAsync(int id)
        {
            return await Repository.GetFirstOrDefaultAsync(q => q.Id == id,
                x => x.Opportunity.Customer.LogoAttachment);
        }

        public async Task<Quotation> GetWithLGByIdAsync(int id)
        {
            return await Repository.GetFirstOrDefaultAsync(q => q.Id == id,
                x => x.Opportunity,
                x => x.PerformanceLG.Request,
                x => x.PerformanceLG.AssignedTo,
                x => x.PerformanceLG.Bank,
                x => x.PerformanceLG.BankBranch,
                x => x.PerformanceLG.IssueBank,
                x => x.PerformanceLG.ReceiveBank,
                x => x.FinalLG.Request,
                x => x.FinalLG.AssignedTo,
                x => x.FinalLG.Bank,
                x => x.FinalLG.BankBranch);
        }

        public async Task<IList<Quotation>> GetTechnicalSessionRejectedAsync(string userId = null, int? userDepartmentId = null, List<int> userDepartmentIds = default!)
        {
            var quotations = Repository.DbContext.Quotations
                .Include(q => q.Opportunity.Type)
                .Include(q => q.Admin)
                .Include(q => q.CreatedBy)
                .Include(q => q.Offers)
                .Where(q => q.TechnicalSessionStatus != null && q.TechnicalSessionStatus == ApplicationEnums.QuotationTechnicalSessionStatus.Rejected);

            if (userId != null && userDepartmentId != null && userDepartmentIds != null && userDepartmentIds.Count > 0)
            {
                quotations = quotations.Where(q => q.CreatedById == userId ||
                q.AdminId == userId ||
                q.Opportunity.DepartmentId == userDepartmentId ||
                userDepartmentIds.Contains(q.Opportunity.DepartmentId));
            }

            return await quotations.ToListAsync();
        }

        public async Task<IList<Quotation>> GetFinancialSessionRejectedAsync(string userId = null, int? userDepartmentId = null, List<int> userDepartmentIds = default!)
        {
            var quotations = Repository.DbContext.Quotations
               .Include(q => q.Opportunity.Type)
               .Include(q => q.Admin)
               .Include(q => q.CreatedBy)
               .Include(q => q.Offers)
               .Where(q => q.FinancialSessionStatus != null && q.FinancialSessionStatus == ApplicationEnums.QuotationFinancialSessionStatus.Rejected);

            if (userId != null && userDepartmentId != null && userDepartmentIds != null && userDepartmentIds.Count > 0)
            {
                quotations = quotations.Where(q => q.CreatedById == userId ||
                q.AdminId == userId ||
                q.Opportunity.DepartmentId == userDepartmentId ||
                userDepartmentIds.Contains(q.Opportunity.DepartmentId));
            }

            return await quotations.ToListAsync();
        }

        public async Task<bool> DeleteProductAlternate(int id,bool isAlternate,bool isIncluded)
        {
            if (isAlternate)
            {
                var productAlternate = Repository.DbContext.AlternateVersionProducts.Find(id);
                productAlternate.IsIncluded = isIncluded;
            }
            else
            {
                var productQuotation = Repository.DbContext.QuotationOfferProducts.Find(id);
                productQuotation.IsIncluded = isIncluded;
            }
            return await Repository.DbContext.SaveChangesAsync() > 0;
        }
    }
}
