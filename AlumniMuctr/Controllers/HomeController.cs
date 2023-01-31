using AlumniMuctr.Data;
using AlumniMuctr.Models;
using AlumniMuctr.Services.SaveFileService;
using AlumniMuctr.Services.Support;
using DocumentFormat.OpenXml.Drawing;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AlumniMuctr.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly ISaveFileService _saveFile;

        public HomeController(ApplicationDbContext db, IWebHostEnvironment appEnvironment, ISaveFileService saveFile)
        {
            _db = db;
            _appEnvironment = appEnvironment;
            _saveFile = saveFile;
        }

        public IActionResult Index()
        {
            AllModels model = new AllModels();
            model.AllNews = _db.News.ToList();
            model.AllProgramms = _db.Programms.ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationFormRequest obj)
        {    
            _db.RegistrationForm.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> HelperRequest(Helper obj)
        {
            Support support = new Support();
            support.AddedNewQuestion(obj, _db);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}