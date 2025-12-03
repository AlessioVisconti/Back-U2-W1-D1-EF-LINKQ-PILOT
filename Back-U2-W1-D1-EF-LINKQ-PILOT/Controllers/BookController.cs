using Back_U2_W1_D1_EF_LINKQ_PILOT.Models.Entity;
using Back_U2_W1_D1_EF_LINKQ_PILOT.Services;
using Back_U2_W1_D1_EF_LINKQ_PILOT.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Back_U2_W1_D1_EF_LINKQ_PILOT.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService _bookService;
        private readonly GenreService _genreService;

        public BookController(BookService bookService, GenreService genreService)
        {
            _bookService = bookService;
            _genreService = genreService;
        }


        // GET: lista dei libri per i clienti
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllAsync();
            return View(books);
        }

        //GET: lista dei libri per il backoffice
        public async Task<IActionResult> EditGrid()
        {
            var books = await _bookService.GetAllAsync();
            return View(books); // Tabella con pulsanti Modifica, Elimina, Crea
        }

        // GET: crea nuovo libro
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var genres = await _genreService.GetAllAsync();
            ViewBag.Genres = genres;
            return View();
        }

        // POST: crea nuovo libro
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Genres = await _genreService.GetAllAsync();
                return View(model);
            }

            var book = new Books
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Author = model.Author,
                Available = model.Available,
                Cover = model.Cover
            };

            await _bookService.CreateAsync(book);

            // Aggiungi generi selezionati
            if (model.SelectedGenreIds != null)
            {
                foreach (var genreId in model.SelectedGenreIds)
                {
                    await _bookService.AddGenreToBookAsync(book.Id, genreId);
                }
            }

            return RedirectToAction("Index");
        }

        // GET: modifica libro
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var book = await _bookService.GetAsync(id);
            if (book == null) return NotFound();

            var genres = await _genreService.GetAllAsync();

            var model = new EditBookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Available = book.Available,
                Cover = book.Cover,
                SelectedGenreIds = book.Genres.Select(g => g.Id).ToList(),
                AllGenres = genres
            };

            return View(model);
        }

        // POST: modifica libro
        [HttpPost]
        public async Task<IActionResult> Edit(EditBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AllGenres = await _genreService.GetAllAsync();
                return View(model);
            }

            var book = await _bookService.GetAsync(model.Id);
            if (book == null) return NotFound();

            book.Title = model.Title;
            book.Author = model.Author;
            book.Available = model.Available;
            book.Cover = model.Cover;

            await _bookService.UpdateAsync(book);

            // Aggiorna generi
            var currentGenreIds = book.Genres.Select(g => g.Id).ToList();

            // Rimuovi generi non selezionati
            foreach (var genreId in currentGenreIds.Except(model.SelectedGenreIds ?? new List<Guid>()))
            {
                _bookService.RemoveGenreFromBook(book.Id, genreId); // sincrono
            }

            // Aggiungi nuovi generi selezionati
            if (model.SelectedGenreIds != null)
            {
                foreach (var genreId in model.SelectedGenreIds.Except(currentGenreIds))
                {
                    await _bookService.AddGenreToBookAsync(book.Id, genreId);
                }
            }

            return RedirectToAction("Index");
        }

        // POST: elimina libro
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            _bookService.Delete(id); // se vuoi async, ma normalmente può restare sincrono
            return RedirectToAction("Index");
        }
    }
}