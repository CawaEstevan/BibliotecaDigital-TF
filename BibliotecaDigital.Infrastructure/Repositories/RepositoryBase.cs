using BibliotecaDigital.Domain.Entities;
using BibliotecaDigital.Domain.Interfaces;
using BibliotecaDigital.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaDigital.Infrastructure.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : EntityBase
    {
        protected readonly BibliotecaDbContext _context;
        protected readonly DbSet<T> _dbSet;

        protected RepositoryBase(BibliotecaDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task<IEnumerable<T>> SearchAsync(string searchTerm)
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
    }
}
