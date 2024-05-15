using Cyber2_Demo.API.Context.Mapper;
using Cyber2_Demo.API.Context.Models.DTO;
using Cyber2_Demo.API.DTO;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Utilisateur))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(int))]
        public ActionResult<Utilisateur> GetById(int id)
        {
            Utilisateur utilisateur = _service.GetById(id);
            if (utilisateur is not null)
            {
                return Ok(utilisateur);
            }

            return NotFound(id);

        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Utilisateur))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Utilisateur> Update(int id, Utilisateur user)
        {
            user.Id = id;
            Utilisateur? utilisateur = _service.Update(user);

            if(utilisateur is not null)
            {
                return Ok(utilisateur);
            }

            return NotFound(id);
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Utilisateur))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<Utilisateur> Create(CreateUtilisateurDTO utilisateurDTO)
        {
            Utilisateur? utilisateur = _service.Create(utilisateurDTO.ToUtilisateur());

            if(utilisateur is not null)
            {
                return CreatedAtAction(nameof(GetById), new { id = utilisateur.Id }, utilisateur);
            }

            return BadRequest();
        }
    }
}
