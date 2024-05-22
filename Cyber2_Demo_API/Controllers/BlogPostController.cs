using Cyber2_Demo.API.DTO.BlogPost;
using Cyber2_Demo.API.DTO.Utilisateur;
using Cyber2_Demo.API.Mapper;
using Cyber2_Demo.BLL.Interfaces;
using Cyber2_Demo.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cyber2_Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {

        private readonly IUtilisateurService _utilisateurService;
        private readonly IBlogPostService _blogPostService;

        public BlogPostController(IUtilisateurService utilisateurService, IBlogPostService blogPostService)
        {
            _utilisateurService = utilisateurService;
            _blogPostService = blogPostService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BlogPostDetailDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<BlogPostDetailDTO> Create(CreateBlogPostDTO post)
        {
            Utilisateur? utilisateur = _utilisateurService.GetById(post.Utilisateur_Id);

            if(utilisateur is not null )
            {
                BlogPost? newPost = _blogPostService.Create(post.ToBlogPost(utilisateur));

                return Ok(newPost.ToBlogPostDetail());

            }

            return BadRequest();

        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BlogPostDetailDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(int))]
        public ActionResult<BlogPostDetailDTO> GetById(int id)
        {
            BlogPostDetailDTO? post = _blogPostService.GetById(id)?.ToBlogPostDetail();

            if (post is not null)
            {
                return Ok(post);
            }

            return BadRequest();
        }
    }
}
