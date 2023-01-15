using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class PerformanceLGsBiz : BizBaseT<PerformanceLG>
    {
        public PerformanceLGsBiz(IPerformanceLGsRepository repository) : base(repository)
        {

        }

        //public async Task<PerformanceLGRequest> GetRequestByIdAsync(int requestId)
        //{
        //    return Repository.DbContext.reques
        //}
    }
}
