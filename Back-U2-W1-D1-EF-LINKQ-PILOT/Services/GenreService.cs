using Microsoft.EntityFrameworkCore;
using Back_U2_W1_D1_EF_LINKQ_PILOT.Models.Entity;

public class GenreService
{
    private readonly ApplicationDbContext _context;

    public GenreService(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET ALL generi
    public async Task<List<Genre>> GetAllAsync()
    {
        return await _context.Genres.ToListAsync();
    }

    // GET genere per Id
    public async Task<Genre?> GetAsync(Guid id)
    {
        return await _context.Genres.FindAsync(id);
    }

    // CREATE genere
    public async Task<bool> CreateAsync(Genre genre)
    {
        await _context.Genres.AddAsync(genre);
        return await SaveAsync();
    }

    // UPDATE genere
    public async Task<bool> UpdateAsync(Genre genre)
    {
        _context.Genres.Update(genre);
        return await SaveAsync();
    }

    // DELETE genere (sincrono)
    public bool Delete(Guid id)
    {
        var genre = _context.Genres.Find(id);
        if (genre == null) return false;

        _context.Genres.Remove(genre);
        return _context.SaveChanges() > 0;
    }

    // Helper per salvare in async
    private async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}