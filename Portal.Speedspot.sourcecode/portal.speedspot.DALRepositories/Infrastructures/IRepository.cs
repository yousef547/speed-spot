using portal.speedspot.Models.BasesAndAbstracts;
using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Infrastructures
{
    public interface IRepository<T> where T : EntityBase
    {
        ApplicationDbContext DbContext { get; set; }
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths);
        Task<IList<T>> GetAllAsync(params Expression<Func<T, object>>[] paths);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths);
        Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths);
        Task<T> GetLastOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths);
        Task<T> GetLastAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths);
        Task<T> FindByIdAsync(int id);
        Task<bool> AddAsync(T entity, string userId = null);
        Task<bool> UpdateAsync(T entity, string userId = null);
        Task<bool> DeleteAsync(T entity, string userId = null);
    }
}
