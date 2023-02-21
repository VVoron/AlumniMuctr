using AlumniMuctr.Data;
using AlumniMuctr.Models;
using AlumniMuctr.Services.SaveFileService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlumniMuctr.Controllers
{
    [Authorize]
    public class ProgrammsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly ISaveFileService _saveFile;

        public ProgrammsController(ApplicationDbContext db, IWebHostEnvironment appEnvironment, ISaveFileService saveFile)
        {
            _db = db;
            _appEnvironment = appEnvironment;
            _saveFile = saveFile;
        }

        public IActionResult Index()
        {
            IEnumerable<Programms> dbList = _db.Programms;
            return View(dbList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProgrammsRequest obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Photo != null)
                {
                    string path = "/media/Programs/";
                    obj.PhotoUrl = await _saveFile.SaveFile(_appEnvironment.WebRootPath, path, obj.Photo);
                }

                _db.Programms.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Программа успешно создана";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var objFromDb = _db.Programms.Find(id);
            if (objFromDb == null)
            {
                return NotFound();
            }

            return View(new ProgrammsRequest(objFromDb));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProgrammsRequest obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Photo != null)
                {
                    string path = "/media/Programs/";
                    obj.PhotoUrl = await _saveFile.SaveFile(_appEnvironment.WebRootPath, path, obj.Photo);
                }

                _db.Programms.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Программа успешно изменена";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            var obj = _db.Programms.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Programms.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Программа успешно удалена";

            return RedirectToAction("Index");
        }
    }
}
