using AlumniMuctr.Data;
using AlumniMuctr.Models;
using AlumniMuctr.Services.HashService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AlumniMuctr.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersTableController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHashService _hash;

        public UsersTableController(ApplicationDbContext db, IHashService hash)
        {
            _db = db;
            _hash = hash;
        }

        public IActionResult Index()
        {
            IEnumerable<Models.User> Users = _db.User.ToList();
            return View(Users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewUserViewModel obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            if (obj.Password != obj.RepeatPassword)
            {
                TempData["error"] = "Пароли не совпадают!";
                return View(obj);
            }

            User newUser = new User();
            newUser.Name = obj.Name;
            newUser.Password = _hash.HashPassword(obj.Password);
            newUser.Role = Enums.Role.Moderator;

            _db.User.Add(newUser);
            _db.SaveChanges();
            TempData["success"] = "Пользователь успешно добавлен";

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var objFromDb = _db.User.Find(id);

            EditUserViewModel objForEdit = new EditUserViewModel();
            objForEdit.Id = objFromDb.Id;
            objForEdit.Name = objFromDb.Name;
            objForEdit.OldPassword = objFromDb.Password;

            if (objFromDb == null)
            {
                return NotFound();
            }

            return View(objForEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel obj)
        {
            var objFromDb = _db.User.Find(obj.Id);
            objFromDb.Password = _hash.HashPassword(obj.NewPassword);
            if (!ModelState.IsValid)
                return View(obj);

            _db.Update(objFromDb);
            _db.SaveChanges();
            TempData["success"] = "Пользователь успешно изменен";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            var obj = _db.User.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.User.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Пользователь успешно удален";

            return RedirectToAction("Index");
        }
    }
}
