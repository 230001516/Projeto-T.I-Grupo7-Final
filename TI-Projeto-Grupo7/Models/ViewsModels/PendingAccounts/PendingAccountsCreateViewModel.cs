using System.ComponentModel.DataAnnotations;

namespace TI_Projeto_Grupo7.Models.ViewsModels.PendingAccounts
{
    public class PendingAccountsCreateViewModel
    {
       
        public int account_state { get; set; }

        public int id_user { get; set; }

        public string motive { get; set; }
    }
}
