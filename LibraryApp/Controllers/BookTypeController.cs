using LibraryApp.Models;
using LibraryApp.Utility;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    public class BookTypeController : Controller
    {

        private readonly AppDbContext _context;

        public BookTypeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<BookType> bookTypes = _context.BookType.ToList();
            return View(bookTypes);
        }
    }
}
