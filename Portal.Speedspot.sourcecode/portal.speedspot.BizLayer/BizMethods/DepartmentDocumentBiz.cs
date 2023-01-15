using Microsoft.EntityFrameworkCore;
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
    public class DepartmentDocumentBiz : BizBaseT<DepartmentDocument>
    {
        public DepartmentDocumentBiz(IDepartmentDocumentRepositery repositery) : base(repositery)
        {

        }

        public async Task<IList<DepartmentDocument>> GetAllDocumentByDepartmentId(int dpartmentId)
        {
            return await Repository.GetAllAsync(x => !x.IsArchived && x.DepartmentId == dpartmentId,
                c => c.Attachment);
        }
        public async Task<IList<DepartmentDocument>> GetAllDocumentByDepartmentIdExpiryDate(int dpartmentId)
        {
            return await Repository.GetAllAsync(x => !x.IsArchived && x.DepartmentId == dpartmentId && x.ExpiryDate <= DateTime.UtcNow.AddDays(30),
                c => c.Attachment);
        }
        public async Task<bool> DeleteAllById(int departmentId, string userId = null)
        {
            var document = await Repository.GetAllAsync(x=> x.DepartmentId == departmentId);
            Repository.DbContext.RemoveRange(document);
            return await Repository.DbContext.SaveChangesAsync(userId) > 0;
        }

        public async Task<bool> AddAllDocumentsAsync(List<DepartmentDocument> documents, string userId = null)
        {
            await Repository.DbContext.AddRangeAsync(documents);
            return await Repository.DbContext.SaveChangesAsync(userId) > 0;

        }
    }
}
