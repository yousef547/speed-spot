using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class ProductOriginsBiz : BizBaseT<ProductOrigin>
    {
        public ProductOriginsBiz(IProductOriginsRepository repository) : base(repository)
        {

        }
    }
}
