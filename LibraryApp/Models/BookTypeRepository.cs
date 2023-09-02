using System.Linq.Expressions;
using LibraryApp.Utility;

namespace LibraryApp.Models;

public class BookTypeRepository : Repository<BookType>, IBookTypeRepository
{
    private AppDbContext _context;
    public BookTypeRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public void Edit(BookType type)
    {
        _context.BookType.Update(type);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}