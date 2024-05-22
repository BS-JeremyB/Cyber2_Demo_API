using Cyber2_Demo.API.DTO.Utilisateur;

namespace Cyber2_Demo.API.DTO.BlogPost
{
    public class BlogPostDetailDTO
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Contenu { get; set; }
        public UtilisateurDetailDTO Auteur { get; set; }
    }
}
