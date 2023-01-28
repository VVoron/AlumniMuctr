using AlumniMuctr.Data;
using AlumniMuctr.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlumniMuctr.Controllers
{
    public class ProgrammsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _appEnvironment;

        public ProgrammsController(ApplicationDbContext db, IWebHostEnvironment appEnvironment)
        {
            _db = db;
            _appEnvironment = appEnvironment;
        }
        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<Programms> dbList = _db.Programms;
            return View(dbList);
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Programms obj)
        {
            if (!ModelState.IsValid)
                return View(obj);

            _db.Programms.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Программа успешно создана";
            return RedirectToAction("Index");
        }
        [Authorize]

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var objFromDb = _db.Programms.Find(id);
            if (objFromDb == null)
            {
                return NotFound();
            }

            return View(objFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Programms obj)
        {
            if (!ModelState.IsValid)
                return View(obj);

            _db.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Программа успешно изменена";
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult Delete(int? id)
        {
            var obj = _db.Programms.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Programms.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Программа успешно удалена";

            return RedirectToAction("Index");
        }
    }
}
