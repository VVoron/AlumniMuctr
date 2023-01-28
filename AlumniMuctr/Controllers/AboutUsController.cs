using Microsoft.AspNetCore.Mvc;

namespace AlumniMuctr.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
