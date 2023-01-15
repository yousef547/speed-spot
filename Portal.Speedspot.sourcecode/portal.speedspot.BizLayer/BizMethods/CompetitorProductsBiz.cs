using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class CompetitorProductsBiz : BizBaseT<CompetitorProduct>
    {
        public CompetitorProductsBiz(ICompetitorProductsRepository repository) : base(repository)
        {

        }

        public async Task<IList<CompetitorProduct>> GetByCompetitorIdAsync(int competitorId)
        {
            var products = await Repository.GetAllAsync(cp => cp.CompetitorId == competitorId, x => x.Product);

            return products;
        }
    }
}
