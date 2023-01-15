using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class UserSupervisorsBiz : BizBaseT<UserSupervisor>
    {
        public UserSupervisorsBiz(IUserSupervisorsRepository repository) : base(repository)
        {

        }

        public async Task DeleteByUserAsync(string id)
        {
            var userSupervisors = await Repository.GetAllAsync(us => us.UserId == id);
            foreach(var supervisor in userSupervisors)
            {
                await Repository.DeleteAsync(supervisor);
            }
        }
    }
}
