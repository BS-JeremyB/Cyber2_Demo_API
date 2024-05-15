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
            throw new NotImplementedException();
        }

        public bool Delete(Utilisateur utilisateur)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
