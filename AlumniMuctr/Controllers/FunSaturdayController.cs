using AlumniMuctr.Data;
using AlumniMuctr.Models;
using AlumniMuctr.Services.Support;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlumniMuctr.Controllers
{
    public class FunSaturdayController : Controller
    {
        private readonly ApplicationDbContext _db;
        public FunSaturdayController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<News> news = (from News in _db.News
                                       where News.CategoryId == 3
                                      select News).ToList();
            FunSatPage thisModel = new FunSatPage();
            thisModel.News = news;
            thisModel.Helper = new Helper();
            thisModel.FunSaturdayReg = new FunSaturdayReg();

            return View(thisModel);
        }

        [HttpPost]
        public async Task<IActionResult> HelperRequest(Helper obj)
        {
            Support support = new Support();
            support.AddedNewQuestion(obj, _db);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Registration(FunSaturdayReg obj)
        {
            _db.FunSaturdayReg.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Вы успешно зарегистрировались на событие";
            return RedirectToAction("Index");
        }
    }
}
