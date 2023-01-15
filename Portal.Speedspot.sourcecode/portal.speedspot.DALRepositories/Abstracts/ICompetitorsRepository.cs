using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Abstracts
{
    public interface ICompetitorsRepository : IRepository<Competitor>
    {
        public Task<Competitor> GetFullItemById(int id);

        public Task<IList<Competitor>> GetFullParents();
    }
}
