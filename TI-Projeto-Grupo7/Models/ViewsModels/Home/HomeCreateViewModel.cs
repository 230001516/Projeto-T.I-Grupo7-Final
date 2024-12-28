using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using TI_Projeto_Grupo7.Models.DTO;

namespace TI_Projeto_Grupo7.Models.ViewsModels.Home

{
    public class HomeCreateViewModel
    {

        // Developers
        [Required]
        public string devName { get; set; }

        [Required]
        public string devDescription { get; set; }

        public string twitter { get; set; }

        public string instagram { get; set; }

        public string linkedin { get; set; }

        [Required]
        public string devImage { get; set; }


        // Support

        [Required]
        public string supName { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string subject { get; set; }

        [Required]
        public string message { get; set; }

        // Users

        [Required]
        public int is_worker { get; set; }

    }
}
