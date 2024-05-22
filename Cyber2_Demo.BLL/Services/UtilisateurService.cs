using Cyber2_Demo.BLL.Interfaces;
using Cyber2_Demo.DAL.Interfaces;
using Cyber2_Demo.DAL.Repositories;
using Cyber2_Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber2_Demo.BLL.Services
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly IUtilisateurRepository _repository;

        public UtilisateurService(IUtilisateurRepository repository)
        {
            _repository = repository;
        }

        public Utilisateur? Create(Utilisateur utilisateur)
        {
            return _repository.Create(utilisateur);
        }

        public bool Delete(int id)
        {
            Utilisateur? utilisateurDeLaDB = _repository.GetById(id);
            if(utilisateurDeLaDB is not null )
            {
                return _repository.Delete(utilisateurDeLaDB);
            }

            return false;
        }

        public IEnumerable<Utilisateur> GetAll()
        {
            return _repository.GetAll();
        }

        public Utilisateur? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Utilisateur? Update(Utilisateur utilisateur)
        {

            Utilisateur? UtilisateurDeLaDB = _repository.GetById(utilisateur.Id);

            if(UtilisateurDeLaDB is not null)
            {
                UtilisateurDeLaDB.Email = utilisateur.Email;
                UtilisateurDeLaDB.Nom = utilisateur.Nom;
                UtilisateurDeLaDB.Prenom = utilisateur.Prenom;
                UtilisateurDeLaDB.Username = utilisateur.Username;

                return _repository.Update(UtilisateurDeLaDB);

            }

            return null;
        }
    }
}
