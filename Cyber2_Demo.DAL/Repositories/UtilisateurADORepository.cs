using Cyber2_Demo.DAL.Interfaces;
using Cyber2_Demo.Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Cyber2_Demo.DAL.Repositories
{
    public class UtilisateurADORepository : IUtilisateurRepository
    {

        private readonly string _connectionString =
           @"Server=(localdb)\BStormLocalDB;Database=DEMO_API;Trusted_Connection=True;";


        public Utilisateur Create(Utilisateur utilisateur)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                using(SqlCommand command = connection.CreateCommand()) 
                {
                    command.CommandText = "CreationUtilisateur";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Username", utilisateur.Username);
                    command.Parameters.AddWithValue("@Nom", utilisateur.Nom);
                    command.Parameters.AddWithValue("@Prenom", utilisateur.Prenom);
                    command.Parameters.AddWithValue("@Email", utilisateur.Email);
                    command.Parameters.AddWithValue("@Password", utilisateur.Password);

                    connection.Open();
                    utilisateur.Id = Convert.ToInt32(command.ExecuteScalar());


                    return utilisateur.Id == -1 ? null : utilisateur;
                }
            }
        }

        public bool Delete(Utilisateur utilisateur)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Utilisateur> GetAll()
        {
            throw new NotImplementedException();
        }

        public Utilisateur? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Utilisateur Update(Utilisateur utilisateur)
        {
            throw new NotImplementedException();
        }
    }
}
