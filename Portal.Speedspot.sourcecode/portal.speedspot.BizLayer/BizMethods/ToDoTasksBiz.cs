using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class ToDoTasksBiz : BizBaseT<ToDoTask>
    {
        public ToDoTasksBiz(IToDoTasksRepository repository) : base(repository)
        {

        }

        public async Task<IList<ToDoTask>> GetPendingOnUser(int employeeTypeId)
        {
            return await Repository.GetAllAsync(t => t.IsArchived == false && t.IsApproved == false && t.IsDone == false && t.PendingOnTypeId == employeeTypeId);
        }

        public async Task<IList<ToDoTask>> GetAssignedToUser(string userId)
        {
            return await Repository.GetAllAsync(t => t.IsArchived == false && t.IsRejected == false && t.AssignedToId == userId && t.IsApproved == true);
        }

        public async Task<bool> ApproveAsync(int id)
        {
            var task = await Repository.FindByIdAsync(id);
            task.IsApproved = true;
            task.IsRejected = false;
            return await Repository.UpdateAsync(task);
        }

        public async Task<bool> RejectAsync(int id)
        {
            var task = await Repository.FindByIdAsync(id);
            task.IsRejected = true;
            task.IsApproved = false;
            return await Repository.UpdateAsync(task);
        }

        public async Task<bool> DoneAsync(int id, bool isDone)
        {
            var task = await Repository.FindByIdAsync(id);
            task.IsDone = isDone;
            if (isDone)
                task.DoneDate = DateTime.UtcNow;
            else
                task.DoneDate = null;

            return await Repository.UpdateAsync(task);
        }
    }
}
