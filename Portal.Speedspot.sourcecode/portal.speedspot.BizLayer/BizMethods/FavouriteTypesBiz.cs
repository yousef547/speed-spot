using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class FavouriteTypesBiz : BizBaseT<FavouriteType>
    {
        public FavouriteTypesBiz(IFavouriteTypesRepository repository) : base(repository)
        {

        }

        public async Task<FavouriteType> GetByEnumValue(int enumValue)
        {
            return await Repository.GetFirstAsync(s => s.EnumValue == enumValue);
        }
    }
}
