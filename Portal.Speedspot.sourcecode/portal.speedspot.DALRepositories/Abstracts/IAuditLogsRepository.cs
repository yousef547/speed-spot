using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Abstracts
{
    public interface IAuditLogsRepository
    {
        ApplicationDbContext dbContext { get; set; }
        IList<Audit> GetAll(Expression<Func<Audit, bool>> predicate, params Expression<Func<Audit, object>>[] paths);
        IList<Audit> GetAll(params Expression<Func<Audit, object>>[] paths);
        Task<IList<Audit>> GetAllAsync(Expression<Func<Audit, bool>> predicate, params Expression<Func<Audit, object>>[] paths);
        Task<IList<Audit>> GetAllAsync(params Expression<Func<Audit, object>>[] paths);
        Task<Audit> GetFirstOrDefaultAsync(Expression<Func<Audit, bool>> predicate, params Expression<Func<Audit, object>>[] paths);
        Task<Audit> GetFirstAsync(Expression<Func<Audit, bool>> predicate, params Expression<Func<Audit, object>>[] paths);
        Task<Audit> FindByIdAsync(int id);
    }
}
