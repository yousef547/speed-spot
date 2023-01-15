using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class FavouriteTypesRepository : RepositoryBase<FavouriteType>, IFavouriteTypesRepository
    {
        public FavouriteTypesRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
