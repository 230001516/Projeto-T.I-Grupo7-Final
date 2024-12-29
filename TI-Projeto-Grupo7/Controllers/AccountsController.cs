using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using TI_Projeto_Grupo7.Helpers;
using TI_Projeto_Grupo7.Models.DTO;
using TI_Projeto_Grupo7.Models.ViewsModels.Accounts;
using TI_Projeto_Grupo7.Services;

namespace TI_Projeto_Grupo7.Controllers
{
    public class AccountsController : Controller
    {
        private readonly MyOptions _myOptions;
        private readonly AccountsService _accountsService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AccountsService> _logger;

        public AccountsController(IOptions<MyOptions> myOptions, IHttpContextAccessor httpContextAccessor, ILogger<AccountsService> logger)
        {
            _accountsService = new AccountsService(myOptions, logger);
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;

        }
        public IActionResult Index()
        {
            return View(GetIndexViewModel());
        }
        public IActionResult Create()
        {
            AccountsCreateViewModel model = new AccountsCreateViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(AccountsCreateViewModel model)
        {
            try
            {
                AccountsDTO dto = new AccountsDTO();
                dto.id_account = model.id_account;
                dto.id_pendingAccount = model.id_pendingAccount;
                dto.account_number = model.account_number;
                dto.balance = model.balance;

                ExecutionResult<AccountsDTO> result = _accountsService.Insert(dto, GetUsername());

                if (!result.Status)
                {

                    _logger.LogWarning("Failed to create account: {Message}", result.Message);
                    return View("Transfers", model);
                }

                _logger.LogInformation("Account successfully created with ID: {id_account}", result.Results?.id_account);
                return RedirectToAction("Login");

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while creating the account.");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
                return View("Transfers", model);
            }
        }


        public IActionResult Edit(int id_account)
        {
            AccountsEditViewModel model = new AccountsEditViewModel();

            AccountsDTO dto = _accountsService.Get(id_account).Results.FirstOrDefault();
            dto.id_account = model.id_account;
            dto.id_pendingAccount = model.id_pendingAccount;
            dto.account_number = model.account_number;
            dto.balance = model.balance;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(AccountsEditViewModel model)
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

                    _logger.LogWarning("Failed to update Account: {Message}", result.Message);
                    return View("Login", model);

                }

                _logger.LogInformation("Account successfully updated with ID: {id_account}", result.Results?.id_account);
                return RedirectToAction("Login");

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while updating the pending account.");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
                return View("Index", model);
            }
        }

        public IActionResult Delete(int id_account)
        {
            try
            {
                AccountsEditViewModel model = new AccountsEditViewModel();

                ExecutionResult<AccountsDTO> result = _accountsService.Delete(id_account);
                if (!result.Status)
                {

                    _logger.LogWarning("Failed to delete Account: {Message}", result.Message);
                    return View("Login", model);

                }

                _logger.LogInformation("Account successfully deleted with ID: {id_account}", result.Results?.id_account);
                return RedirectToAction("Login");

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while deleting the account.");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
                return View("Index");
            }
        }

        private AccountsIndexViewModel GetIndexViewModel()
        {
            AccountsIndexViewModel model = new AccountsIndexViewModel();
            model.Accounts = _accountsService.Get().Results;

            return model;
        }

        private string GetUsername()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }
    }
}

