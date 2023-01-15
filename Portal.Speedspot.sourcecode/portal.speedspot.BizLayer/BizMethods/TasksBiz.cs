using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class TasksBiz : BizBaseT<Tasks>
    {
        public TasksBiz(ITasksRepository repository) : base(repository)
        {

        }
        public async Task<bool> DoneAsync(int id)
        {
            var task = await Repository.FindByIdAsync(id);
            task.IsDone = true;
            task.DoneDate = DateTime.Now;
            return await Repository.UpdateAsync(task);
        }


        public async Task<bool> UnDoAsync(int id)
        {
            var task = await Repository.FindByIdAsync(id);
            task.IsDone = false;
            task.DoneDate = DateTime.Now;
            return await Repository.UpdateAsync(task);

        }

        public async Task<IList<Tasks>> GetAssignedToUserAsync(string userId)
        {
            return (await Repository.GetAllAsync(t => !t.IsArchived &&
            t.AssignedToId == userId &&
            (!t.IsDone || !(t.IsDone && t.Deadline <= DateTime.UtcNow.AddMonths(-2))),
            x => x.CreatedBy)).OrderBy(o => o.IsDone).ThenBy(o => o.Deadline).ToList();
        }

        public async Task<IList<Tasks>> GetMyTeamTasksAsync(List<string> userIds)
        {
            return (await Repository.GetAllAsync(t => !t.IsArchived &&
            userIds.Contains(t.AssignedToId), x => x.CreatedBy, x => x.AssignedTo))
            .OrderBy(o => o.IsDone).ThenBy(o => o.Deadline)
            .ToList();
        }

        public override async Task<Tasks> GetByIdAsync(int id)
        {
            return await Repository.GetFirstOrDefaultAsync(t => t.Id == id, x => x.CreatedBy);
        }
    }
}
