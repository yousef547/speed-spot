using Microsoft.EntityFrameworkCore;
using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class QuotationOfferVersionsBiz : BizBaseT<QuotationOfferVersion>
    {
        private readonly IQuotationOfferVersionsRepository _repository;
        public QuotationOfferVersionsBiz(IQuotationOfferVersionsRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public override async Task<QuotationOfferVersion> GetByIdAsync(int id)
        {
            return await _repository.GetFullItemById(id);
        }

        public override Task<bool> UpdateAsync(QuotationOfferVersion entity, string userId = null)
        {
            Repository.DbContext.QuotationOfferProducts.RemoveRange(Repository.DbContext.QuotationOfferProducts.Where(p => p.OfferId == entity.Id));
            Repository.DbContext.QuotationOfferCertificates.RemoveRange(Repository.DbContext.QuotationOfferCertificates.Where(p => p.OfferId == entity.Id));
            return base.UpdateAsync(entity, userId);
        }

        public async Task<QuotationOfferVersion> GetSelectedQuotationVersion(int quotationId)
        {
            var quotationOffers = await Repository.DbContext.QuotationOffers.Include(x => x.Versions).Where(v => v.QuotationId == quotationId).ToListAsync();
            var selectedVersion = quotationOffers.SelectMany(o => o.Versions).FirstOrDefault(v => v.IsSelected == true);
            if (selectedVersion is null)
            {
                selectedVersion = quotationOffers.LastOrDefault().Versions.LastOrDefault();
            }

            return selectedVersion;
        }

        public async Task<IList<QuotationOfferProduct>> GetVersionProductsAsync(int id)
        {
            return await Repository.DbContext.QuotationOfferProducts.Where(p => p.OfferId == id).ToListAsync();
        }
    }
}
