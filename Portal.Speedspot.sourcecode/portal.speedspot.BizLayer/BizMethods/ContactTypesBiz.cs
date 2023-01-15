using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class ContactTypesBiz : BizBaseT<ContactType>
    {
        public ContactTypesBiz(IContactTypesRepository repository) : base(repository)
        {

        }
    }
}
