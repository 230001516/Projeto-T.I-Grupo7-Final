using System.ComponentModel.DataAnnotations;


namespace TI_Projeto_Grupo7.Models.ViewsModels.Admin
{
    public class AdminCreateViewModel
    {

        // Users
        [Required]
        public int Id { get; set; }

        [Required] 
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public int NIF { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        // Support

        [Required]
        public int id_ticket { get; set; }

        [Required]
        public string supName { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string subject { get; set; }

        [Required]
        public string message { get; set; }

        // Pending Accounts

        [Required]
        public int id_accountPending { get; set; }

        [Required]
        public int id_user { get; set; }

        [Required]
        public int account_state { get; set; }

        [Required]
        public int id_worker { get; set; }

        [Required]
        public string motive { get; set; }
    }
}
