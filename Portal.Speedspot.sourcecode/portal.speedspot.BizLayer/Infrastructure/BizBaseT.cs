using portal.speedspot.DALRepositories;
using portal.speedspot.DALRepositories.Infrastructures;
using portal.speedspot.Models.BasesAndAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.BizLayer.Infrastructure
{
    public class BizBaseT<T> where T : EntityBase, new()
    {
        private IRepository<T> _repository;
        public IRepository<T> Repository
        {
            get { return _repository; }
        }

        public BizBaseT(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<bool> AddAsync(T entity, string userId = null)
        {
            return await _repository.AddAsync(entity, userId);
        }

        public virtual async Task<IList<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public virtual async Task<IList<T>> GetAllUnarchivedAsync()
        {
            return await _repository.GetAllAsync(t => t.IsArchived == false);
        }

        public virtual async Task<IList<T>> GetAllArchivedAsync()
        {
            return await _repository.GetAllAsync(t => t.IsArchived == true);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _repository.FindByIdAsync(id);
        }

        public virtual async Task<bool> UpdateAsync(T entity, string userId = null)
        {
            return await _repository.UpdateAsync(entity, userId);
        }

        public virtual async Task<bool> DeleteAsync(int id, string userId = null)
        {
            var entity = await GetByIdAsync(id);
            return await _repository.DeleteAsync(entity, userId);
        }

        public virtual async Task<bool> ArchiveAsync(int id, string userId = null)
        {
            var entity = await GetByIdAsync(id);
            entity.IsArchived = true;
            return await _repository.UpdateAsync(entity, userId);
        }

        public virtual async Task<bool> UnarchiveAsync(int id, string userId = null)
        {
            var entity = await GetByIdAsync(id);
            entity.IsArchived = false;
            return await _repository.UpdateAsync(entity, userId);
        }

        public async virtual Task<bool> ExistsAsync(int id)
        {
            return (await _repository.GetAllAsync(t => t.Id == id)).Any(e => e.Id == id);
        }
    }
}
