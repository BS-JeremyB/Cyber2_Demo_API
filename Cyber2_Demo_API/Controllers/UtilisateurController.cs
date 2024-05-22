using Cyber2_Demo.API.Context.Mapper;
using Cyber2_Demo.API.DTO.Utilisateur;
using Cyber2_Demo.BLL.CustomExceptions;
using Cyber2_Demo.BLL.Interfaces;
using Cyber2_Demo.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cyber2_Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateurController : ControllerBase
    {

        private readonly IUtilisateurService _service;

        public UtilisateurController(IUtilisateurService service)
        {
            _service = service;
        }


        [HttpGet]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UtilisateurListDTO>))]
        public ActionResult<IEnumerable<UtilisateurListDTO>> GetAll()
        {
            return Ok(_service.GetAll().ToListDTO());

        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UtilisateurDetailDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(int))]
        public ActionResult<UtilisateurDetailDTO> GetById(int id)
        {
            UtilisateurDetailDTO utilisateur = _service.GetById(id)?.ToDetailDTO();
            if (utilisateur is not null)
            {
                return Ok(utilisateur);
            }

            return NotFound(id);

        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UtilisateurDetailDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UtilisateurDetailDTO> Update(int id, UpdateUtilisateurDTO user)
        {
            if (ModelState.IsValid)
            {
                Utilisateur utilisateurToUpdate = user.ToUtilisateur();
                utilisateurToUpdate.Id = id;

                try
                {

                    UtilisateurDetailDTO? utilisateur = _service.Update(utilisateurToUpdate)?.ToDetailDTO();

                    if(utilisateur is not null)
                    {
                        return Ok(utilisateur);
                    }
                }catch(AlreadyExistException ex)
                {
                    return BadRequest(ex.Message);
                }

                return NotFound(id);

            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("{id:int}")] // Annotation alternative pour indiquer la route
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(int))]
        public ActionResult Delete(int id)
        {
            bool delete = _service.Delete(id);

            return delete ? NoContent() : NotFound(id);

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UtilisateurDetailDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<UtilisateurDetailDTO> Create(CreateUtilisateurDTO utilisateurDTO)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    UtilisateurDetailDTO? utilisateur = _service.Create(utilisateurDTO.ToUtilisateur())?.ToDetailDTO();

                    if(utilisateur is not null)
                    {
                        return CreatedAtAction(nameof(GetById), new { id = utilisateur.Id }, utilisateur);
                    }
                }catch(AlreadyExistException ex)
                {
                    return BadRequest(ex.Message);
                }

            }

            return BadRequest();

        }


        [HttpPost("login")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Utilisateur> Login([FromBody] UtilisateurLoginDTO login)
        {

            string? token = _service.Login(login.Username, login.Password);

            if (token is not null)
            {
                return Ok(token);
            }

            return BadRequest(StatusCodes.Status400BadRequest);
        }
    }
}
