using Cyber2_Demo.API.Context.Mapper;
using Cyber2_Demo.API.DTO.BlogPost;
using Cyber2_Demo.Domain.Models;

namespace Cyber2_Demo.API.Mapper
{
    public static class BlogPostMapper
    {
        public static BlogPost ToBlogPost(this CreateBlogPostDTO post, Utilisateur utilisateur)
        {
            return new BlogPost
            {
                Contenu = post.Contenu,
                Titre = post.Titre,
                Auteur = utilisateur
            };
        }

        public static BlogPost ToBlogPost(this CreateBlogPostDTO post)
        {
            return new BlogPost
            {
                Contenu = post.Contenu,
                Titre = post.Titre,
            };
        }

        public static BlogPostDetailDTO ToBlogPostDetail(this BlogPost post)
        {
            return new BlogPostDetailDTO
            {
                Id = post.Id,
                Contenu = post.Contenu,
                Titre = post.Titre,
                Auteur = post.Auteur.ToDetailDTO()
            };
        }

        public static IEnumerable<BlogPostDetailDTO> ToBlogPostList(this IEnumerable<BlogPost> posts)
        {
            List<BlogPostDetailDTO> postsDetail = new List<BlogPostDetailDTO>();
            foreach(BlogPost post in posts)
            {
                postsDetail.Add(post.ToBlogPostDetail());
            }

            return postsDetail;
        }
    }
}
