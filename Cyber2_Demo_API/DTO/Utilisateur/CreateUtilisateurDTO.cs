using System.ComponentModel.DataAnnotations;

namespace Cyber2_Demo.API.DTO.Utilisateur
{
    public class CreateUtilisateurDTO
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

        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=!]).{8,25}$", ErrorMessage = "Le mot de passe doit contenir 1 Maj, 1 Min, 1 chiffre, 1 char spécial")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Les mots de passes ne correspondent pas")]
        public string PasswordVerif { get; set; }
    }
}
