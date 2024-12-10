using Microsoft.AspNetCore.Mvc;

namespace TI_Grupo7.Controllers
{
    public class TransferenciaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Transferencia(int transferencia)
        {
            ViewData["Transferências"] = transferencia;

            return View();
        }

        public IActionResult HisTransferencias(int hisTrans)
        {
            ViewData["Histórico Transferências"] = hisTrans;

            return View();
        }

    }
}
