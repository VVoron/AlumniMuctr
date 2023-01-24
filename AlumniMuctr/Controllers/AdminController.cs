using AlumniMuctr.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AlumniMuctr.Controllers
{
    public class AdminController : Controller
    {
        List<User> peoples = new List<User>
        {
            new User {Name = "admin", Password = "admin", Role = Enums.Role.Admin},
        };
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
            return View();
        }

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        public ClaimsIdentity? Login(LoginViewModel model)
        {
            try
            {
                var user = peoples.FirstOrDefault(x => x.Name == model.Name);
                if (user == null)
                {
                    return null;
                }
                if (user.Password != model.Password)
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
