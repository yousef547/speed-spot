using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class CitiesRepository : RepositoryBase<City>, ICitiesRepository
    {
        public CitiesRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
