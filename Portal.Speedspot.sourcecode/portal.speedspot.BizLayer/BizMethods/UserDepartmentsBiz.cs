using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class UserDepartmentsBiz : BizBaseT<UserDepartment>
    {
        public UserDepartmentsBiz(IUserDepartmentsRepository repository) : base(repository)
        {

        }

        public async Task<bool> IsInDepartmentAsync(string userId, int departmentId)
        {
            return (await Repository.GetAllAsync(ud => ud.UserId == userId && ud.DepartmentId == departmentId)).Count > 0;
        }

        public async Task<IList<UserDepartment>> GetByUserAsync(string userId)
        {
            return await Repository.GetAllAsync(ud => ud.UserId == userId);
        }

        public async Task<IList<UserDepartment>> GetFullByUserAsync(string userId)
        {
            return await Repository.GetAllAsync(ud => ud.UserId == userId, x => x.Department);
        }

        public async Task<bool> DeleteByUserAsync(string userId)
        {
            var userDepartments = await Repository.GetAllAsync(ud => ud.UserId == userId);
            foreach (var userDepartment in userDepartments)
            {
                await Repository.DeleteAsync(userDepartment);
            }

            return true;
        }
    }
}
