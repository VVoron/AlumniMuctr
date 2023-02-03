using AlumniMuctr.Data;
using AlumniMuctr.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlumniMuctr.Controllers
{
    public class FunSaturdayTable : Controller
    {
        private readonly ApplicationDbContext _db;
        public FunSaturdayTable(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            FunSaturdayWithEvent model = new FunSaturdayWithEvent();
            model.FunSaturdayReg = _db.FunSaturdayReg.ToList();
            model.News = new List<News>();
            for (int i = 0; i < model.FunSaturdayReg.Count(); i++)
                model.News.Add(_db.News.Where(x => x.Id == model.FunSaturdayReg[i].NewsId).First());
            return View(model);
        }

        public IActionResult Delete(int? id)
        {
            var obj = _db.FunSaturdayReg.Find(id);

            if (obj == null)
            {
                NotFound();
            }

            _db.FunSaturdayReg.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Заявка успешно удалена";

            return RedirectToAction("Index");
        }
    }
}
