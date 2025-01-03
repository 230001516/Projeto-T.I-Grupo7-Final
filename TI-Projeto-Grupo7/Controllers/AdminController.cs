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
        private readonly MyOptions _myOptions;
        private readonly AccountsService _accountsService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AccountsService> _logger;
        private readonly TransferService _transferService;
        private readonly PendingAccountsService _pendingAccountsService;

        public AdminController(
           MyOptions myOptions,
           IHttpContextAccessor httpContextAccessor,
           ILogger<AccountsService> logger,
           TransferService transferService,
           PendingAccountsService pendingAccountsService,
           AccountsService accountsService)  
        {
            _myOptions = myOptions;
            _httpContextAccessor = httpContextAccessor;
            _accountsService = accountsService; 
            _logger = logger;
            _transferService = transferService;
            _pendingAccountsService = pendingAccountsService; 
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
            return View();
        }
        public IActionResult user()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ApprovePendingAccount(int id_pendingAccount)
        {
            try
            {

                string username = GetUsername();
                if (string.IsNullOrEmpty(username))
                {
                    _logger.LogWarning("Unable to retrieve username for processing approved account.");
                    return BadRequest("User not authenticated.");
                }

                var result = _accountsService.ProcessApprovedAccount(id_pendingAccount, username);

                if (!result.Status)
                {
                    _logger.LogWarning("Failed to process approved account: {Message}", result.Message);
                    TempData["ErrorMessage"] = result.Message;
                    return RedirectToAction("Index"); 
                }

                _logger.LogInformation("Successfully processed approved account with PendingAccount ID: {id_pendingAccount}", id_pendingAccount);
                TempData["SuccessMessage"] = "Account created successfully.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while approving the pending account with ID: {id_pendingAccount}", id_pendingAccount);
                TempData["ErrorMessage"] = "An unexpected error occurred. Please try again later.";
                return RedirectToAction("Index");
            }
        }

        private string GetUsername()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }
    }
}
