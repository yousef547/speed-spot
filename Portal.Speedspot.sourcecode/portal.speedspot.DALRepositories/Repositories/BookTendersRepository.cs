using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class BookTendersRepository : RepositoryBase<BookTender>, IBookTendersRepository
    {
        public BookTendersRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
