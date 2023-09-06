using LibraryApp.Models;
using LibraryApp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    [Authorize(Roles = UserRole.Role_Admin)]
    public class BookTypeController : Controller
    {

        private readonly IBookTypeRepository _repository;

        public BookTypeController(IBookTypeRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            List<BookType> bookTypes = _repository.GetAll().ToList();
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
                _repository.Add(type);
                _repository.Save();
                TempData["successful"] = "Type successfully created";
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

            BookType bookType = _repository.Get(u=> u.TypeId == id);
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
                _repository.Edit(
                    new BookType()
                    {
                        TypeId = type.TypeId,
                        TypeName = type.TypeName
                    });
                _repository.Save();
                TempData["successful"] = "Type successfully edited";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]

        public IActionResult Delete(int id)
        {
            BookType bookType = _repository.Get(u=> u.TypeId == id);
            return View(bookType);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookType bookType = _repository.Get(u=> u.TypeId == id);

            _repository.Delete(bookType);
            _repository.Save();
            TempData["successful"] = "Type successfully deleted";
            return RedirectToAction("Index");
        }
    }
}
