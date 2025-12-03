using Back_U2_W1_D1_EF_LINKQ_PILOT.Models.Entity;
using Microsoft.EntityFrameworkCore;

public class UserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET ALL utenti
    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    // GET utente per Id
    public async Task<User?> GetAsync(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    // CREATE utente
    public async Task<bool> CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
        return await SaveAsync();
    }

    // UPDATE utente
    public async Task<bool> UpdateAsync(User user)
    {
        _context.Users.Update(user);
        return await SaveAsync();
    }

    // DELETE utente (sincrono)
    public bool Delete(Guid id)
    {
        var user = _context.Users.Find(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        return _context.SaveChanges() > 0;
    }

    // Helper per salvare in async
    private async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}