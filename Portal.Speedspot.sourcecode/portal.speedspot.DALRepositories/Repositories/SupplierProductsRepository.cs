using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class SupplierProductsRepository : RepositoryBase<SupplierProduct>, ISupplierProductsRepository
    {
        public SupplierProductsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
