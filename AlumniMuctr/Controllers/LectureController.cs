using AlumniMuctr.Data;
using AlumniMuctr.Models;
using AlumniMuctr.Services.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlumniMuctr.Controllers
{
    [Authorize]
    public class LectureController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LectureController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var helpers = _db.Lectures.ToList();
            return View(helpers);
        }

        public IActionResult Create()
        {
            var entity = new Lecture();

            return View(entity);
        }

        [HttpPost]
        public IActionResult Create(Lecture lecture)
        {
            if (!ModelState.IsValid)
                return View(lecture);

            _db.Lectures.Add(lecture);
            _db.SaveChanges();

            TempData["Success"] = "Лекция успешно добавлена";

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();

            var entity = _db.Lectures.Find(id);

            if (entity == null)
                return NotFound();

            return View(entity);
        }

        [HttpPost]
        public IActionResult Edit(Lecture lecture)
        {
            if (!ModelState.IsValid)
                return View(lecture);

            _db.Lectures.Update(lecture);
            _db.SaveChanges();

            TempData["Success"] = "Лекция была успешно изменена!";

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            var entity = _db.Lectures.Find(id);

            if (entity == null)
                return NotFound();

            _db.Lectures.Remove(entity);
            _db.SaveChanges();

            TempData["Success"] = "Лекция была успешно удалена!";

            return RedirectToAction("Index");
        }
    }
}
