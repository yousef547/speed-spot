using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class ContactsBiz : BizBaseT<Contact>
    {
        public ContactsBiz(IContactsRepository repository) : base(repository)
        {

        }
    }
}
