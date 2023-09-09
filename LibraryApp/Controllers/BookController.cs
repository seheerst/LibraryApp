using LibraryApp.Models;
using LibraryApp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryApp.Controllers
{

    public class BookController : Controller
    {

        private readonly IBookRepository _bookRepository;
        private readonly IBookTypeRepository _bookTypeRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(IBookRepository repository, IBookTypeRepository bookTypeRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = repository;
            _bookTypeRepository = bookTypeRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Index(string type)
        {
            var booksByTag = _bookRepository.GetAll(includeProps: "BookType");
            
            if (!string.IsNullOrEmpty(type))
            {
                booksByTag = booksByTag.Where(x => x.BookType.TypeName == type);
                return View(booksByTag.ToList());
            }
            else
            {
                return View(booksByTag.ToList());
            }
       
        }
        [Authorize(Roles = UserRole.Role_Admin)]
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
        [Authorize(Roles = UserRole.Role_Admin)]
        [HttpPost]
        public IActionResult CreateAndEdit(Book book, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string bookPath = Path.Combine(wwwRootPath, @"img");
                
                if (file != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(bookPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                      
                    book.ImageUrl = @"\img\" + file.FileName;
                }
              

                if (book.BookId == 0)
                {
                    _bookRepository.Add(book);
                    TempData["successful"] = "Book successfully created";
                }
                else
                {
                    _bookRepository.Edit(book);
                    TempData["successful"] = "Book successfully edited";
                }
                
                _bookRepository.Save();
                return RedirectToAction("Index");
            }
            return View(book);
        }
        [HttpGet]
        [Authorize(Roles = UserRole.Role_Admin)]
        public IActionResult Delete(int id)
        {
            Book book = _bookRepository.Get(u=> u.BookId == id);
            return View(book);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRole.Role_Admin)]
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
