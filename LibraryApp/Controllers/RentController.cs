using LibraryApp.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Controllers
{
    public class RentController : Controller
    {

        private readonly IRentRepository _rentRepository;
        private readonly IBookRepository _bookRepository;

        public RentController( IRentRepository rentRepository, IBookRepository repository)
        {
            _rentRepository = rentRepository;
            _bookRepository = repository;
          
        }
        public async Task<IActionResult> Index()
        {
            List<Rent> rentList = _rentRepository.GetAll(includeProps: "Book").ToList();
           
            return View(rentList);
        }
        
        [HttpGet]
        public IActionResult CreateAndEdit(int id)
        {
            IEnumerable<SelectListItem> BookList = _bookRepository.GetAll().Select(k=> new SelectListItem
            {
                Text = k.BookName,
                Value = k.BookId.ToString()
            });
            ViewBag.BookList = BookList;

            if (id==null || id==0)
            {
                return View();
            }
            else
            {
                Rent rent = _rentRepository.Get(u=> u.RentId == id);
                if (rent == null)
                {
                    return NotFound();
                }
                return View(rent);
            }
            
        }
        
        [HttpPost]
        public IActionResult CreateAndEdit(Rent rent)
        {
            if (ModelState.IsValid)
            {
                if (rent.RentId == 0)
                {
                    _rentRepository.Add(rent);
                    TempData["successful"] = "Rent successfully created";
                }
                else
                {
                    _rentRepository.Edit(rent);
                    TempData["successful"] = "Rent successfully edited";
                }
                
                _rentRepository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]

        public IActionResult Delete(int id)
        {
            Rent rent = _rentRepository.Get(u=> u.RentId == id);
            IEnumerable<SelectListItem> BookList = _bookRepository.GetAll().Select(k=> new SelectListItem
            {
                Text = k.BookName,
                Value = k.BookId.ToString()
            });
            ViewBag.BookList = BookList;
            return View(rent);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rent rent = _rentRepository.Get(u=> u.RentId == id);

            _rentRepository.Delete(rent);
            _rentRepository.Save();
            TempData["successful"] = "Rent successfully deleted";
            return RedirectToAction("Index");
        }
    }
}
