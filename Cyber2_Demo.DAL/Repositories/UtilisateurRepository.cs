using Cyber2_Demo.DAL.Data;
using Cyber2_Demo.DAL.Interfaces;
using Cyber2_Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber2_Demo.DAL.Repositories
{
    public class UtilisateurRepository : IUtilisateurRepository
    {
        public Utilisateur Create(Utilisateur utilisateur)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Utilisateur utilisateur)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Utilisateur> GetAll()
        {

            return FakeDB.utilisateurs;
        }

        public Utilisateur? GetById(int id)
        {
            return FakeDB.utilisateurs.SingleOrDefault(u => u.Id == id);
        }

        public Utilisateur Update(Utilisateur utilisateur)
        {
            throw new NotImplementedException();
        }
    }
}
