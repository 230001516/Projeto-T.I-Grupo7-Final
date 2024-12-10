using Microsoft.AspNetCore.Mvc;

namespace TI_Grupo7.Controllers
{
    public class AutenticacaoController : Controller
    {

        public IActionResult Login(string email, string pass) 
        {
            ViewData["Nome"] = email;
            ViewData["email"] = pass;

            return View();
        }

        public IActionResult Register(string nomeconta, string email, string pass1, string validacao)
        {
            ViewData["Nome"] = nomeconta;
            ViewData["email"] = email;
            ViewData["Pass1"] = pass1;
            ViewData["Validacao"] = validacao;
            return View();
        }
    }
}
