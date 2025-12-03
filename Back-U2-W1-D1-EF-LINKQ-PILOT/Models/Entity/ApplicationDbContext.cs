using Microsoft.EntityFrameworkCore;

namespace Back_U2_W1_D1_EF_LINKQ_PILOT.Models.Entity
{
   
        // Database context class for Entity Framework Core
        public class ApplicationDbContext : DbContext
        {

            // initialize db context with connection options (Dependency Injection)
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            { // pass options to base DbContext class
            }

            // DbSet representing the Products table in the database
            public DbSet<Books> Books { get; set; }
            public DbSet<User> Users { get; set; } 

        
            public DbSet<Genre> Genres { get; set; } 

            public DbSet<Borrowing> Borrows { get; set; } 

    }
    
}
