using Cyber2_Demo.API.Context.Models;
using Cyber2_Demo.API.Context.Models.DTO;

namespace Cyber2_Demo.API.Context.Mapper
{
    public static class UtilisateurMapper
    {
        public static Utilisateur ToUtilisateur(this CreateUtilisateurDTO utilisateurDTO)
        {
            return new Utilisateur
            {
                Email = utilisateurDTO.Email,
                Prenom = utilisateurDTO.Prenom,
                Nom = utilisateurDTO.Nom,
            };
        }
    }
}
