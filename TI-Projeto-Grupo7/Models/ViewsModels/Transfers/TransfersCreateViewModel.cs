﻿namespace TI_Projeto_Grupo7.Models.ViewsModels.Transfers
{
    public class TransfersCreateViewModel
    {
        public int id_account { get; set; }
        public DateTime transfer_date { get; set; }
        public decimal transfer_value { get; set; }
        public int account_number { get; set; }
    }
}
