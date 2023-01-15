using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class TreasuriesRepository : RepositoryBase<Treasury>, ITreasuriesRepository
    {
        public TreasuriesRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
