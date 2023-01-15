using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class CountriesBiz : BizBaseT<Country>
    {
        public CountriesBiz(ICountriesRepository repository) : base(repository)
        {

        }

        public async Task<IList<Country>> GetAllUnarchivedExceptAsync(List<int> countryIds)
        {
            return await Repository.GetAllAsync(c => !c.IsArchived && !countryIds.Contains(c.Id));
        }
    }
}
