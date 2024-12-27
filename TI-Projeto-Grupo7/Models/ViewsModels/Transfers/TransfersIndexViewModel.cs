using TI_Projeto_Grupo7.Models.DTO;

namespace TI_Projeto_Grupo7.Models.ViewsModels.Transfers
{
    public class TransfersIndexViewModel
    {
        public List<TransfersDTO> transfers { get; set; }

        public List<UsersDTO> users { get; set; }

        public List<AccountsDTO> accounts { get; set; }

    }
}
