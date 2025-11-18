using Microsoft.AspNetCore.Mvc;
using BibliotecaDigital.Application.Interfaces;
using BibliotecaDigital.Application.ViewModels;

namespace BibliotecaDigital.Web.Controllers
{
    public class AutoresController : Controller
    {
        private readonly IAutorService _autorService;

        public AutoresController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        public async Task<IActionResult> Index()
        {
            var autores = await _autorService.GetAllAsync();
            return View(autores);
        }

        public async Task<IActionResult> Details(int id)
        {
            var autor = await _autorService.GetByIdAsync(id);
            if (autor == null)
                return NotFound();

            return View(autor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AutorViewModel autorViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var autorExistenteEmail = await _autorService.GetByEmailAsync(autorViewModel.Email);
                    if (autorExistenteEmail != null)
                    {
                        TempData["ErrorMessage"] = $"⚠️ Já existe um autor cadastrado com o email '{autorViewModel.Email}'.";
                        ModelState.AddModelError("Email", "Este email já está cadastrado no sistema.");
                        return View(autorViewModel);
                    }


                    var autorExistenteNome = await _autorService.GetByNomeAsync(autorViewModel.Nome);
                    if (autorExistenteNome != null)
                    {
                        TempData["ErrorMessage"] = $"⚠️ Já existe um autor cadastrado com o nome '{autorViewModel.Nome}'.";
                        ModelState.AddModelError("Nome", "Este nome já está cadastrado no sistema.");
                        return View(autorViewModel);
                    }

                    await _autorService.AddAsync(autorViewModel);
                    TempData["SuccessMessage"] = "✅ Autor cadastrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"❌ Erro ao cadastrar autor: {ex.Message}";
                }
            }

            return View(autorViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var autor = await _autorService.GetByIdAsync(id);
            if (autor == null)
                return NotFound();

            return View(autor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AutorViewModel autorViewModel)
        {
            if (id != autorViewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {

                    var autorExistenteEmail = await _autorService.GetByEmailAsync(autorViewModel.Email);
                    if (autorExistenteEmail != null && autorExistenteEmail.Id != id)
                    {
                        TempData["ErrorMessage"] = $"⚠️ Já existe outro autor cadastrado com o email '{autorViewModel.Email}'.";
                        ModelState.AddModelError("Email", "Este email já está cadastrado em outro autor.");
                        return View(autorViewModel);
                    }

   
                    var autorExistenteNome = await _autorService.GetByNomeAsync(autorViewModel.Nome);
                    if (autorExistenteNome != null && autorExistenteNome.Id != id)
                    {
                        TempData["ErrorMessage"] = $"⚠️ Já existe outro autor cadastrado com o nome '{autorViewModel.Nome}'.";
                        ModelState.AddModelError("Nome", "Este nome já está cadastrado em outro autor.");
                        return View(autorViewModel);
                    }

                    await _autorService.UpdateAsync(autorViewModel);
                    TempData["SuccessMessage"] = "✅ Autor atualizado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"❌ Erro ao atualizar autor: {ex.Message}";
                }
            }

            return View(autorViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var autor = await _autorService.GetByIdAsync(id);
            if (autor == null)
                return NotFound();

            return View(autor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _autorService.DeleteAsync(id);
                TempData["SuccessMessage"] = "✅ Autor excluído com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"❌ Erro ao excluir autor: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm)
        {
            IEnumerable<AutorViewModel> autores;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                autores = await _autorService.GetAllAsync();
            }
            else
            {
                autores = await _autorService.SearchAsync(searchTerm);
            }

            return PartialView("_AutoresListPartial", autores);
        }
    }
}