using Microsoft.EntityFrameworkCore;
using portal.speedspot.DALRepositories.Abstracts;
using portal.speedspot.Models.Concretes;
using System;
using System.Threading.Tasks;

namespace portal.speedspot.DALRepositories.Repositories
{
    public class CompanyDataRepository : ICompanyDataRepository
    {
        public ApplicationDbContext _dbContext;
        public DbSet<CompanyData> dbSet;

        ApplicationDbContext ICompanyDataRepository.DbContext { get => _dbContext; set => _ = _dbContext; }

        public CompanyDataRepository(ApplicationDbContext context)
        {
            _dbContext = context;
            dbSet = context.Set<CompanyData>();
        }

        public async Task<CompanyData> GetAsync()
        {
            try
            {
                return await dbSet.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public async Task<bool> AddAsync(CompanyData entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await _dbContext.AddAsync(entity);
                var result = await _dbContext.SaveChangesAsync();

                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }

        public async Task<bool> UpdateAsync(CompanyData entity, string userId = null)
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
    }
}
