﻿namespace TI_Projeto_Grupo7.Models.ViewsModels.PendingAccounts
{
    public class PendingAccountsEditViewModel
    {
        public int id_accountPending {  get; set; }
        public string id_user { get; set; }    
        public int account_state { get; set; }  
        public string id_worker { get; set; }
        public string motive { get; set; }   
    }
}
