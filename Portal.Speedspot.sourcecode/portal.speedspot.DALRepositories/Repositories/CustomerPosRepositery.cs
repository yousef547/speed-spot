using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class CustomerPosRepositery: RepositoryBase<CustomerPo>, ICustomerPoRepositery
    {
        public CustomerPosRepositery(ApplicationDbContext context):base(context)
        {

        }
    }
}
