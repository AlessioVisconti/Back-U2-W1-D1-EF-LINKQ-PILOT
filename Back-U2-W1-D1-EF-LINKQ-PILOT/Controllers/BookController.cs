using Back_U2_W1_D1_EF_LINKQ_PILOT.Models.Entity;
using Back_U2_W1_D1_EF_LINKQ_PILOT.Services;
using Back_U2_W1_D1_EF_LINKQ_PILOT.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Back_U2_W1_D1_EF_LINKQ_PILOT.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService _bookService;

        public BookController(
           BookService bookService
            
            )
        {
            _bookService = bookService; // DI
        }


        public IActionResult Index()
        {
           
            List<Books> bookDb = this._bookService.GetAll();

            List<BooksViewModel> bookViewModels = bookDb.Select(b => new BooksViewModel()
            {
                Id = b.Id,
                Title=b.Title,
                Author=b.Author,
                Genre=b.Genre,
                Available=b.Available,
                Cover=b.Cover
            }
            ).ToList();

            return View(bookViewModels);
            
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateViewModel createViewModel)
        {
            Books newBook = new Books()
            {
                Id = Guid.NewGuid(),
                Title = createViewModel.Title,
                Author = createViewModel.Author,
                Genre = createViewModel.Genre,
                Available = createViewModel.Available,
                Cover = createViewModel.Cover
                
            };

            bool creationResult = _bookService.Create(newBook);
            if (!creationResult)
            {
                TempData["CreationError"] = "Errore durante la creazione del prodotto";
                return RedirectToAction("Create", "Book");
            }

            return RedirectToAction("Index", "Book");
        }
    }
}
