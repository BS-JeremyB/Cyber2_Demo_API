using System.ComponentModel.DataAnnotations;

namespace Cyber2_Demo.API.DTO.Utilisateur
{
    public class UtilisateurLoginDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
