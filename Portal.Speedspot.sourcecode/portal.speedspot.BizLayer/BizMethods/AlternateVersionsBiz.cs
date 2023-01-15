
using Microsoft.EntityFrameworkCore;

using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class AlternateVersionsBiz : BizBaseT<AlternateVersion>
    {
        private readonly IAlternateVersionsRepository _repository;

        public AlternateVersionsBiz(IAlternateVersionsRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async override Task<AlternateVersion> GetByIdAsync(int id)
        {
            return await _repository.GetFullItemById(id);
        }

        public async Task<IList<AlternateVersion>> GetByVersionIdAsync(int versionId)
        {
            return await Repository.DbContext.AlternateVersions
                .Include(x => x.Products)
                .ThenInclude(y => y.Product.TechnicalSpecificationProduct.Product)
                .Where(av => av.QuotationOfferVersionId == versionId)
                .ToListAsync();
        }

        public override Task<bool> UpdateAsync(AlternateVersion entity, string userId = null)
        {
            Repository.DbContext.AlternateVersionProducts.RemoveRange(Repository.DbContext.AlternateVersionProducts.Where(p => p.OfferId == entity.Id));
            Repository.DbContext.AlternateVersionCertificates.RemoveRange(Repository.DbContext.AlternateVersionCertificates.Where(p => p.OfferId == entity.Id));
            return base.UpdateAsync(entity, userId);
        }

        public async Task<int> GenerateSerialNo(int versionId)
        {
            var alternateVersionsCount = await Repository.DbContext.AlternateVersions
                .Where(av => av.QuotationOfferVersionId == versionId)
                .CountAsync();

            return alternateVersionsCount + 1;
        }
    }
}
