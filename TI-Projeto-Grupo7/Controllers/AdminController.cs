using Microsoft.AspNetCore.Mvc;

namespace TI_Grupo7.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult dashboard()
        {
            return View();
        }
        public IActionResult notifications()
        {
            return View();
        }
        public IActionResult table()
        {
            return View();
        }
        public IActionResult user()
        {
            return View();
        }
    }
}
