using System.ComponentModel.DataAnnotations;

namespace Cyber2_Demo.BLL.Models
{
    public class Utilisateur
    {
        public Utilisateur() { }
        public Utilisateur(int id, string nom, string prenom, string email)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Email = email;
        }

        public int Id { get; set; }
        public string Nom {  get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
    }
}
