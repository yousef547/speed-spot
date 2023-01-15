using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using portal.speedspot.BizLayer.Infrastructure;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class PerformaInvoicesBiz : BizBaseT<PerformaInvoice>
    {
        public PerformaInvoicesBiz(IPerformaInvoicesRepository repository) : base(repository)
        {

        }

        public async Task<string> GenerateCode(int departmentId, DateTime invoiceDate)
        {
            var department = await Repository.DbContext.Departments
                .FirstOrDefaultAsync(d => d.Id == departmentId);

            string codePrefix = invoiceDate.ToString("yy") + department.Code;

            var invoiceIndices = (await Repository
                .GetAllAsync(j => j.GeneratedCode.StartsWith(codePrefix)))
                .Select(x => x.GeneratedCode[codePrefix.Length..])
                .Select(str => int.Parse(str))
            .ToArray();

            int nextCode = invoiceIndices.Length > 0 ? invoiceIndices.Max() + 1 : 1;
            return $"{codePrefix}{nextCode:000}";
        }
    }
}
