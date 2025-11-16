using BibliotecaDigital.Domain.Entities;
using BibliotecaDigital.Domain.Interfaces;
using BibliotecaDigital.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaDigital.Infrastructure.Repositories
{
    public class LivroRepository : RepositoryBase<Livro>, ILivroRepository
    {
        public LivroRepository(BibliotecaDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Livro>> GetAllAsync()
        {
            return await _dbSet
                .Include(l => l.Autor)
                .AsNoTracking()
                .ToListAsync();
        }

        public override async Task<Livro?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(l => l.Autor)
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Livro>> GetByAutorIdAsync(int autorId)
        {
            return await _dbSet
                .Include(l => l.Autor)
                .AsNoTracking()
                .Where(l => l.AutorId == autorId)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Livro>> SearchAsync(string searchTerm)
        {
            return await _dbSet
                .Include(l => l.Autor)
                .AsNoTracking()
                .Where(l => l.Titulo.Contains(searchTerm) || 
                           l.ISBN.Contains(searchTerm) ||
                           l.Editora.Contains(searchTerm) ||
                           (l.Autor != null && l.Autor.Nome.Contains(searchTerm)))
                .ToListAsync();
        }
    }
}
