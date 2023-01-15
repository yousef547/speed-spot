using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class ProductCategoriesRepository : RepositoryBase<ProductCategory>, IProductCategoriesRepository
    {
        public ProductCategoriesRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
