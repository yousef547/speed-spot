using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class OrganizationTypesBiz : BizBaseT<OrganizationType>
    {
        public OrganizationTypesBiz(IOrganizationTypesRepository repository) : base(repository)
        {

        }
    }
}
