using BibliotecaDigital.Application.ViewModels;

namespace BibliotecaDigital.Application.Interfaces
{
    public interface IAutorService
    {
        Task<IEnumerable<AutorViewModel>> GetAllAsync();
        Task<AutorViewModel?> GetByIdAsync(int id);
        Task<AutorViewModel> AddAsync(AutorViewModel viewModel);
        Task UpdateAsync(AutorViewModel viewModel);
        Task DeleteAsync(int id);
        Task<IEnumerable<AutorViewModel>> SearchAsync(string searchTerm);
        Task<AutorViewModel?> GetByEmailAsync(string email);
        Task<AutorViewModel?> GetByNomeAsync(string nome);
    }
}