using BibliotecaDigital.Application.Interfaces;
using BibliotecaDigital.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Create(AutorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _autorService.AddAsync(viewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
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
        public async Task<IActionResult> Edit(int id, AutorViewModel viewModel)
        {
            if (id != viewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _autorService.UpdateAsync(viewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
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
            await _autorService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        
        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                var allAutores = await _autorService.GetAllAsync();
                return PartialView("_AutoresListPartial", allAutores);
            }

            var autores = await _autorService.SearchAsync(searchTerm);
            return PartialView("_AutoresListPartial", autores);
        }
    }
}