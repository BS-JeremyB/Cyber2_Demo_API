using Cyber2_Demo.Domain.Models;

namespace Cyber2_Demo.DAL.Data
{
    public class FakeDB
    {

        public static int Compteur = 10;


        public static List<Utilisateur> utilisateurs = new List<Utilisateur>
        {
            new Utilisateur
            {
                Id = 1,
                Nom = "Doe",
                Prenom = "John",
                Email = "johndoe1@email.be"
            },
            new Utilisateur
            {
                Id = 2,
                Nom = "Smith",
                Prenom = "Jane",
                Email = "janesmith2@email.be"
            },
            new Utilisateur
            {
                Id = 3,
                Nom = "Brown",
                Prenom = "Chris",
                Email = "chrisbrown3@email.be"
            },
            new Utilisateur
            {
                Id = 4,
                Nom = "Wilson",
                Prenom = "Mike",
                Email = "mikewilson4@email.be"
            },
            new Utilisateur
            {
                Id = 5,
                Nom = "Taylor",
                Prenom = "Emma",
                Email = "emmataylor5@email.be"
            },
            new Utilisateur
            {
                Id = 6,
                Nom = "Anderson",
                Prenom = "Julia",
                Email = "juliaanderson6@email.be"
            },
            new Utilisateur
            {
                Id = 7,
                Nom = "Thomas",
                Prenom = "David",
                Email = "davidthomas7@email.be"
            },
            new Utilisateur
            {
                Id = 8,
                Nom = "Jackson",
                Prenom = "Alice",
                Email = "alicejackson8@email.be"
            },
            new Utilisateur
            {
                Id = 9,
                Nom = "White",
                Prenom = "Bob",
                Email = "bobwhite9@email.be"
            },
            new Utilisateur
            {
                Id = 10,
                Nom = "Harris",
                Prenom = "Linda",
                Email = "lindaharris10@email.be"
            }
        };
    }
}
