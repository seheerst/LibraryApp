using LibraryApp.Models;
using LibraryApp.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(BookType type)
        {
            if (ModelState.IsValid)
            {
                _context.BookType.Add(type);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(type);
        }
        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id==null)
            {
                return NotFound();
            }

            BookType bookType = _context.BookType.Find(id);
            if (bookType == null)
            {
                return NotFound();
            }
            return View(bookType);
        }
        
        [HttpPost]
        public IActionResult Edit(int id, BookType type)
        {
            if (id!= type.TypeId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.Update(
                    new BookType()
                    {
                        TypeId = type.TypeId,
                        TypeName = type.TypeName
                    });
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]

        public IActionResult Delete(int id)
        {
            BookType bookType = _context.BookType.Find(id);
            return View(bookType);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookType type = _context.BookType.Find(id)!;
            _context.BookType.Remove(type);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
