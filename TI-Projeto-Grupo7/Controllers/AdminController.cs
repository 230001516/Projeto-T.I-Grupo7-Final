using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using TI_Projeto_Grupo7.Helpers;
using TI_Projeto_Grupo7.Services;

namespace TI_Projeto_Grupo7.Controllers
{
    [Authorize(Roles = "Admin")]
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
