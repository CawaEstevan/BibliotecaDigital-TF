using BibliotecaDigital.Domain.Entities;
using BibliotecaDigital.Domain.Interfaces;
using BibliotecaDigital.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaDigital.Infrastructure.Repositories
{
    public class AutorRepository : RepositoryBase<Autor>, IAutorRepository
    {
        public AutorRepository(BibliotecaDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Autor>> GetAllAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task<Autor?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(a => a.Livros)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Autor?> GetByIdWithLivrosAsync(int id)
        {
            return await _dbSet
                .Include(a => a.Livros)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public override async Task<IEnumerable<Autor>> SearchAsync(string searchTerm)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(a => a.Nome.Contains(searchTerm) || 
                           a.Email.Contains(searchTerm) ||
                           a.Nacionalidade.Contains(searchTerm) ||
                           a.Biografia.Contains(searchTerm))
                .ToListAsync();
        }
    }
}
