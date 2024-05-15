﻿using Cyber2_Demo.API.Context.Mapper;
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


        //[HttpPut("{id:int}")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Utilisateur))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(int))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public ActionResult<Utilisateur> Update(int id, Utilisateur user)
        //{

        //    List<Utilisateur> utilisateurs = FakeDB.utilisateurs;
        //    Utilisateur? utilisateur = utilisateurs.SingleOrDefault(u => u.Id == id);
        //    if(utilisateur is null)
        //    {
        //        return NotFound(id);
        //    }
        //    try
        //    {
        //        int pos = utilisateurs.IndexOf(utilisateur);
        //        utilisateur.Email = user.Email;
        //        utilisateur.Prenom = user.Prenom;
        //        utilisateur.Nom = user.Nom;

        //        utilisateurs[pos] = utilisateur;


        //    }catch(Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //    return Ok(utilisateur);

        //}

        //[HttpDelete]
        //[Route("{id:int}")] // Annotation alternative pour indiquer la route
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(int))]
        //public ActionResult Delete(int id)
        //{
        //    Utilisateur? utilisateur = FakeDB.utilisateurs.SingleOrDefault(u => u.Id == id);
        //    if(utilisateur is not null )
        //    {
        //        FakeDB.utilisateurs.Remove(utilisateur);
        //        return NoContent();
        //    }

        //    return NotFound(id);

        //}

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Utilisateur))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]

        //public ActionResult<Utilisateur> Create(CreateUtilisateurDTO utilisateurDTO)
        //{
        //    try
        //    {
        //        Utilisateur utilisateur = utilisateurDTO.ToUtilisateur();
        //        utilisateur.Id = ++FakeDB.Compteur;
        //        FakeDB.utilisateurs.Add(utilisateur);

        //        return CreatedAtAction(nameof(GetOne), new { id = utilisateur.Id }, utilisateur);
        //        //return Created($"api/Utilisateur/{utilisateur.Id}", utilisateur);
        //    }catch(Exception ex)
        //    {
        //        return BadRequest(ex.Message);   
        //    }




        //}
    }
}
