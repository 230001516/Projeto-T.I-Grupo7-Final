using Microsoft.AspNetCore.Mvc;

namespace TI_Grupo7.Controllers
{
    public class SuporteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SupUtil(string mensagem) {

            ViewData["Mensagem"] = mensagem;

            return View();
        }
    }
}
