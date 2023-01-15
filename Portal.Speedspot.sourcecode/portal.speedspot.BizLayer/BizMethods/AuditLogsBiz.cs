using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.BizMethods
{
    public class AuditLogsBiz
    {
        private IAuditLogsRepository _repository;
        public IAuditLogsRepository Repository
        {
            get { return _repository; }
        }

        public AuditLogsBiz(IAuditLogsRepository repository)
        {
            _repository = repository;
        }

        public virtual async Task<IList<Audit>> GetAllAsync()
        {
            return await _repository.GetAllAsync(x => x.User);
        }

        public async Task<IList<Audit>> GetByDateAsync(DateTime dateSearch)
        {
            DateTime fromDate = new(dateSearch.Year, dateSearch.Month, dateSearch.Day, 0, 0, 0);
            DateTime toDate = new(dateSearch.Year, dateSearch.Month, dateSearch.Day, 23, 59, 59);
            return await _repository.GetAllAsync(l => l.DateTime >= fromDate && l.DateTime <= toDate, x => x.User);
        }

        public virtual async Task<Audit> GetByIdAsync(int id)
        {
            return await _repository.FindByIdAsync(id);
        }

        public virtual bool Exists(int id)
        {
            return _repository.GetAll(t => t.Id == id).Any(e => e.Id == id);
        }
    }
}
