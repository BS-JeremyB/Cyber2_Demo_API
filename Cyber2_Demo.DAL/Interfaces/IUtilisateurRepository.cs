using Cyber2_Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber2_Demo.DAL.Interfaces
{
    public interface IUtilisateurRepository
    {

        public Utilisateur? Create(Utilisateur utilisateur);
        public Utilisateur? Update(Utilisateur utilisateur);
        public bool Delete(Utilisateur utilisateur);
        public Utilisateur? GetById(int id);
        public IEnumerable<Utilisateur> GetAll();
        public bool Exist(Utilisateur utilisateur);
    }
}
