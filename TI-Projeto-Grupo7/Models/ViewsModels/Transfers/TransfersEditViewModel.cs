namespace TI_Projeto_Grupo7.Models.ViewsModels.Transfers
{
    public class TransfersEditViewModel
    {
        public int id_transfer { get; set; }

        public int id_account { get; set; }

        public DateTime transfer_date { get; set; }

        public decimal transfer_value { get; set; }

        public int account_number { get; set; }
    }
}
