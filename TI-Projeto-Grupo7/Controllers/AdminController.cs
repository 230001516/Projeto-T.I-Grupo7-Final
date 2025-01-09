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
using TI_Projeto_Grupo7.Models.ViewsModels.PendingAccounts;

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

        public IActionResult EditPendAcc(int id_accountPending)
        {
            AdminEditViewModel model = new AdminEditViewModel();

            PendingAccountsDTO dto = _pendingAccountsService.Get(id_accountPending).Results.FirstOrDefault();
            model.id_accountPending = dto.id_accountPending;
            model.id_user = dto.id_user;
            model.id_worker = dto.id_worker;
            model.account_state = dto.account_state;
            model.motive = dto.motive;

            return View("EditPendAcc", model);
        }

        [HttpPost]
        public IActionResult EditPendAcc(AdminEditViewModel model)
        {
            try
            {

                PendingAccountsDTO dto = new PendingAccountsDTO();
                model.id_accountPending = dto.id_accountPending;
                model.id_user = dto.id_user;
                model.id_worker = dto.id_worker;
                model.account_state = dto.account_state;
                model.motive = dto.motive;

                ExecutionResult<PendingAccountsDTO> result = _pendingAccountsService.Update(dto, GetUsername());

                if (!result.Status)
                {

                    _loggerPA.LogWarning("Failed to update Pending Account: {Message}", result.Message);
                    return View("Login", model);

                }

                _loggerPA.LogInformation("Pending Account successfully updated with ID: {id_accountPending}", result.Results?.id_accountPending);
                return RedirectToAction("Login");

            }
            catch (Exception ex)
            {

                _loggerPA.LogError(ex, "An error occurred while creating the pending account.");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
                return View("EditPendAcc", model);
            }
        }

        public IActionResult Delete(int id_accountPending)
        {
            try
            {
                if (id_accountPending <= 0)
                {
                    _loggerPA.LogWarning("Invalid ID for deletion.");
                    return RedirectToAction("dashboard");
                }

                AdminIndexViewModel model = new AdminIndexViewModel();

                ExecutionResult<PendingAccountsDTO> result = _pendingAccountsService.Delete(id_accountPending);
                if (!result.Status)
                {

                    _loggerPA.LogWarning("Failed to delete Pending Account: {Message}", result.Message);
                    return View("table", model);

                }

                _loggerPA.LogInformation("Pending Account successfully deleted with ID: {id_accountPending}", result.Results?.id_accountPending);
                return RedirectToAction("table");

            }
            catch (Exception ex)
            {

                _loggerPA.LogError(ex, "An error occurred while creating the pending account.");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
                return View("Index");
            }
        }

        private AdminIndexViewModel GetIndexViewModel()
        {
            AdminIndexViewModel model = new AdminIndexViewModel();
            model.PendingAccounts = _pendingAccountsService.Get().Results;

            return model;
        }
    }
}
