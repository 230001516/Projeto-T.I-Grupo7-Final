using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TI_Projeto_Grupo7.Helpers;
using TI_Projeto_Grupo7.Models.DTO;
using TI_Projeto_Grupo7.Models.ViewsModels.PendingAccounts;
using TI_Projeto_Grupo7.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Humanizer;

namespace TI_Projeto_Grupo7.Controllers
{
    [Authorize]
    public class PendingAccountsController : Controller
    {
        private readonly MyOptions _myOptions;
        private readonly PendingAccountsService _pendingAccountsService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<PendingAccountsService> _loggerPendAcc;

        public PendingAccountsController(IOptions<MyOptions> myOptions, IHttpContextAccessor httpContextAccessor, ILogger<PendingAccountsService> loggerPendAcc)
        {
            _pendingAccountsService = new PendingAccountsService(myOptions, loggerPendAcc);
            _httpContextAccessor = httpContextAccessor;
            _loggerPendAcc = loggerPendAcc;

        }
        public IActionResult Index()
        {
            return View(GetIndexViewModel());
        }
        public IActionResult Create()
        {
            PendingAccountsCreateViewModel model = new PendingAccountsCreateViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(PendingAccountsCreateViewModel model)
        {
            try
            {
                PendingAccountsDTO dto = new PendingAccountsDTO();
                dto.id_user = model.id_user;
                dto.motive = model.motive;
                dto.account_state = model.account_state;

                ExecutionResult<PendingAccountsDTO> result = _pendingAccountsService.Insert(dto, GetUsername());

                if (!result.Status)
                {

                    _loggerPendAcc.LogWarning("Failed to create pending account: {Message}", result.Message);
                    return View("Transfers", model);
                }

                _loggerPendAcc.LogInformation("Pending Account successfully created with ID: {id_accountPending}", result.Results?.id_accountPending);
                return RedirectToAction("Transfers");

            }
            catch (Exception ex)
            {

                _loggerPendAcc.LogError(ex, "An error occurred while creating the transfer.");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
                return View("Transfers", model);
            }
        }


        public IActionResult Edit(int id_accountPending)
        {
            PendingAccountsEditViewModel model = new PendingAccountsEditViewModel();

            PendingAccountsDTO dto = _pendingAccountsService.Get(id_accountPending).Results.FirstOrDefault();
            model.id_accountPending = dto.id_accountPending;
            model.id_user = dto.id_user;
            model.id_worker = dto.id_worker;
            model.account_state = dto.account_state;
            model.motive = dto.motive;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(PendingAccountsEditViewModel model)
        {
            try { 

                PendingAccountsDTO dto = new PendingAccountsDTO();
                model.id_accountPending = dto.id_accountPending;
                model.id_user = dto.id_user;
                model.id_worker = dto.id_worker;
                model.account_state = dto.account_state;
                model.motive = dto.motive;

                ExecutionResult<PendingAccountsDTO> result = _pendingAccountsService.Update(dto, GetUsername());
            
                if (!result.Status){

                    _loggerPendAcc.LogWarning("Failed to update Pending Account: {Message}", result.Message);
                    return View("Login", model);
                
                }

                _loggerPendAcc.LogInformation("Pending Account successfully updated with ID: {id_accountPending}", result.Results?.id_accountPending);
                return RedirectToAction("Login");

            }catch (Exception ex){

                _loggerPendAcc.LogError(ex, "An error occurred while creating the pending account.");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
                return View("Index", model);
            }
        }

        public IActionResult Delete(int id_accountPending)
        {
            try
            {
                PendingAccountsEditViewModel model = new PendingAccountsEditViewModel();

                ExecutionResult<PendingAccountsDTO> result = _pendingAccountsService.Delete(id_accountPending);
                if (!result.Status)
                {

                    _loggerPendAcc.LogWarning("Failed to delete Pending Account: {Message}", result.Message);
                    return View("Login", model);

                }

                _loggerPendAcc.LogInformation("Pending Account successfully deleted with ID: {id_accountPending}", result.Results?.id_accountPending);
                return RedirectToAction("Login");

            }catch (Exception ex){

                _loggerPendAcc.LogError(ex, "An error occurred while creating the pending account.");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
                return View("Index");
            }
        }

        private PendingAccountsIndexViewModel GetIndexViewModel()
        {
            PendingAccountsIndexViewModel model = new PendingAccountsIndexViewModel();
            model.PendingAccounts = _pendingAccountsService.Get().Results;

            return model;
        }

        private string GetUsername()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        } 
    }

}
