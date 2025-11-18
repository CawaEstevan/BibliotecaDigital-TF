using BibliotecaDigital.Application.Interfaces;
using BibliotecaDigital.Application.ViewModels;
using BibliotecaDigital.Domain.Entities;
using BibliotecaDigital.Domain.Interfaces;
using Mapster;

namespace BibliotecaDigital.Application.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;

        public LivroService(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public async Task<IEnumerable<LivroViewModel>> GetAllAsync()
        {
            var livros = await _livroRepository.GetAllAsync();
            
            var livrosViewModel = livros.Select(livro => new LivroViewModel
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                ISBN = livro.ISBN,
                Editora = livro.Editora,
                AnoPublicacao = livro.AnoPublicacao,
                Preco = livro.Preco,
                NumeroPaginas = livro.NumeroPaginas,
                AutorId = livro.AutorId,
                NomeAutor = livro.Autor?.Nome
            }).ToList();
            
            return livrosViewModel;
        }

        public async Task<LivroViewModel?> GetByIdAsync(int id)
        {
            var livro = await _livroRepository.GetByIdAsync(id);
            
            if (livro == null)
                return null;
            
            return new LivroViewModel
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                ISBN = livro.ISBN,
                Editora = livro.Editora,
                AnoPublicacao = livro.AnoPublicacao,
                Preco = livro.Preco,
                NumeroPaginas = livro.NumeroPaginas,
                AutorId = livro.AutorId,
                NomeAutor = livro.Autor?.Nome,
                Autor = livro.Autor != null ? new AutorViewModel
                {
                    Id = livro.Autor.Id,
                    Nome = livro.Autor.Nome,
                    Email = livro.Autor.Email,
                    Nacionalidade = livro.Autor.Nacionalidade,
                    Biografia = livro.Autor.Biografia,
                    DataNascimento = livro.Autor.DataNascimento
                } : null
            };
        }

        public async Task<LivroViewModel> AddAsync(LivroViewModel viewModel)
        {
            var livro = viewModel.Adapt<Livro>();
            var createdLivro = await _livroRepository.AddAsync(livro);
            
            return new LivroViewModel
            {
                Id = createdLivro.Id,
                Titulo = createdLivro.Titulo,
                ISBN = createdLivro.ISBN,
                Editora = createdLivro.Editora,
                AnoPublicacao = createdLivro.AnoPublicacao,
                Preco = createdLivro.Preco,
                NumeroPaginas = createdLivro.NumeroPaginas,
                AutorId = createdLivro.AutorId
            };
        }

        public async Task UpdateAsync(LivroViewModel viewModel)
        {
            var livro = viewModel.Adapt<Livro>();
            await _livroRepository.UpdateAsync(livro);
        }

        public async Task DeleteAsync(int id)
        {
            await _livroRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<LivroViewModel>> SearchAsync(string searchTerm)
        {
            var livros = await _livroRepository.SearchAsync(searchTerm);
            
            var livrosViewModel = livros.Select(livro => new LivroViewModel
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                ISBN = livro.ISBN,
                Editora = livro.Editora,
                AnoPublicacao = livro.AnoPublicacao,
                Preco = livro.Preco,
                NumeroPaginas = livro.NumeroPaginas,
                AutorId = livro.AutorId,
                NomeAutor = livro.Autor?.Nome
            }).ToList();
            
            return livrosViewModel;
        }

        public async Task<IEnumerable<LivroViewModel>> GetByAutorIdAsync(int autorId)
        {
            var livros = await _livroRepository.GetByAutorIdAsync(autorId);
            
            var livrosViewModel = livros.Select(livro => new LivroViewModel
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                ISBN = livro.ISBN,
                Editora = livro.Editora,
                AnoPublicacao = livro.AnoPublicacao,
                Preco = livro.Preco,
                NumeroPaginas = livro.NumeroPaginas,
                AutorId = livro.AutorId,
                NomeAutor = livro.Autor?.Nome
            }).ToList();
            
            return livrosViewModel;
        }


        public async Task<LivroViewModel?> GetByISBNAsync(string isbn)
        {
            var livros = await _livroRepository.GetAllAsync();
            

            var isbnLimpo = isbn.Replace("-", "").Replace(" ", "");
            
            var livro = livros.FirstOrDefault(l => 
                l.ISBN.Replace("-", "").Replace(" ", "")
                    .Equals(isbnLimpo, StringComparison.OrdinalIgnoreCase));
            
            if (livro == null)
                return null;
            
            return new LivroViewModel
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                ISBN = livro.ISBN,
                Editora = livro.Editora,
                AnoPublicacao = livro.AnoPublicacao,
                Preco = livro.Preco,
                NumeroPaginas = livro.NumeroPaginas,
                AutorId = livro.AutorId,
                NomeAutor = livro.Autor?.Nome
            };
        }

        public async Task<LivroViewModel?> GetByTituloEAutorAsync(string titulo, int autorId)
        {
            var livros = await _livroRepository.GetByAutorIdAsync(autorId);
            
            var livro = livros.FirstOrDefault(l => 
                l.Titulo.Trim().Equals(titulo.Trim(), StringComparison.OrdinalIgnoreCase));
            
            if (livro == null)
                return null;
            
            return new LivroViewModel
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                ISBN = livro.ISBN,
                Editora = livro.Editora,
                AnoPublicacao = livro.AnoPublicacao,
                Preco = livro.Preco,
                NumeroPaginas = livro.NumeroPaginas,
                AutorId = livro.AutorId,
                NomeAutor = livro.Autor?.Nome
            };
        }
    }
}