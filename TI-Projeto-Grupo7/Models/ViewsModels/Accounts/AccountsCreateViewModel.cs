namespace TI_Projeto_Grupo7.Models.ViewsModels.Accounts
{
    public class AccountsCreateViewModel
    {
        public int id_account { get; set; }
        public int id_pendingAccount {  get; set; }

        public decimal balance { get; set; }

        public int account_number { get; set; }
    }
}
