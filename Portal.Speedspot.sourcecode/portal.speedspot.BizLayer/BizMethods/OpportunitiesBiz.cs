using Microsoft.EntityFrameworkCore;

using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using portal.speedspot.Models.Concretes.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class OpportunitiesBiz : BizBaseT<Opportunity>
    {
        private static IOpportunitiesRepository _repository;
        public OpportunitiesBiz(IOpportunitiesRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public override async Task<IList<Opportunity>> GetAllUnarchivedAsync()
        {
            return (await Repository.GetAllAsync(o => !o.IsArchived && o.DueDate >= DateTime.UtcNow.Date && !o.IsConverted,
                x => x.Type,
                x => x.Customer,
                x => x.SalesAgent,
                x => x.Country,
                x => x.Department,
                x => x.BidBond.Request,
                x => x.BookTender.Request,
                x => x.Status,
                x => x.CreatedBy,
                x => x.TechnicalSpecification.Products,
                x => x.RequestOffers,
                x => x.SupplierOffers))
                .OrderBy(o => o.DueDate).ToList();
        }
        public async Task<IList<Opportunity>> GetAllUnarchivedAsync(int customerId)
        {
            return (await Repository.GetAllAsync(o => !o.IsArchived && o.DueDate >= DateTime.UtcNow.Date && o.CustomerId == customerId && !o.IsConverted,
                x => x.Type,
                x => x.Customer,
                x => x.SalesAgent,
                x => x.Country,
                x => x.Department,
                x => x.BidBond.Request,
                x => x.BookTender.Request,
                x => x.Status,
                x => x.CreatedBy,
                x => x.TechnicalSpecification.Products,
                x => x.RequestOffers,
                x => x.SupplierOffers))
                .OrderBy(o => o.DueDate).ToList();
        }

        public async Task<IList<Opportunity>> GetAllUnarchivedByUserAsync(string userId, int userDepartmentId, List<int> userDepartmentIds)
        {
            return (await Repository
                .GetAllAsync(o => !o.IsArchived && o.DueDate >= DateTime.UtcNow.Date && !o.IsConverted &&
                (o.CreatedById == userId ||
                (o.ProjectManagerId != null && o.ProjectManagerId == userId) ||
                (o.GuestId != null && o.GuestId == userId) ||
                (o.BidBond != null && o.BidBond.AssignedToId == userId) ||
                (o.BookTender != null && o.BookTender.AssignedToId == userId) ||
                o.SalesAgentId == userId ||
                o.DepartmentId == userDepartmentId ||
                userDepartmentIds.Contains(o.DepartmentId)),
                x => x.Type,
                x => x.Customer,
                x => x.SalesAgent,
                x => x.Country,
                x => x.Department,
                x => x.BidBond.Request,
                x => x.BookTender.Request,
                x => x.Status,
                x => x.CreatedBy,
                x => x.TechnicalSpecification.Products,
                x => x.RequestOffers,
                x => x.SupplierOffers))
                .OrderBy(o => o.DueDate).ToList();
        }
        public async Task<IList<Opportunity>> GetAllUnarchivedByUserAsync(string userId, int userDepartmentId, List<int> userDepartmentIds, int customerId)
        {
            return (await Repository
             .GetAllAsync(o => o.IsArchived == false && o.DueDate >= DateTime.UtcNow.Date &&
             o.CustomerId == customerId &&
             (o.CreatedById == userId ||
             (o.ProjectManagerId != null && o.ProjectManagerId == userId) ||
             (o.GuestId != null && o.GuestId == userId) ||
             (o.BidBond != null && o.BidBond.AssignedToId == userId) ||
             (o.BookTender != null && o.BookTender.AssignedToId == userId) ||
             o.SalesAgentId == userId ||
             o.DepartmentId == userDepartmentId ||
             userDepartmentIds.Contains(o.DepartmentId)),
             x => x.Type,
             x => x.Customer,
             x => x.SalesAgent,
             x => x.Country,
             x => x.Department,
             x => x.BidBond.Request,
             x => x.BookTender.Request,
             x => x.Status,
             x => x.CreatedBy,
             x => x.TechnicalSpecification.Products,
             x => x.RequestOffers,
             x => x.SupplierOffers))
             .OrderBy(o => o.DueDate).ToList();
        }


        public override async Task<IList<Opportunity>> GetAllArchivedAsync()
        {
            return (await Repository.GetAllAsync(o => o.IsArchived || (o.DueDate < DateTime.UtcNow.Date),
            x => x.Type,
            x => x.Customer,
            x => x.SalesAgent,
            x => x.Country,
            x => x.Department,
            x => x.BidBond,
            x => x.Status,
            x => x.CreatedBy))
            .OrderBy(o => o.DueDate).ToList();
        }

        public async Task<IList<Opportunity>> GetAllArchivedByUserAsync(string userId, int userDepartmentId, List<int> userDepartmentIds)
        {
            return (await Repository
              .GetAllAsync(o => (o.IsArchived || (o.DueDate < DateTime.UtcNow.Date)) &&
              (o.CreatedById == userId ||
              (o.ProjectManagerId != null && o.ProjectManagerId == userId) ||
              (o.GuestId != null && o.GuestId == userId) ||
              (o.BidBond != null && o.BidBond.AssignedToId == userId) ||
              (o.BookTender != null && o.BookTender.AssignedToId == userId) ||
              o.SalesAgentId == userId ||
              o.DepartmentId == userDepartmentId ||
              userDepartmentIds.Contains(o.DepartmentId)),
              x => x.Type,
              x => x.Customer,
              x => x.SalesAgent,
              x => x.Country,
              x => x.Department,
              x => x.BidBond,
              x => x.Status,
              x => x.CreatedBy))
              .OrderBy(o => o.DueDate).ToList();
        }

        public override async Task<Opportunity> GetByIdAsync(int id)
        {
            return await _repository.GetFullItemById(id);
        }

        public async Task<Opportunity> GetNameByIdAsync(int id)
        {
            return await _repository.FindByIdAsync(id);
        }

        public async Task<Opportunity> GetBasicDataByIdAsync(int id)
        {
            var opportunity = await Repository.GetFirstOrDefaultAsync(o => o.Id == id,
                x => x.Customer, x => x.Department);

            return opportunity;
        }

        public async Task<bool> TechnicalApproveAsync(int id, string userId)
        {
            var opportunity = await Repository.GetFirstOrDefaultAsync(o => o.Id == id);
            opportunity.IsTechnicalApproved = true;
            opportunity.TechnicalApprovedById = userId;
            var result = await Repository.UpdateAsync(opportunity, userId);
            return result;
        }

        public async Task<int> GetNewSerialNo(int? id, int year, int departmentId)
        {
            var opportunities = await Repository.GetAllAsync(o => o.Id != id && o.DepartmentId == departmentId && o.DueDate.Year == year);
            opportunities = opportunities.OrderByDescending(x => x.SerialNo).ToList();
            if (opportunities.Count == 0)
            {
                return 1;
            }
            return opportunities.FirstOrDefault().SerialNo + 1;
        }

        public async Task<int> CheckSerialNo(int id, int year, int departmentId, int oldSerialNo)
        {
            var opportunities = await Repository.GetAllAsync(o => o.Id != id && o.DepartmentId == departmentId && o.DueDate.Year == year);
            if (opportunities.Where(o => o.SerialNo == oldSerialNo).Count() > 0)
            {
                opportunities = opportunities.OrderByDescending(x => x.SerialNo).ToList();
                return opportunities.FirstOrDefault().SerialNo + 1;
            }
            else
            {
                return oldSerialNo;
            }
        }

        public override async Task<bool> UpdateAsync(Opportunity entity, string userId = null)
        {
            Opportunity oldEntity = await _repository.GetFullItemById(entity.Id);
            if (entity.BidBond == null)
            {
                var bidBondEntity = _repository.DbContext.BidBonds.FirstOrDefault(bb => bb.OpportunityId == entity.Id);
                if (bidBondEntity != null)
                    _repository.DbContext.BidBonds.Remove(bidBondEntity);
            }

            if (entity.BookTender == null)
            {
                var bookTenderEntity = _repository.DbContext.BookTenders.FirstOrDefault(bt => bt.OpportunityId == entity.Id);
                if (bookTenderEntity != null)
                    _repository.DbContext.BookTenders.Remove(bookTenderEntity);
            }

            if (!entity.IsTechnicalApproved)
            {
                var products = Repository.DbContext.TechnicalSpecificationProducts.Where(c => c.TechnicalSpecificationId == entity.TechnicalSpecification.Id);
                Repository.DbContext.TechnicalSpecificationProducts.RemoveRange(products);

                if (entity.TechnicalSpecification != null && entity.TechnicalSpecification.Products != null && entity.TechnicalSpecification.Products.Count > 0)
                {
                    foreach (var product in entity.TechnicalSpecification.Products)
                    {
                        Repository.DbContext.TechnicalSpecificationProductOrigins.RemoveRange(Repository.DbContext.TechnicalSpecificationProductOrigins.Where(o => o.TechnicalSpecificationProductId == product.Id));
                    }
                }
            }
            else
            {
                entity.TechnicalSpecification = oldEntity.TechnicalSpecification;
            }
            Repository.DbContext.Entry(oldEntity).State = EntityState.Detached;

            return await _repository.UpdateAsync(entity, userId);
        }

        public async Task<Opportunity> GetByQuotationId(int quotationId)
        {
            var quotation = await Repository.DbContext.Quotations.FirstOrDefaultAsync(q => q.Id == quotationId);
            return await _repository.GetFullInfoById(quotation.OpportunityId);
        }

        public async Task<IList<Opportunity>> GetConvertedAsync(int customerId)
        {
            return await Repository.GetAllAsync(o => o.IsConverted && o.CustomerId == customerId);
        }

        public async Task<IList<Opportunity>> GetConvertedByUserAsync(string userId, int userDepartmentId, List<int> userDepartmentIds, int customerId)
        {
            return (await Repository
                .GetAllAsync(o => o.IsConverted &&
                o.CustomerId == customerId &&
                (o.CreatedById == userId ||
                (o.ProjectManagerId != null && o.ProjectManagerId == userId) ||
                (o.GuestId != null && o.GuestId == userId) ||
                (o.BidBond != null && o.BidBond.AssignedToId == userId) ||
                (o.BookTender != null && o.BookTender.AssignedToId == userId) ||
                o.SalesAgentId == userId ||
                o.DepartmentId == userDepartmentId ||
                userDepartmentIds.Contains(o.DepartmentId)),
                x => x.Type,
                x => x.Customer,
                x => x.SalesAgent,
                x => x.Country,
                x => x.Department,
                x => x.BidBond.Request,
                x => x.BookTender.Request,
                x => x.Status,
                x => x.CreatedBy,
                x => x.TechnicalSpecification.Products,
                x => x.RequestOffers,
                x => x.SupplierOffers))
                .OrderBy(o => o.DueDate).ToList();
        }
    }
}
