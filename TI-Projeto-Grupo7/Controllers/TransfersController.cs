using Microsoft.AspNetCore.Mvc;
using TI_Projeto_Grupo7.Services;
using TI_Projeto_Grupo7.Models.DTO;
using TI_Projeto_Grupo7.Models.ViewsModels.Transfers;
using System.Security.Claims;
using TI_Projeto_Grupo7.Helpers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;

namespace TI_Projeto_Grupo7.Controllers
{
    [Authorize]
    public class TransfersController : Controller
    {
        private readonly TransferService _transferService;
        private readonly ILogger<TransferService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransfersController(TransferService transferService, ILogger<TransferService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _transferService = transferService;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;

        }

        public IActionResult Transfers()
        {
            return View(GetIndexViewModel());
        }

        public IActionResult Create()
        {
            TransfersCreateViewModel model = new TransfersCreateViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult MakeTransfer(TransfersCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for transfer creation.");
                return View("Transfers", GetIndexViewModel);
            }

            try{

                TransfersDTO dto = new TransfersDTO{

                    id_account = model.id_account,
                    transfer_date = DateTime.Now,
                    account_number = model.account_number,
                    transfer_value = model.transfer_value
                };

                ExecutionResult<TransfersDTO> result = _transferService.Insert(dto, GetUsername());

                if (!result.Status){
              
                    _logger.LogWarning("Failed to create transfer: {Message}", result.Message);
                    return View("Transfers", model);
                }

                _logger.LogInformation("Transfer successfully created with ID: {id_transfer}", result.Results?.id_transfer);
                return RedirectToAction("Transfers");
            
            }catch (Exception ex){

                _logger.LogError(ex, "An error occurred while creating the transfer.");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");
                return View("Transfers", model);
            }
        }

        public IActionResult TransfersHistory()
        {
            try{
                
                var result = _transferService.Get();

                if (result.Status){
                 
                    var history = result.Results.Select(transfer => new TransfersHistoryViewModel{

                        id_transfer = transfer.id_transfer,
                        id_account = transfer.id_account,
                        transfer_value = transfer.transfer_value,
                        account_number = transfer.account_number

                    }).ToList();

                    return View(history); 
                }

                return View(new List<TransfersHistoryViewModel>()); 
            
            }catch (Exception ex){

                _logger.LogError(ex, "An error occurred while fetching the transfer history.");

                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again later.");

                return View(new List<TransfersHistoryViewModel>());
            }
        }

        private string GetUsername()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }

        private TransfersIndexViewModel GetIndexViewModel()
        {
            TransfersIndexViewModel model = new TransfersIndexViewModel();
            model.transfers = _transferService.Get().Results;

            return model;
        }
    }
}
