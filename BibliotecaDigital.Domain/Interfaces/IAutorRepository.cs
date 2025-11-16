using BibliotecaDigital.Domain.Entities;

namespace BibliotecaDigital.Domain.Interfaces
{
    public interface IAutorRepository : IRepositoryBase<Autor>
    {
        Task<Autor?> GetByIdWithLivrosAsync(int id);
    }
}