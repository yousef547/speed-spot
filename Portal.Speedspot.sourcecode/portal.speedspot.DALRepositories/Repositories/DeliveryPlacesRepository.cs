using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class DeliveryPlacesRepository : RepositoryBase<DeliveryPlace>, IDeliveryPlacesRepository
    {
        public DeliveryPlacesRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
