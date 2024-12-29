using System.ComponentModel.DataAnnotations;

namespace TI_Projeto_Grupo7.Models.ViewsModels.PendingAccounts
{
    public class PendingAccountsCreateViewModel
    {
        [Required]
        public int account_state { get; set; }
        [Required]
        public int id_user { get; set; }

        [Required]
        public string motive { get; set; }
    }
}
