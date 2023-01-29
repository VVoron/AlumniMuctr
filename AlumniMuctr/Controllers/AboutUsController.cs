using AlumniMuctr.Data;
using AlumniMuctr.Models;
using Microsoft.AspNetCore.Mvc;

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
            _db.Helper.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
