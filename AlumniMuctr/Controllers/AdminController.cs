using AlumniMuctr.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AlumniMuctr.Data;
using AlumniMuctr.Services.HashService;

namespace AlumniMuctr.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHashService _hash;
        public AdminController(ApplicationDbContext db, IHashService hash)
        {
            _db = db;
            _hash = hash;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Index(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var response = Login(user);
                if (response != null)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response));
                    TempData["success"] = "Добро пожаловать, " + user.Name;
                    return RedirectToAction("Index");
                }
            }
            TempData["error"] = "Неверный логин/пароль";
            return View();
        }

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, (user.Role == Enums.Role.Admin) ? "admin" : "moderator")
            };
            return new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        public ClaimsIdentity? Login(LoginViewModel model)
        {
            try
            {
                var user = _db.User.FirstOrDefault(x => x.Name == model.Name);
                if (user == null)
                {
                    return null;
                }

                if (!_hash.VerifyHashedPassword(user.Password, model.Password))
                    return null;

                var result = Authenticate(user);
                return result;
            }
            catch
            {
                return null;
            }
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["warning"] = "Вы вышли из аккаунта";
            return RedirectToAction("Index", "Home");
        }
    }
}
