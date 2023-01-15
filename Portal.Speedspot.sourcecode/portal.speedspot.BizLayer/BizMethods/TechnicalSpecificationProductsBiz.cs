using Microsoft.EntityFrameworkCore;
using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class TechnicalSpecificationProductsBiz : BizBaseT<TechnicalSpecificationProduct>
    {
        private readonly ITechnicalSpecificationProductsRepository _repository;
        public TechnicalSpecificationProductsBiz(ITechnicalSpecificationProductsRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IList<TechnicalSpecificationProduct>> GetByTechnicalSpecificationId(int technicalSpecificationId)
        {
            return await Repository.GetAllAsync(p => p.TechnicalSpecificationId == technicalSpecificationId, x => x.Product);
        }

        public async Task<IList<TechnicalSpecificationProduct>> GetByQuotationIdAsync(int quotationId)
        {
            var quotation = await Repository.DbContext.Quotations.FirstOrDefaultAsync(q => q.Id == quotationId);
            var technicalSpecs = await Repository.DbContext.TechnicalSpecifications.FirstOrDefaultAsync(t => t.OpportunityId == quotation.OpportunityId);
            return await Repository.GetAllAsync(p => p.TechnicalSpecificationId == technicalSpecs.Id, x => x.Product);
        }

        public override async Task<TechnicalSpecificationProduct> GetByIdAsync(int id)
        {
            return await _repository.GetFullItemByIdAsync(id);
        }
    }
}
