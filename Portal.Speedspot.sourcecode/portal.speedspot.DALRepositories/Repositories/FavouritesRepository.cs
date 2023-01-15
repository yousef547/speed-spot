using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class FavouritesRepository : RepositoryBase<Favourite>, IFavouritesRepository
    {
        public FavouritesRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
