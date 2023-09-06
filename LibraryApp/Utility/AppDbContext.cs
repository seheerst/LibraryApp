using LibraryApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Utility
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<BookType> BookType { get; set;}

        public DbSet<Book> Books { get; set; }

        public DbSet<Rent> Rents { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }

    }
}