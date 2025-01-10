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
using TI_Projeto_Grupo7.Models.ViewsModels.Accounts;


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
            return View(GetIndexViewModel());
        }
        public IActionResult user()
        {
            return View(GetIndexViewModel());
        }


        // Pending Accounts Actions
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
                    return View("EditPendAcc", model);

                }

                _loggerPA.LogInformation("Pending Account successfully updated with ID: {id_accountPending}", result.Results?.id_accountPending);
                return RedirectToAction("table", model);

            }
            catch (Exception ex)
            {

                _loggerPA.LogError(ex, "An error occurred while editing the pending account.");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
                return View("EditPendAcc", model);
            }
        }

        public IActionResult DeletePendAcc(int id_accountPending)
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

        // Accounts Actions
        public IActionResult DeleteAcc(int id_account)
        {
            try
            {
                AdminIndexViewModel model = new AdminIndexViewModel();

                ExecutionResult<AccountsDTO> result = _accountsService.Delete(id_account);
                if (!result.Status)
                {

                    _loggerAcc.LogWarning("Failed to delete Account: {Message}", result.Message);
                    return View("Login", model);

                }

                _loggerAcc.LogInformation("Account successfully deleted with ID: {id_account}", result.Results?.id_account);
                return RedirectToAction("Login");

            }
            catch (Exception ex)
            {

                _loggerAcc.LogError(ex, "An error occurred while deleting the account.");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
                return View("Index");
            }
        }

        public IActionResult EditAcc(int id_account)
        {
            AdminEditViewModel model = new AdminEditViewModel();

            AccountsDTO dto = _accountsService.Get(id_account).Results.FirstOrDefault();
            dto.id_account = model.id_account;
            dto.id_pendingAccount = model.id_pendingAccount;
            dto.account_number = model.account_number;
            dto.balance = model.balance;

            return View(model);
        }

        [HttpPost]
        public IActionResult EditAcc(AdminEditViewModel model)
        {
            try
            {

                AccountsDTO dto = new AccountsDTO();
                dto.id_account = model.id_account;
                dto.id_pendingAccount = model.id_pendingAccount;
                dto.account_number = model.account_number;
                dto.balance = model.balance;

                ExecutionResult<AccountsDTO> result = _accountsService.Update(dto, GetUsername());

                if (!result.Status)
                {

                    _loggerAcc.LogWarning("Failed to update Account: {Message}", result.Message);
                    return View("Login", model);

                }

                _loggerAcc.LogInformation("Account successfully updated with ID: {id_account}", result.Results?.id_account);
                return RedirectToAction("Login");

            }
            catch (Exception ex)
            {

                _loggerAcc.LogError(ex, "An error occurred while updating the account.");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
                return View("Index", model);
            }
        }

        [HttpPost]
        public IActionResult CreateAcc(AdminCreateViewModel model)
        {
            try
            {
                AccountsDTO dto = new AccountsDTO();
                dto.id_account = model.id_account;
                dto.id_pendingAccount = model.id_pendingAccount;
                dto.account_number = model.account_number;
                dto.balance = model.balance = 0;

                ExecutionResult<AccountsDTO> result = _accountsService.Insert(dto, GetUsername());

                if (!result.Status)
                {

                    _loggerAcc.LogWarning("Failed to create account: {Message}", result.Message);
                    return View("table", model);
                }

                _loggerAcc.LogInformation("Account successfully created with ID: {id_account}", result.Results?.id_account);
                return RedirectToAction("table");

            }
            catch (Exception ex)
            {

                _loggerAcc.LogError(ex, "An error occurred while creating the account.");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
                return View("table", model);
            }
        }
        

        // Private methods
        private AdminIndexViewModel GetIndexViewModel()
        {
            AdminIndexViewModel model = new AdminIndexViewModel();
            model.PendingAccounts = _pendingAccountsService.Get().Results;
            model.AspNetUsers = _userManager.Users.ToList();
            model.Support = _supportService.Get().Results;
            model.Accounts = _accountsService.Get().Results;

            return model;
        }
        private string GetUsername()
        {
            // Validações intermediárias para depuração
            if (_httpContextAccessor == null)
            {
                throw new NullReferenceException("_httpContextAccessor is null.");
            }

            if (_httpContextAccessor.HttpContext == null)
            {
                throw new NullReferenceException("HttpContext is null.");
            }

            if (_httpContextAccessor.HttpContext.User == null)
            {
                throw new NullReferenceException("HttpContext.User is null.");
            }

            if (_httpContextAccessor.HttpContext.User.Identity == null)
            {
                throw new NullReferenceException("HttpContext.User.Identity is null.");
            }

            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new Exception("User is not authenticated.");
            }

            return _httpContextAccessor.HttpContext.User.Identity.Name ?? "Anonymous";
        }
    }
}
