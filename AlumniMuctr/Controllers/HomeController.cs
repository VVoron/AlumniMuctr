using AlumniMuctr.Data;
using AlumniMuctr.Models;
using AlumniMuctr.Services.Support;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AlumniMuctr.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _appEnvironment;

        public HomeController(ApplicationDbContext db, IWebHostEnvironment appEnvironment)
        {
            _db = db;
            _appEnvironment = appEnvironment;
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
            var entity = new RegistrationForm(obj);
            if (obj.Photo != null)
            {
                var newDirName = Guid.NewGuid();

                string path = _appEnvironment.WebRootPath + "/media/UserPictures/" + newDirName;

                Directory.CreateDirectory(path);

                using (var fileStream = new FileStream(path + "/" + obj.Photo.FileName, FileMode.Create))
                {
                    await obj.Photo.CopyToAsync(fileStream);
                }

                entity.Photo = "/media/UserPictures/" + newDirName + "/" + obj.Photo.FileName;
            }


            _db.RegistrationForm.Add(entity);
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