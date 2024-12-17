namespace TI_Projeto_Grupo7.Models.ViewsModels.Home
{
    public class HomeEditViewModel
    {
        // Developers
        public int id_developer { get; set; }

        public string devName { get; set; }

        public string description { get; set; }

        public string twitter { get; set; }

        public string instagram { get; set; }

        public string linkedin { get; set; }

        public string image { get; set; }

        //Support
        public int id_ticket { get; set; }

        public int id_user { get; set; }

        public string supName { get; set; }

        public string email { get; set; }

        public string subject { get; set; }

        public string message { get; set; }
    }
}
