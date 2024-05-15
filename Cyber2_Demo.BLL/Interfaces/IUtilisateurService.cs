using Cyber2_Demo.Domain.Models;


namespace Cyber2_Demo.BLL.Interfaces
{
    public interface IUtilisateurService
    {
        public Utilisateur? Create(Utilisateur utilisateur);
        public Utilisateur? Update(Utilisateur utilisateur);
        public bool Delete(int id);
        public Utilisateur? GetById(int id);
        public IEnumerable<Utilisateur> GetAll();
    }
}
