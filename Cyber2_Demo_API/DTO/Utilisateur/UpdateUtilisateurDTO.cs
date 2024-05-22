using System.ComponentModel.DataAnnotations;

namespace Cyber2_Demo.API.DTO.Utilisateur
{
    public class UpdateUtilisateurDTO
    {
        [Required]
        [MinLength(2)]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
    }
}
