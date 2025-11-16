using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Domain.Interfaces
{
    public interface ILivroRepository : IRepositoryBase<Livro>
    {
        Task<IEnumerable<Livro>> GetByAutorIdAsync(int autorId);
    }
}