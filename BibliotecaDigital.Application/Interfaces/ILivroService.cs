using BibliotecaDigital.Application.ViewModels;

namespace BibliotecaDigital.Application.Interfaces
{
    public interface ILivroService
    {
        Task<IEnumerable<LivroViewModel>> GetAllAsync();
        Task<LivroViewModel?> GetByIdAsync(int id);
        Task<LivroViewModel> AddAsync(LivroViewModel viewModel);
        Task UpdateAsync(LivroViewModel viewModel);
        Task DeleteAsync(int id);
        Task<IEnumerable<LivroViewModel>> SearchAsync(string searchTerm);
        Task<IEnumerable<LivroViewModel>> GetByAutorIdAsync(int autorId);
        Task<LivroViewModel?> GetByISBNAsync(string isbn);
        Task<LivroViewModel?> GetByTituloEAutorAsync(string titulo, int autorId);
    }
}