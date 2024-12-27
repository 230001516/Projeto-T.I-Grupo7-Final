using Microsoft.AspNetCore.Mvc;

namespace TI_Projeto_Grupo7.Controllers
{
    public class TransfersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Transfer(int transferencia)
        {
            ViewData["Transferências"] = transferencia;

            return View();
        }

        public IActionResult TransfersHistory(int hisTrans)
        {
            ViewData["Histórico Transferências"] = hisTrans;

            return View();
        }

    }
}
