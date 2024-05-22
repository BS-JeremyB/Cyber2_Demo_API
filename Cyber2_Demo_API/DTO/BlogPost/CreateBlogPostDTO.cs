using System.ComponentModel.DataAnnotations;

namespace Cyber2_Demo.API.DTO.BlogPost
{
    public class CreateBlogPostDTO
    {
        [Required]
        public string Titre { get; set; }
        [Required]
        public string Contenu { get; set; }

    }
}
