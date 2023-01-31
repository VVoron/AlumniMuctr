using AlumniMuctr.Data;
using AlumniMuctr.Models;
using AlumniMuctr.Services.SaveFileService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AlumniMuctr.Controllers
{
    public class FullRegistrationController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly ISaveFileService _saveFile;

        public FullRegistrationController(ApplicationDbContext db, IWebHostEnvironment appEnvironment, ISaveFileService saveFileService)
        {
            _db = db;
            _appEnvironment = appEnvironment;
            _saveFile = saveFileService;
        }

        [Route("[controller]/{id}")]
        public IActionResult Index(Guid? id)
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[controller]")]
        public async Task<IActionResult> Register(RegistrationFormRequest obj)
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
                TempData["success"] = "Спасибо, что полностью заполнили анкету";
                return RedirectToAction("Index", "Home");
            }
            return View(obj);
        }
    }
}
