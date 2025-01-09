using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using TI_Grupo7.Areas.Identity.Data;
using TI_Projeto_Grupo7.Helpers;
using TI_Projeto_Grupo7.Models.DTO;
using TI_Projeto_Grupo7.Models.ViewsModels.Admin;
using TI_Projeto_Grupo7.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder.Extensions;
using Application.Data;
using System.Text.Encodings.Web;

namespace TI_Projeto_Grupo7.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PendingAccountsService _pendingAccountsService;
        private readonly SupportService _supportService;
        private readonly AccountsService _accountsService;
        private readonly ILogger<PendingAccountsService> _loggerPA;
        private readonly ILogger<SupportService> _loggerSup;
        private readonly ILogger<AccountsService> _loggerAcc;

        public AdminController(IOptions<MyOptions> myoptions,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            ILogger<PendingAccountsService> loggerPA,
            ILogger<SupportService> loggerSup,
            ILogger<AccountsService> loggerAcc)
        {
            _userManager = userManager;
            _pendingAccountsService = new PendingAccountsService(myoptions, loggerPA);
            _supportService = new SupportService(myoptions, loggerSup);
            _accountsService = new AccountsService(myoptions, loggerAcc);
            _loggerPA = loggerPA;
            _loggerSup = loggerSup;
            _loggerAcc = loggerAcc;
        }
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
            var model = new AdminIndexViewModel();
            model.PendingAccounts = _pendingAccountsService.Get().Results;
            model.Support = _supportService.Get().Results;
            return View(model);
        }
        public IActionResult user()
        {
            var model = new AdminIndexViewModel();
            model.AspNetUsers = _userManager.Users.ToList();
            return View(model);
        }


        private string GetUsername()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }
    }
}
