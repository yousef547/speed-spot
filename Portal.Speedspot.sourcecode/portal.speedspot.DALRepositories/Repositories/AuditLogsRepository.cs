using Microsoft.EntityFrameworkCore;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class AuditLogsRepository : IAuditLogsRepository
    {
        public ApplicationDbContext _dbContext;
        public DbSet<Audit> dbSet;

        ApplicationDbContext IAuditLogsRepository.dbContext { get => _dbContext; set => _ = _dbContext; }

        public AuditLogsRepository(ApplicationDbContext context)
        {
            _dbContext = context;
            dbSet = context.Set<Audit>();
        }

        public async Task<Audit> FindByIdAsync(int id)
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

        public IList<Audit> GetAll(Expression<Func<Audit, bool>> predicate, params Expression<Func<Audit, object>>[] paths)
        {
            try
            {
                var query = GetQueryWithPredicate(predicate);
                query = AddIncludesToQuery(query, paths);
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public IList<Audit> GetAll(params Expression<Func<Audit, object>>[] paths)
        {
            try
            {
                var query = AddIncludesToQuery(dbSet, paths);
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public async Task<IList<Audit>> GetAllAsync(Expression<Func<Audit, bool>> predicate, params Expression<Func<Audit, object>>[] paths)
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

        public async Task<IList<Audit>> GetAllAsync(params Expression<Func<Audit, object>>[] paths)
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

        public async Task<Audit> GetFirstAsync(Expression<Func<Audit, bool>> predicate, params Expression<Func<Audit, object>>[] paths)
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

        public async Task<Audit> GetFirstOrDefaultAsync(Expression<Func<Audit, bool>> predicate, params Expression<Func<Audit, object>>[] paths)
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

        #region Private Methods
        private IQueryable<Audit> AddIncludesToQuery(IQueryable<Audit> query, params Expression<Func<Audit, object>>[] paths)
        {
            foreach (var path in paths)
            {
                query = query.Include(path);
            }
            return query;
        }
        private IQueryable<Audit> GetQueryWithPredicate(Expression<Func<Audit, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }
        #endregion
    }
}
