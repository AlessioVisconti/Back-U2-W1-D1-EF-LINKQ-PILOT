using Back_U2_W1_D1_EF_LINKQ_PILOT.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Back_U2_W1_D1_EF_LINKQ_PILOT.Services
{
    public class BorrowingService
    {
        private readonly ApplicationDbContext _context;

        public BorrowingService(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET ALL prestiti
        public async Task<List<Borrowing>> GetAllAsync()
        {
            return await _context.Borrows
                .Include(b => b.User)
                .Include(b => b.Book)
                .ToListAsync();
        }

        // GET prestito per Id
        public async Task<Borrowing?> GetAsync(Guid id)
        {
            return await _context.Borrows
                .Include(b => b.User)
                .Include(b => b.Book)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        // CREATE prestito
        public async Task<bool> CreateAsync(Borrowing borrow)
        {
            await _context.Borrows.AddAsync(borrow);
            return await SaveAsync();
        }

        // UPDATE prestito
        public async Task<bool> UpdateAsync(Borrowing borrow)
        {
            _context.Borrows.Update(borrow);
            return await SaveAsync();
        }

        // DELETE prestito (sincrono)
        public bool Delete(Guid id)
        {
            var borrow = _context.Borrows.Find(id);
            if (borrow == null) return false;

            _context.Borrows.Remove(borrow);
            return _context.SaveChanges() > 0;
        }

        // Helper per salvare in async
        private async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}