using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class ProjectsBiz : BizBaseT<Project>
    {
        public ProjectsBiz(IProjectsRepository repository) : base(repository)
        {

        }

        public override async Task<IList<Project>> GetAllAsync()
        {
            return await Repository.GetAllAsync(x => !x.IsArchived,
            y => y.Customer,
            z => z.Opportunity,
            s => s.Quotation);
        }

        public override async Task<Project> GetByIdAsync(int id)
        {
            return await Repository.GetFirstOrDefaultAsync(x => x.Id == id,
            y => y.Customer,
            z => z.Opportunity,
            s => s.Quotation);
        }

        public async Task<Project> GetByOpportunityId(int id)
        {
            return await Repository.GetFirstOrDefaultAsync(x => x.OpportunityId == id,
           y => y.Customer,
           z => z.Opportunity,
           s => s.Quotation);
        }

        public async Task<Project> GetByQuotationId(int id)
        {
            return await Repository.GetFirstOrDefaultAsync(x => x.QuotationId == id,
           y => y.Customer,
           z => z.Opportunity,
           s => s.Quotation);
        }

        public async Task<Project> GetByCode(string code)
        {
            return await Repository.GetFirstOrDefaultAsync(p => p.Code == code);
        }
    }
}
