using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class CitiesBiz : BizBaseT<City>
    {
        public CitiesBiz(ICitiesRepository repository) : base(repository)
        {

        }

        public async Task<IList<City>> GetByCountryId(int countryId)
        {
            return await Repository.GetAllAsync(c => c.CountryId == countryId);
        }

        public override async Task<IList<City>> GetAllAsync()
        {
            return await Repository.GetAllAsync(x => x.Country);
        }
    }
}
