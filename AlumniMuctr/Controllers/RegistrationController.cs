using AlumniMuctr.Data;
using AlumniMuctr.Models;
using AlumniMuctr.Services.Excel;
using AlumniMuctr.Services.SaveFileService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlumniMuctr.Controllers
{
    [Authorize]
    public class RegistrationController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly ISaveFileService _saveFile;

        public RegistrationController(ApplicationDbContext db, IWebHostEnvironment appEnvironment, ISaveFileService saveFile)
        {
            _db = db;
            _appEnvironment = appEnvironment;
            _saveFile = saveFile;
        }

        public IActionResult Index()
        {
            IEnumerable<RegistrationForm> dbList = _db.RegistrationForm;
            return View(dbList);
        }

        public ActionResult ExportDataAll()
        {
            ExcelWork excel = new ExcelWork();
            return excel.ExportData("Reg-1", _db);
        }

        public ActionResult ExportDataVeify()
        {
            ExcelWork excel = new ExcelWork();
            return excel.ExportData("Reg-2", _db);
        }

        public ActionResult ExportDataNotVerify()
        {
            ExcelWork excel = new ExcelWork();
            return excel.ExportData("Reg-3", _db);
        }

        //Get
        public IActionResult Create()
        {
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegistrationFormRequest obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Photo != null)
                {
                    string path = "/media/UserPictures/";
                    obj.PhotoUrl = await _saveFile.SaveFile(_appEnvironment.WebRootPath, path, obj.Photo);
                }

                _db.RegistrationForm.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Заявка успешно создана";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Get
        public IActionResult Edit(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }
            
            var objFromDb = _db.RegistrationForm.Find(id);
            if (objFromDb == null)
            {
                NotFound();
            }

            return View(new RegistrationFormRequest(objFromDb));
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RegistrationFormRequest obj)
        {
            if (ModelState.IsValid)
            { 
                if (obj.Photo != null)
                {
                    string path = "/media/UserPictures/";
                    obj.PhotoUrl = await _saveFile.SaveFile(_appEnvironment.WebRootPath, path, obj.Photo);
                }

                _db.RegistrationForm.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Заявка успешно изменена";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(Guid? id)
        {
            var obj = _db.RegistrationForm.Find(id);
            if (obj == null)
            {
                NotFound();
            }
            _db.RegistrationForm.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Заявка успешно удалена";
            return RedirectToAction("Index");
        }
    }
}
