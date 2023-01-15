using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class CustomerPoBiz : BizBaseT<CustomerPo>
    {
        private static ICustomerPoRepositery _repository;
        public CustomerPoBiz(ICustomerPoRepositery repositery) : base(repositery)
        {
            _repository = repositery;
        }

        public async Task<string> GenerateNewCode(int departmentId)
        {
            var departments = await Repository.DbContext.Departments.FindAsync(departmentId);
            string codePrefix = DateTime.UtcNow.ToString("yyMMdd") + "-C";
            var CustomerP = (await Repository
                .GetAllAsync(j => j.Code.StartsWith(codePrefix)))
                .Select(x => x.Code[codePrefix.Length..])
                .Select(str => int.Parse(str))
                .ToArray();
           
            int nextCode = CustomerP.Length > 0 ? CustomerP.Max() + 1 : 1;
            return $"{departments.Code}{codePrefix}{nextCode:00}";
        }
    }
}
