using System.Linq.Expressions;
using LibraryApp.Utility;

namespace LibraryApp.Models;

public class BookRepository : Repository<Book>, IBookRepository
{
    private AppDbContext _context;
    public BookRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public void Edit(Book book)
    {
        _context.Books.Update(book);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}