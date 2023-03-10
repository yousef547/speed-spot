using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class BanksRepository : RepositoryBase<Bank>, IBanksRepository
    {
        public BanksRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
