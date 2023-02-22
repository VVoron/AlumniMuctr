using AlumniMuctr.Data;
using AlumniMuctr.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AlumniMuctr.Services.Excel;

namespace AlumniMuctr.Controllers
{
    [Authorize]
    public class HelperController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HelperController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Helper> helpers = _db.Helper.ToList();
            return View(helpers);
        }

        public ActionResult ExportData()
        {
            ExcelWork excel = new ExcelWork();
            return excel.ExportData("Lectures", _db);
        }

        public IActionResult WatchMore(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var objFromDb = _db.Helper.Find(id);
            if (objFromDb == null)
            {
                return NotFound();
            }

            return View(objFromDb);
        }

        public IActionResult Delete(int? id)
        {
            var obj = _db.Helper.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Helper.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Вопрос успешно удален";

            return RedirectToAction("Index");
        }
    }
}
