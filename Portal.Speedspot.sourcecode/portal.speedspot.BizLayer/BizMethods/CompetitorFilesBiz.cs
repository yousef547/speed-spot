using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class CompetitorFilesBiz : BizBaseT<CompetitorFile>
    {
        public CompetitorFilesBiz(ICompetitorFilesRepository repository) : base(repository)
        {

        }

        public async Task<IList<CompetitorFile>> GetByCompetitorIdAsync(int competitorId)
        {
            var files = await Repository.GetAllAsync(cf => cf.CompetitorId == competitorId, x => x.Attachment);

            return files;
        }
    }
}
