using Microsoft.EntityFrameworkCore;
using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Infrastructures
{
    public class RepositoryBase<T> : IRepository<T> where T : EntityBase
    {
        public ApplicationDbContext _dbContext;
        public DbSet<T> dbSet;

        ApplicationDbContext IRepository<T>.DbContext { get => _dbContext; set => _ = _dbContext; }

        public RepositoryBase(ApplicationDbContext context)
        {
            _dbContext = context;
            dbSet = context.Set<T>();
        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths)
        {
            try
            {
                var query = GetQueryWithPredicate(predicate);
                query = AddIncludesToQuery(query, paths);
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public async Task<IList<T>> GetAllAsync(params Expression<Func<T, object>>[] paths)
        {
            try
            {
                var query = AddIncludesToQuery(dbSet, paths);
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths)
        {
            try
            {
                var query = GetQueryWithPredicate(predicate);
                query = AddIncludesToQuery(query, paths);
                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            { throw new Exception($"Couldn't retrieve entities: {ex.Message}"); }
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths)
        {
            try
            {
                var query = GetQueryWithPredicate(predicate);
                query = AddIncludesToQuery(query, paths);
                return await query.FirstAsync();
            }
            catch (Exception ex)
            { throw new Exception($"Couldn't retrieve entities: {ex.Message}"); }
        }

        public async Task<T> GetLastOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths)
        {
            try
            {
                var query = GetQueryWithPredicate(predicate);
                query = AddIncludesToQuery(query, paths);
                return await query.LastOrDefaultAsync();
            }
            catch (Exception ex)
            { throw new Exception($"Couldn't retrieve entities: {ex.Message}"); }
        }

        public async Task<T> GetLastAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths)
        {
            try
            {
                var query = GetQueryWithPredicate(predicate);
                query = AddIncludesToQuery(query, paths);
                return await query.LastAsync();
            }
            catch (Exception ex)
            { throw new Exception($"Couldn't retrieve entities: {ex.Message}"); }
        }

        public async Task<T> FindByIdAsync(int id)
        {
            try
            {
                return await dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public async Task<bool> AddAsync(T entity, string userId = null)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await _dbContext.AddAsync(entity);
                var result = await _dbContext.SaveChangesAsync(userId);

                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }

        public async Task<bool> UpdateAsync(T entity, string userId = null)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(UpdateAsync)} entity must not be null");
            }

            try
            {
                _dbContext.Update(entity);
                var result = await _dbContext.SaveChangesAsync(userId);

                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }

        public async Task<bool> DeleteAsync(T entity, string userId = null)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(DeleteAsync)} entity must not be null");
            }

            try
            {
                _dbContext.Remove(entity);
                var result = await _dbContext.SaveChangesAsync(userId);

                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }

        #region Private Methods
        private static IQueryable<T> AddIncludesToQuery(IQueryable<T> query, params Expression<Func<T, object>>[] paths)
        {
            foreach (var path in paths)
            {
                query = query.Include(path);
            }
            return query;
        }
        private IQueryable<T> GetQueryWithPredicate(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }
        #endregion
    }
}
