using portal.speedspot.Models.Concretes;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Abstracts
{
    public interface ICompanyDataRepository
    {
        ApplicationDbContext DbContext { get; set; }
        Task<CompanyData> GetAsync();
        Task<bool> AddAsync(CompanyData entity);
        Task<bool> UpdateAsync(CompanyData entity, string userId = null);
    }
}
