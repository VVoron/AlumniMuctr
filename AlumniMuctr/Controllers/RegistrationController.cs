using AlumniMuctr.Data;
using AlumniMuctr.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlumniMuctr.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ApplicationDbContext _db;

        public RegistrationController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<RegistrationForm> dbList = _db.RegistrationForm;
            return View(dbList);
        }

        //Get
        public IActionResult Create()
        {
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RegistrationForm obj)
        {
            if (ModelState.IsValid)
            {
                _db.RegistrationForm.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Заявка успешно создана";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var objFromDb = _db.RegistrationForm.Find(id);
            if (objFromDb == null)
            {
                NotFound();
            }
            return View(objFromDb);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RegistrationForm obj)
        {
            if (ModelState.IsValid)
            {
                _db.RegistrationForm.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Заявка успешно изменена";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            var obj = _db.RegistrationForm.Find(id);
            if (obj == null)
            {
                NotFound();
            }
            _db.RegistrationForm.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Заявка успешно удалена";
            return RedirectToAction("Index");
        }
    }
}
