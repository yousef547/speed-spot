using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class PartnerEmployeesBiz : BizBaseT<PartnerEmployee>
    {
        public PartnerEmployeesBiz(IPartnerEmployeesRepository repository) : base(repository)
        {

        }
    }
}
