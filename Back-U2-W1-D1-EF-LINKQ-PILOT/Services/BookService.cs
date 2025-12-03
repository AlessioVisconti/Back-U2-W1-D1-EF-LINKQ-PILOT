using Back_U2_W1_D1_EF_LINKQ_PILOT.Models.Entity;
using Microsoft.EntityFrameworkCore;

public class BookService
{
    private readonly ApplicationDbContext _context;

    public BookService(ApplicationDbContext context)
    {
        this._context = context;
    }

    // GET ALL libri con i generi
    public async Task<List<Books>> GetAllAsync()
    {
        return await this._context.Books
            .Include(b => b.Genres)
            .ToListAsync();
    }

    // GET libro per Id
    public async Task<Books?> GetAsync(Guid id)
    {
        return await this._context.Books
            .Include(b => b.Genres)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    // CREATE libro
    public async Task<bool> CreateAsync(Books book)
    {
        await this._context.Books.AddAsync(book);
        return await SaveAsync();
    }

    // UPDATE libro
    public async Task<bool> UpdateAsync(Books book)
    {
        this._context.Books.Update(book);
        return await SaveAsync();
    }

    // DELETE libro
    public bool Delete(Guid id)
    {
        var book = _context.Books.Find(id);
        if (book == null) return false;

        _context.Books.Remove(book);
        return _context.SaveChanges() > 0;
    }



    public async Task<bool> AddGenreToBookAsync(Guid bookId, Guid genreId)
    {
        var book = await _context.Books
            .Include(b => b.Genres)
            .FirstOrDefaultAsync(b => b.Id == bookId);

        var genre = await _context.Genres.FindAsync(genreId);

        if (book == null || genre == null) return false;

        book.Genres.Add(genre);
        return await SaveAsync();
    }

    // REMOVE GENRE da un libro
    public bool RemoveGenreFromBook(Guid bookId, Guid genreId)
    {
        var book = _context.Books
            .Include(b => b.Genres)
            .FirstOrDefault(b => b.Id == bookId);

        var genre = _context.Genres.Find(genreId);

        if (book == null || genre == null) return false;

        book.Genres.Remove(genre); // sincrono, in memoria
        return _context.SaveChanges() > 0; // salva sul DB
    }
    
    
    // Metodo helper per salvare i cambiamenti
    private async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}

//public bool Create(Books book)
//{
//    // insert product into database
//    this._context.Books.Add(book);
//    // save changes
//    if (this._context.SaveChanges() > 0)
//    {
//        return true;
//    }
//    return false;
//}
///*SE NON SI VUOLE SCIRVERE TUTTO IL SaveChanges si può fare una mini funzione per richiamarla ed accorciare ed è la seguente Save*/
//public bool Save()
//{
//    if (this._context.SaveChanges() > 0)
//    {
//        return true;
//    }
//    return false;
//}