using TI_Grupo7.Areas.Identity.Data;
using TI_Projeto_Grupo7.Models.DTO;

namespace TI_Projeto_Grupo7.Models.ViewsModels.Admin
{
    public class AdminIndexViewModel
    {
        public IEnumerable<ApplicationUser> AspNetUsers { get; set; }
        public List<PendingAccountsDTO> PendingAccounts { get; set; }
        public List<SupportDTO> Support { get; set; }
        public List<AccountsDTO> Accounts { get; set; }
    }
}
