using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BibliotecaDigital.Application.Interfaces;
using BibliotecaDigital.Application.ViewModels;

namespace BibliotecaDigital.Web.Controllers
{
    public class LivrosController : Controller
    {
        private readonly ILivroService _livroService;
        private readonly IAutorService _autorService;

        public LivrosController(ILivroService livroService, IAutorService autorService)
        {
            _livroService = livroService;
            _autorService = autorService;
        }

        // GET: Livros
        public async Task<IActionResult> Index()
        {
            var livros = await _livroService.GetAllAsync();
            return View(livros);
        }

        // GET: Livros/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var livro = await _livroService.GetByIdAsync(id);
            if (livro == null)
                return NotFound();

            return View(livro);
        }

        // GET: Livros/Create
        public async Task<IActionResult> Create()
        {
            await PopulateAutoresDropdown();
            return View();
        }

        // POST: Livros/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LivroViewModel livroViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _livroService.AddAsync(livroViewModel);
                    TempData["SuccessMessage"] = "Livro cadastrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Erro ao cadastrar livro: {ex.Message}";
                }
            }

            await PopulateAutoresDropdown(livroViewModel.AutorId);
            return View(livroViewModel);
        }

        // GET: Livros/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var livro = await _livroService.GetByIdAsync(id);
            if (livro == null)
                return NotFound();

            await PopulateAutoresDropdown(livro.AutorId);
            return View(livro);
        }

        // POST: Livros/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LivroViewModel livroViewModel)
        {
            if (id != livroViewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _livroService.UpdateAsync(livroViewModel);
                    TempData["SuccessMessage"] = "Livro atualizado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Erro ao atualizar livro: {ex.Message}";
                }
            }

            await PopulateAutoresDropdown(livroViewModel.AutorId);
            return View(livroViewModel);
        }

        // GET: Livros/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var livro = await _livroService.GetByIdAsync(id);
            if (livro == null)
                return NotFound();

            return View(livro);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _livroService.DeleteAsync(id);
                TempData["SuccessMessage"] = "Livro excluído com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao excluir livro: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Livros/Search
        public async Task<IActionResult> Search(string searchTerm)
        {
            IEnumerable<LivroViewModel> livros;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                livros = await _livroService.GetAllAsync();
            }
            else
            {
                livros = await _livroService.SearchAsync(searchTerm);
            }

            return PartialView("_LivrosListPartial", livros);
        }

        private async Task PopulateAutoresDropdown(int? autorIdSelecionado = null)
        {
            var autores = await _autorService.GetAllAsync();
            ViewBag.Autores = new SelectList(autores, "Id", "Nome", autorIdSelecionado);
        }
    }
}