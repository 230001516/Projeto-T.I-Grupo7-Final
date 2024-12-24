using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TI_Projeto_Grupo7.Helpers;
using TI_Projeto_Grupo7.Models.DTO;
using TI_Projeto_Grupo7.Models.ViewsModels.Home;
using TI_Projeto_Grupo7.Services;
using System.Security.Claims;

namespace LojaOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyOptions _myOptions;
        private readonly DevelopersService _developersService;
        private readonly SupportService _supportService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IOptions<MyOptions> myOptions, IHttpContextAccessor httpContextAccessor)
        {
            _developersService = new DevelopersService(myOptions);
            _supportService = new SupportService(myOptions);

            _httpContextAccessor = httpContextAccessor;
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
        public IActionResult CreateDev(HomeCreateViewModel model)
        {
            DevelopersDTO dto = new DevelopersDTO();
            dto.devName = model.devName;

            ExecutionResult<DevelopersDTO> result = _developersService.Insert(dto, GetUsername());


            return View("Index", GetIndexViewModel());
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

            return RedirectToAction("Index");
        }

        public IActionResult EditDev(int id_developer)
        {
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

        public IActionResult EditSup(int id_ticket)
        {
            HomeEditViewModel model = new HomeEditViewModel();

            SupportDTO support = _supportService.Get(id_ticket).Results.FirstOrDefault();
            model.id_ticket = support.id_ticket;
            model.supName = support.supName;

            return View(model);
        }

        [HttpPost]
        public IActionResult EditDev(HomeEditViewModel model)
        {
            DevelopersDTO developer = new DevelopersDTO();
            developer.id_developer = model.id_developer;
            developer.devName = model.devName;

            ExecutionResult<DevelopersDTO> resultd = _developersService.Update(developer, GetUsername());

            return View(model);
        }

        [HttpPost]
        public IActionResult EditSup(HomeEditViewModel model)
        {
            SupportDTO support = new SupportDTO();
            support.id_ticket = model.id_ticket;
            support.supName = model.supName;

            ExecutionResult<SupportDTO> results = _supportService.Update(support, GetUsername());

            return View(model);
        }

        public IActionResult DeleteDev(int id_developer)
        {
            HomeEditViewModel model = new HomeEditViewModel();

            ExecutionResult<DevelopersDTO> resultd = _developersService.Delete(id_developer);

            return View("Index", GetIndexViewModel());
        }

        public IActionResult DeleteSup(int id_ticket)
        {
            HomeEditViewModel model = new HomeEditViewModel();

            ExecutionResult<SupportDTO> results = _supportService.Delete(id_ticket);

            return View("Index", GetIndexViewModel());
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

