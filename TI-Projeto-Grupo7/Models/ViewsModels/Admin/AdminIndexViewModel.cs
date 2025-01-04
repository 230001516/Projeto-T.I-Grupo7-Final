using TI_Grupo7.Areas.Identity.Data;

namespace TI_Projeto_Grupo7.Models.ViewsModels.Admin
{
    public class AdminIndexViewModel
    {
        public IEnumerable<ApplicationUser> AspNetUsers { get; set; }
    }
}
