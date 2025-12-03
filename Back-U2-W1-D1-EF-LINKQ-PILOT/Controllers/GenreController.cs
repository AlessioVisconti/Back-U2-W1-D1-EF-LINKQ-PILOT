using Back_U2_W1_D1_EF_LINKQ_PILOT.Models.Entity;
using Back_U2_W1_D1_EF_LINKQ_PILOT.Services;
using Back_U2_W1_D1_EF_LINKQ_PILOT.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Back_U2_W1_D1_EF_LINKQ_PILOT.Controllers
{
    public class GenreController : Controller
    {
        private readonly GenreService _genreService;

        public GenreController(GenreService genreService)
        {
            _genreService = genreService;
        }

        // GET: lista dei generi
        public async Task<IActionResult> Index()
        {
            var genres = await _genreService.GetAllAsync();
            return View(genres);
        }

        // GET: crea nuovo genere
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: crea nuovo genere
        [HttpPost]
        public async Task<IActionResult> Create(GenreViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var genre = new Genre
            {
                Id = model.Id,
                Name = model.Name
            };

            await _genreService.CreateAsync(genre);
            return RedirectToAction("Index");
        }

        // GET: modifica genere
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var genre = await _genreService.GetAsync(id);
            if (genre == null) return NotFound();

            var model = new GenreViewModel
            {
                Id = genre.Id,
                Name = genre.Name
            };

            return View(model);
        }

        // POST: modifica genere
        [HttpPost]
        public async Task<IActionResult> Edit(GenreViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var genre = await _genreService.GetAsync(model.Id);
            if (genre == null) return NotFound();

            genre.Name = model.Name;
            await _genreService.UpdateAsync(genre);

            return RedirectToAction("Index");
        }

        // POST: elimina genere (rimane sincrono)
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            _genreService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}