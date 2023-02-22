using AlumniMuctr.Data;
using AlumniMuctr.Models;
using Microsoft.AspNetCore.Mvc;
using AlumniMuctr.Services.Support;

namespace AlumniMuctr.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AboutUsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            Helper obj = new Helper();
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> HelperRequest(Helper obj)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            Support support = new Support();
            support.AddedNewQuestion(obj, _db);
            return RedirectToAction("Index");
        }
    }
}
