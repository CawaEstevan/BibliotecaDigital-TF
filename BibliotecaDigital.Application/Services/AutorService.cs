using BibliotecaDigital.Application.Interfaces;
using BibliotecaDigital.Application.ViewModels;
using BibliotecaDigital.Domain.Entities;
using BibliotecaDigital.Domain.Interfaces;
using Mapster;

namespace BibliotecaDigital.Application.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _autorRepository;

        public AutorService(IAutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public async Task<IEnumerable<AutorViewModel>> GetAllAsync()
        {
            var autores = await _autorRepository.GetAllAsync();
            return autores.Adapt<IEnumerable<AutorViewModel>>();
        }

        public async Task<AutorViewModel?> GetByIdAsync(int id)
        {
            var autor = await _autorRepository.GetByIdAsync(id);
            
            if (autor == null)
                return null;

            // Mapear manualmente para evitar referência circular
            var autorViewModel = new AutorViewModel
            {
                Id = autor.Id,
                Nome = autor.Nome,
                Email = autor.Email,
                Nacionalidade = autor.Nacionalidade,
                Biografia = autor.Biografia,
                DataNascimento = autor.DataNascimento,
                Livros = autor.Livros?.Select(l => new LivroViewModel
                {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    ISBN = l.ISBN,
                    Editora = l.Editora,
                    AnoPublicacao = l.AnoPublicacao,
                    Preco = l.Preco,
                    NumeroPaginas = l.NumeroPaginas,
                    AutorId = l.AutorId,
                    NomeAutor = autor.Nome
                }).ToList() ?? new List<LivroViewModel>()
            };

            return autorViewModel;
        }

        public async Task<AutorViewModel> AddAsync(AutorViewModel viewModel)
        {
            var autor = viewModel.Adapt<Autor>();
            var createdAutor = await _autorRepository.AddAsync(autor);
            return createdAutor.Adapt<AutorViewModel>();
        }

        public async Task UpdateAsync(AutorViewModel viewModel)
        {
            var autor = viewModel.Adapt<Autor>();
            await _autorRepository.UpdateAsync(autor);
        }

        public async Task DeleteAsync(int id)
        {
            await _autorRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<AutorViewModel>> SearchAsync(string searchTerm)
        {
            var autores = await _autorRepository.SearchAsync(searchTerm);
            return autores.Adapt<IEnumerable<AutorViewModel>>();
        }
    }
}
