using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class CompanyDataBiz
    {
        private readonly ICompanyDataRepository _repository;

        public ICompanyDataRepository Repository
        {
            get { return _repository; }
        }

        public CompanyDataBiz(ICompanyDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<CompanyData> GetAsync()
        {
            return await _repository.GetAsync();
        }

        public async Task<bool> AddAsync(CompanyData entity)
        {
            return await _repository.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(CompanyData entity, string userId = null)
        {
            return await _repository.UpdateAsync(entity, userId);
        }
    }
}
