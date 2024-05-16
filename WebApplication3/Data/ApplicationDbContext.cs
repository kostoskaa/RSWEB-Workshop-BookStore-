using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Areas.Identity.Data;


namespace WebApplication3.Data
{
    public class ApplicationDbContext : IdentityDbContext<WebApplication3User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WebApplication3.Models.Book> Book { get; set; } = default!;
        public DbSet<WebApplication3.Models.Author> Author { get; set; } = default!;
        public DbSet<WebApplication3.Models.BookGenre> BookGenre { get; set; } = default!;
        public DbSet<WebApplication3.Models.Genre> Genre { get; set; } = default!;
        public DbSet<WebApplication3.Models.Review> Review { get; set; } = default!;
        public DbSet<WebApplication3.Models.UserBooks> UserBooks { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        
    }
}
