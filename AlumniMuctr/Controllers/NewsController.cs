using AlumniMuctr.Data;
using AlumniMuctr.Models;
using AlumniMuctr.Services.Excel;
using AlumniMuctr.Services.SaveFileService;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlumniMuctr.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly ISaveFileService _saveFile;

        public NewsController(ApplicationDbContext db, IWebHostEnvironment appEnvironment, ISaveFileService saveFile)
        {
            _db = db;
            _appEnvironment = appEnvironment;
            _saveFile = saveFile;
        }

        public IActionResult Index()
        {
            IEnumerable<News> dbList = _db.News.Include(c=>c.Category).ToList();
            return View(dbList);
        } 

        public ActionResult ExportData()
        {
            ExcelWork excel = new ExcelWork();
            return excel.ExportData("News", _db);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsEdit request)
        {
            if (!ModelState.IsValid)
                return View(request);


            var entity = new News(request.News);

            entity.Photo = String.Empty;
            
            if (request.News.Photo != null)
            {
                entity.Photo = await _saveFile.SaveFile(_appEnvironment.WebRootPath, "/media/NewsMedia/", request.News.Photo);
            } 
            else if (request.News.PhotoUrl != null)
            {
                entity.Photo = request.News.PhotoUrl;
            }

            _db.News.Add(entity);
            _db.SaveChanges();
            TempData["success"] = "Новость успешно создана";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var objFromDb = _db.News.Include(c => c.Category).First(x => x.Id == id);
            var categs = _db.Categories.ToList();
            if (objFromDb == null)
            {
                return NotFound();
            }

            var entity = new NewsEdit(new NewsRequest(objFromDb), categs);


            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NewsRequest request)
        {
            var entity = _db.News.Find(request.Id);

            if (entity == null)
                return View(request);

            entity.Title = request.Title;
            entity.BriefDescription = request.BriefDescription;
            entity.Description = request.Description;
            entity.CategoryId = request.CategoryId;

            if (request.Photo != null)
            {
                entity.Photo = await _saveFile.SaveFile(_appEnvironment.WebRootPath, "/media/NewsMedia/", request.Photo);
            }
            else if (request.PhotoUrl != null)
            {
                entity.Photo = request.PhotoUrl;
            }

            _db.SaveChanges();
            TempData["success"] = "Новость успешно изменена";
            return RedirectToAction("Index");
        }

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
