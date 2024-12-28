using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TI_Projeto_Grupo7.Helpers;
using TI_Projeto_Grupo7.Models.DTO;
using TI_Projeto_Grupo7.Models.ViewsModels.Home;
using TI_Projeto_Grupo7.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Humanizer;

namespace TI_Projeto_Grupo7.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly MyOptions _myOptions;
        private readonly DevelopersService _developersService;
        private readonly SupportService _supportService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<DevelopersService> _loggerDev;
        private readonly ILogger<SupportService> _loggerSup;

        public HomeController(IOptions<MyOptions> myOptions, IHttpContextAccessor httpContextAccessor, ILogger<SupportService> loggerSup, ILogger<DevelopersService> loggerDev)
        {
            _developersService = new DevelopersService(myOptions, loggerDev);
            _supportService = new SupportService(myOptions, loggerSup);
            _httpContextAccessor = httpContextAccessor;
            _loggerDev = loggerDev;
            _loggerSup = loggerSup;
        }

        public IActionResult Index()
        {
            return View(GetIndexViewModel());
        }

        public IActionResult Create()
        {
            HomeCreateViewModel model = new HomeCreateViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateDev(HomeCreateViewModel model){

            DevelopersDTO dto = new DevelopersDTO();
            dto.devName = model.devName;

            ExecutionResult<DevelopersDTO> result = _developersService.Insert(dto, GetUsername());
            
            if (!result.Status){

                _loggerDev.LogWarning("Failed to create developer: {Message}", result.Message);
                return View("Index", model);
            
            }

            _loggerDev.LogInformation("Successfully created developer: {DevName}", dto.devName);
            return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult CreateSup(HomeCreateViewModel model)
        {
            SupportDTO dto = new SupportDTO() {
                supName = model.supName,
                email = model.email,
                subject = model.subject,
                message = model.message
            };
            
            ExecutionResult<SupportDTO> result = _supportService.Insert(dto, GetUsername());

            if (!result.Status){

                _loggerDev.LogWarning("Failed to create ticket: {Message}", result.Message);
                return View("Index", model);

            }

            _loggerDev.LogInformation("Successfully created ticket: {Subject}", dto.subject);
            return RedirectToAction("Index");
        }

        public IActionResult EditDev(int id_developer){

            HomeEditViewModel model = new HomeEditViewModel();

            DevelopersDTO developer = _developersService.Get(id_developer).Results.FirstOrDefault();
            model.id_developer = developer.id_developer;
            model.devName = developer.devName;
            model.devDescription = developer.devDescription;
            model.twitter = developer.twitter;
            model.instagram = developer.instagram;
            model.linkedin = developer.linkedin;
            model.devImage = developer.devImage;

            return View(model);
        }

        public IActionResult EditSup(int id_ticket){

            HomeEditViewModel model = new HomeEditViewModel();

            SupportDTO support = _supportService.Get(id_ticket).Results.FirstOrDefault();
            model.id_ticket = support.id_ticket;
            model.supName = support.supName;

            return View(model);
        }

        [HttpPost]
        public IActionResult EditDev(HomeEditViewModel model){

            DevelopersDTO developer = new DevelopersDTO();
            developer.id_developer = model.id_developer;
            developer.devName = model.devName;

            ExecutionResult<DevelopersDTO> resultd = _developersService.Update(developer, GetUsername());

            if (!resultd.Status){

                _loggerDev.LogWarning("Failed to edit developer: {Message}", resultd.Message);
                return View("Index", model);
            }

            _loggerDev.LogInformation("Successfully edited developer: {DevName}", developer.devName);
            return RedirectToAction("Index");

  
        }

        [HttpPost]
        public IActionResult EditSup(HomeEditViewModel model){

            SupportDTO support = new SupportDTO();
            support.id_ticket = model.id_ticket;
            support.supName = model.supName;

            ExecutionResult<SupportDTO> results = _supportService.Update(support, GetUsername());

            if (!results.Status){

                _loggerDev.LogWarning("Failed to edit ticket: {Message}", results.Message);
                return View("Index", model);

            }

            _loggerDev.LogInformation("Successfully edited ticket: {Subject}", support.subject);
            return RedirectToAction("Index");

        }

        public IActionResult DeleteDev(int id_developer){

            HomeEditViewModel model = new HomeEditViewModel();

            ExecutionResult<DevelopersDTO> resultd = _developersService.Delete(id_developer);

            if (!resultd.Status){

                _loggerDev.LogWarning("Failed to delete developer: {Message}", resultd.Message);
                return BadRequest(resultd.Message);
            
            }

            _loggerDev.LogInformation("Successfully deleted developer with ID: {Id}", id_developer);
            return RedirectToAction("Index");

        }

        public IActionResult DeleteSup(int id_ticket){

            HomeEditViewModel model = new HomeEditViewModel();

            ExecutionResult<SupportDTO> results = _supportService.Delete(id_ticket);

            if (!results.Status){

                _loggerDev.LogWarning("Failed to delete support ticket: {Message}", results.Message);
                return BadRequest(results.Message);

            }

            _loggerDev.LogInformation("Successfully deleted support ticket with ID: {Id}", id_ticket);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy(){

            return View("Privacy");

        }

        private HomeIndexViewModel GetIndexViewModel()
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            model.dev = _developersService.Get().Results;
            model.supp = _supportService.Get().Results;

            return model;
        }

        private string GetUsername()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }
    }
}

