using Cyber2_Demo.API.Context.Mapper;
using Cyber2_Demo.API.DTO.BlogPost;
using Cyber2_Demo.API.DTO.Utilisateur;
using Cyber2_Demo.API.Mapper;
using Cyber2_Demo.BLL.CustomExceptions;
using Cyber2_Demo.BLL.Interfaces;
using Cyber2_Demo.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BlogPostDetailDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<BlogPostDetailDTO> Create(CreateBlogPostDTO post)
        {
            Claim? idClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            Utilisateur? utilisateur = _utilisateurService.GetById(Convert.ToInt32(idClaim.Value));

            if(utilisateur is not null )
            {
                BlogPost? newPost = _blogPostService.Create(post.ToBlogPost(utilisateur));

                return CreatedAtAction("GetById", new { id = newPost.Id}, newPost.ToBlogPostDetail());

            }

            return BadRequest();

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BlogPostDetailDTO>))]
        public ActionResult<IEnumerable<BlogPostDetailDTO>> GetAll()
        {
            return Ok(_blogPostService.GetAll().ToBlogPostList());

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

        [HttpDelete]
        [Route("{id:int}")] // Annotation alternative pour indiquer la route
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(int))]
        public ActionResult Delete(int id)
        {
            bool delete = _blogPostService.Delete(id);

            return delete ? NoContent() : NotFound(id);

        }


        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BlogPostDetailDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<BlogPostDetailDTO> Update(int id, CreateBlogPostDTO post)
        {
            Claim? idClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            Utilisateur? auteur = _utilisateurService.GetById(Convert.ToInt32(idClaim.Value));
            BlogPost? postToCheckAuthor = _blogPostService.GetById(id);
            if (ModelState.IsValid && auteur.Id == postToCheckAuthor.Auteur.Id)
            {
                BlogPost? postToUpdate = post.ToBlogPost();
                postToUpdate.Id = id;

                BlogPostDetailDTO? postUpdated = _blogPostService.Update(postToUpdate)?.ToBlogPostDetail();

                if (postUpdated is not null)
                {
                    return Ok(postUpdated);
                }


                return NotFound(id);

            }
            return BadRequest(ModelState);
        }
    }
}
