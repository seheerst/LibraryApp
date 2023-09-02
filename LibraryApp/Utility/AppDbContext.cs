using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Utility
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<BookType> BookType { get; set;}

        public DbSet<Book> Books { get; set; }


    }
}

