using Cyber2_Demo.DAL.Data;
using Cyber2_Demo.DAL.Interfaces;
using Cyber2_Demo.Domain.Models;


namespace Cyber2_Demo.DAL.Repositories
{
    public class UtilisateurRepository : IUtilisateurRepository
    {
        public Utilisateur Create(Utilisateur utilisateur)
        {
            utilisateur.Id = ++FakeDB.Compteur;
            FakeDB.utilisateurs.Add(utilisateur);

            return utilisateur;
        }

        public bool Delete(Utilisateur utilisateur)
        {
            return FakeDB.utilisateurs.Remove(utilisateur);
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

            // Inutile
            int position = FakeDB.utilisateurs.IndexOf(utilisateur);
            FakeDB.utilisateurs[position] = utilisateur;

            return utilisateur;
        }
    }
}
