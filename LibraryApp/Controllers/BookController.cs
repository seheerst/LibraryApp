using LibraryApp.Models;
using LibraryApp.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Controllers
{
    public class BookController : Controller
    {

        private readonly IBookRepository _bookRepository;
        private readonly IBookTypeRepository _bookTypeRepository;


        public BookController(IBookRepository repository, IBookTypeRepository bookTypeRepository)
        {
            _bookRepository = repository;
            _bookTypeRepository = bookTypeRepository;
        }
        public async Task<IActionResult> Index()
        {
            List<Book> books = _bookRepository.GetAll().ToList();
           
            return View(books);
        }
        
        [HttpGet]
        public IActionResult CreateAndEdit(int id)
        {
            IEnumerable<SelectListItem> BookTypeList = _bookTypeRepository.GetAll().Select(k=> new SelectListItem
            {
                Text = k.TypeName,
                Value = k.TypeId.ToString()
            });
            ViewBag.Types = BookTypeList;

            if (id==null || id==0)
            {
                return View();
            }
            else
            {
                Book book = _bookRepository.Get(u=> u.BookId == id);
                if (book == null)
                {
                    return NotFound();
                }
                return View(book);
            }
            
        }
        
        [HttpPost]
        public IActionResult CreateAndEdit(Book book, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                _bookRepository.Add(book);
                _bookRepository.Save();
                TempData["successful"] = "Book successfully created";
                return RedirectToAction("Index");
            }
            return View(book);
        }
        
        // [HttpGet]
        // public IActionResult Edit(int id)
        // {
        //     if (id==null)
        //     {
        //         return NotFound();
        //     }
        //
        //     Book book = _bookRepository.Get(u=> u.BookId == id);
        //     if (book == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(book);
        // }
        //
        // [HttpPost]
        // public IActionResult Edit(int id, Book book)
        // {
        //     if (id!= book.BookId)
        //     {
        //         return NotFound();
        //     }
        //     if (ModelState.IsValid)
        //     {
        //         _bookRepository.Edit(
        //             new Book()
        //             {
        //                BookId = book.BookId,
        //                BookName = book.BookName,
        //                Author = book.Author,
        //                Description = book.Description,
        //                Price = book.Price
        //             });
        //         _bookRepository.Save();
        //         TempData["successful"] = "Book successfully edited";
        //         return RedirectToAction("Index");
        //     }
        //     return View();
        // }

        [HttpGet]

        public IActionResult Delete(int id)
        {
            Book book = _bookRepository.Get(u=> u.BookId == id);
            return View(book);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = _bookRepository.Get(u=> u.BookId == id);

            _bookRepository.Delete(book);
            _bookRepository.Save();
            TempData["successful"] = "Book successfully deleted";
            return RedirectToAction("Index");
        }
    }
}
