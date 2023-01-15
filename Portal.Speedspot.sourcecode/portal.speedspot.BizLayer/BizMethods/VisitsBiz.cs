using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class VisitsBiz : BizBaseT<Visit>
    {
        public VisitsBiz(IVisitsRepository repository) : base(repository)
        {

        }

        public override async Task<IList<Visit>> GetAllAsync()
        {
            return await Repository.GetAllAsync(x => x.SalesAgent, x => x.Customer, x => x.Reason);
        }

        public async Task<IList<Visit>> GetBySalesAgentAsync(string id)
        {
            return await Repository.GetAllAsync(v => v.SalesAgentId == id,
                x => x.SalesAgent, x => x.Customer, x => x.Reason);
        }

        public async Task<IList<Visit>> GetByUserAsync(string id, int departmentId, List<int> departmentIds)
        {
            return await Repository.GetAllAsync(v => v.SalesAgentId == id ||
            v.Customer.Departments.Select(d => d.DepartmentId).Any(d => departmentIds.Contains(d)) ||
            v.Customer.Departments.Select(d => d.DepartmentId).Contains(departmentId),
                x => x.SalesAgent, x => x.Customer.Departments, x => x.Reason);
        }

        public override async Task<Visit> GetByIdAsync(int id)
        {
            return await Repository.GetFirstOrDefaultAsync(v=>v.Id==id,
                x=>x.Customer.Departments,
                x=>x.Reason,
                x=>x.SalesAgent);
        }
    }
}
