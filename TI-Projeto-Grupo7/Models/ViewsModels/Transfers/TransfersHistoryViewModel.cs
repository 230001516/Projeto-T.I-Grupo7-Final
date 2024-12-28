namespace TI_Projeto_Grupo7.Models.ViewsModels.Transfers
{
    public class TransfersHistoryViewModel
    {
        public int id_transfer { get; set; }
        public int id_account { get; set; }
        public int account_number { get; set; }
        public decimal transfer_value { get; set; }
        public string transfer_date { get; set; }
    }
}
