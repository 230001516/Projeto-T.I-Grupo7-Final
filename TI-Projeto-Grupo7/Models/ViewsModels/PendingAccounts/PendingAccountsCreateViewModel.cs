using System.ComponentModel.DataAnnotations;

namespace TI_Projeto_Grupo7.Models.ViewsModels.PendingAccounts
{
    public class PendingAccountsCreateViewModel
    {
        [Required]
        [Display(Name = "User ID")]
        public int id_user { get; set; }

        [Required]
        [Display(Name = "Reason")]
        public string motive { get; set; }
    }
}
