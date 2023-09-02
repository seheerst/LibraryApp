using LibraryApp.Models;
using LibraryApp.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Controllers
{
    public class BookController : Controller
    {

        private readonly IBookRepository _repository;

        public BookController(IBookRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            List<Book> books = _repository.GetAll().ToList();
            return View(books);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(book);
                _repository.Save();
                TempData["successful"] = "Book successfully created";
                return RedirectToAction("Index");
            }
            return View(book);
        }
        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id==null)
            {
                return NotFound();
            }

            Book book = _repository.Get(u=> u.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        
        [HttpPost]
        public IActionResult Edit(int id, Book book)
        {
            if (id!= book.BookId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _repository.Edit(
                    new Book()
                    {
                       BookId = book.BookId,
                       BookName = book.BookName,
                       Author = book.Author,
                       Description = book.Description,
                       Price = book.Price
                    });
                _repository.Save();
                TempData["successful"] = "Book successfully edited";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]

        public IActionResult Delete(int id)
        {
            Book book = _repository.Get(u=> u.BookId == id);
            return View(book);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = _repository.Get(u=> u.BookId == id);

            _repository.Delete(book);
            _repository.Save();
            TempData["successful"] = "Book successfully deleted";
            return RedirectToAction("Index");
        }
    }
}
