using AlumniMuctr.Data;
using AlumniMuctr.Models;
using AlumniMuctr.Services.Excel;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlumniMuctr.Controllers
{
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _appEnvironment;

        public NewsController(ApplicationDbContext db, IWebHostEnvironment appEnvironment)
        {
            _db = db;
            _appEnvironment = appEnvironment;
        }

        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<News> dbList = _db.News.Include(c=>c.Category).ToList();
            return View(dbList);
        } 

        [Authorize]
        public ActionResult ExportData()
        {
            ExcelWork excel = new ExcelWork();
            return excel.ExportData("News", _db);
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);


            var entity = new News(request);

            entity.Photo = String.Empty;
            
            if (request.Photo != null)
            {
                var newDirName = Guid.NewGuid();

                string path = _appEnvironment.WebRootPath + "/media/NewsMedia/" + newDirName;

                Directory.CreateDirectory(path);

                using (var fileStream = new FileStream(path + "/" + request.Photo.FileName, FileMode.Create))
                {
                    await request.Photo.CopyToAsync(fileStream);
                }

                entity.Photo = "/media/NewsMedia/" + newDirName + "/" + request.Photo.FileName;
            } else if (request.PhotoUrl != null)
            {
                entity.Photo = request.PhotoUrl;
            }

            _db.News.Add(entity);
            _db.SaveChanges();
            TempData["success"] = "Новость успешно создана";
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var objFromDb = _db.News.Find(id);
            if (objFromDb == null)
            {
                return NotFound();
            }

            var entity = new NewsRequest(objFromDb);


            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NewsRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);



            var entity = _db.News.Find(request.Id);

            entity.Title = request.Title;
            entity.BriefDescription = request.BriefDescription;
            entity.Description = request.Description;

            if (request.Photo != null)
            {
                var newDirName = Guid.NewGuid();

                string path = _appEnvironment.WebRootPath + "/media/NewsMedia/" + newDirName;

                Directory.CreateDirectory(path);

                using (var fileStream = new FileStream(path + "/" + request.Photo.FileName, FileMode.Create))
                {
                    await request.Photo.CopyToAsync(fileStream);
                }

                entity.Photo = "/media/NewsMedia/" + newDirName + "/" + request.Photo.FileName;
            }
            else if (request.PhotoUrl != null)
            {
                entity.Photo = request.PhotoUrl;
            }

            _db.SaveChanges();
            TempData["success"] = "Новость успешно изменена";
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult Delete(int? id)
        {
            var obj = _db.News.Find(id);

            if (obj == null)
            {
                NotFound();
            }

            _db.News.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Новость успешно удалена";

            return RedirectToAction("Index");
        }

        public IActionResult GetAllNews()
        {
            return View();
        }
    }
}
