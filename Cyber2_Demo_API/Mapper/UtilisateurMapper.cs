using Cyber2_Demo.API.Context.Models.DTO;
using Cyber2_Demo.API.DTO;
using Cyber2_Demo.Domain.Models;

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
                Username = utilisateurDTO.Username,
                Password = utilisateurDTO.Password,
            };
        }

        public static UtilisateurListDTO UtilisateurInfo(this Utilisateur utilisateurDTO)
        {
            return new UtilisateurListDTO
            {
                Id = utilisateurDTO.Id,
                Email = utilisateurDTO.Email,
            };
        }

        public static IEnumerable<UtilisateurListDTO> ToListDTO(this IEnumerable<Utilisateur> utilisateurs)
        {
            List<UtilisateurListDTO> utilisateursDTO = new List<UtilisateurListDTO>();
            foreach (Utilisateur utilisateur in utilisateurs)
            {
                utilisateursDTO.Add(utilisateur.UtilisateurInfo());   
            }

            return utilisateursDTO;

        }

    }
}
