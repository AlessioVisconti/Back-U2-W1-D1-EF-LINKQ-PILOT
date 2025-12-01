using Back_U2_W1_D1_EF_LINKQ_PILOT.Models.Entity;

namespace Back_U2_W1_D1_EF_LINKQ_PILOT.Services
{
    public class BookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;

        }

        public Books Get(Guid Id)
        {
            return this._context.Books.FirstOrDefault(p => p.Id == Id);
        }

        public List<Books> GetAll()
        {
            // select all products from database
            return this._context.Books.ToList();
        }

        public bool Create(Books book)
        {
            // insert product into database
            this._context.Books.Add(book);
            // save changes
            if (this._context.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
