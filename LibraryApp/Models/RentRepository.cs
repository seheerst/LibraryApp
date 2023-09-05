using System.Linq.Expressions;
using LibraryApp.Utility;

namespace LibraryApp.Models;

public class RentRepository : Repository<Book>, IRentRepository
{
    private AppDbContext _context;
    public RentRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public void Edit(Rent rent)
    {
        _context.Rents.Update(rent);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}