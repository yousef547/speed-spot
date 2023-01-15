using Microsoft.EntityFrameworkCore;

using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class CompetitorOffersBiz : BizBaseT<CompetitorOffer>
    {
        private readonly ICompetitorOffersRepository _repository;

        public CompetitorOffersBiz(ICompetitorOffersRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async override Task<CompetitorOffer> GetByIdAsync(int id)
        {
            return await _repository.GetFullItemById(id);
        }

        public async Task<IList<CompetitorOffer>> GetByQuotationIdAsync(int quotationId)
        {
            return await _repository.GetFullByQuotationIdAsync(quotationId);
        }

        public async override Task<bool> UpdateAsync(CompetitorOffer entity, string userId = null)
        {
            Repository.DbContext.CompetitorOfferCertificates.RemoveRange(Repository.DbContext.CompetitorOfferCertificates.Where(x => x.OfferId == entity.Id));

            Repository.DbContext.CompetitorOfferProducts.RemoveRange(Repository.DbContext.CompetitorOfferProducts.Where(x => x.OfferId == entity.Id));

            return await base.UpdateAsync(entity, userId);
        }

        public async Task<bool> UpdateOfferProducts(List<CompetitorOfferProduct> products)
        {
            foreach(var item in products.Select(x=>x.OfferId).Distinct())
            {
                var oldProducts = Repository.DbContext.CompetitorOfferProducts.Where(x=>x.OfferId == item);
                Repository.DbContext.CompetitorOfferProducts.RemoveRange(oldProducts);
            }

            await Repository.DbContext.CompetitorOfferProducts.AddRangeAsync(products);
            
            return await Repository.DbContext.SaveChangesAsync() > 0;
        }


    }
}
