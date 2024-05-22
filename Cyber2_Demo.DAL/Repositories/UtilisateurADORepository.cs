using Cyber2_Demo.DAL.Interfaces;
using Cyber2_Demo.Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                using(SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Utilisateur WHERE Id = @Id";
                    command.CommandType = CommandType.Text;

                    command.Parameters.AddWithValue("@Id", utilisateur.Id);

                    connection.Open();
                    int nbrLigne = command.ExecuteNonQuery();

                    return nbrLigne > 0 ? true : false;
                }
            }
        }

        public IEnumerable<Utilisateur> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Utilisateur";
                    command.CommandType = CommandType.Text;

                    List<Utilisateur> utilisateurs = new List<Utilisateur>();

                    connection.Open();

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            utilisateurs.Add(new Utilisateur
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Username = Convert.ToString(reader["Username"]),
                                Nom = Convert.ToString(reader["Nom"]),
                                Prenom = Convert.ToString(reader["Prenom"]),
                                Email = Convert.ToString(reader["Email"]),
                                Salt = Convert.ToString(reader["Salt"])
                            });
                        }
                    }

                    return utilisateurs;
                }
            }
        }

        public Utilisateur? GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Utilisateur WHERE Id = @Id";
                    command.CommandType = CommandType.Text;

                    
                    command.Parameters.AddWithValue("@Id", id);

                    Utilisateur? utilisateur = null;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            utilisateur = new Utilisateur
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Username = (reader["Username"] is DBNull) ? null : Convert.ToString(reader["Username"]),
                                //Username = Convert.ToString(reader["Username"]),
                                Nom = Convert.ToString(reader["Nom"]),
                                Prenom = Convert.ToString(reader["Prenom"]),
                                Email = Convert.ToString(reader["Email"]),
                                Salt = Convert.ToString(reader["Salt"])
                            };
                        }
                    }

                    return utilisateur;
                }
            }
        }

        public Utilisateur Update(Utilisateur utilisateur)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using(SqlTransaction transaction = connection.BeginTransaction())
                {

                    using(SqlCommand command = connection.CreateCommand())
                    {

                        command.CommandText = "UPDATE Utilisateur SET Nom = @Nom, Prenom = @Prenom, Email = @Email, Username = @Username WHERE Id = @Id";
                        command.CommandType = CommandType.Text;
                        command.Transaction = transaction;

                        command.Parameters.AddWithValue("@Nom", utilisateur.Nom);
                        command.Parameters.AddWithValue("@Prenom", utilisateur.Prenom);
                        command.Parameters.AddWithValue("@Email", utilisateur.Email);
                        command.Parameters.AddWithValue("@Username", utilisateur.Username);
                        command.Parameters.AddWithValue("@Id", utilisateur.Id);
                        int nbrLigne = 0;
                        try
                        {
                            nbrLigne = command.ExecuteNonQuery();
                            if(nbrLigne > 1)
                            {
                                throw new InvalidOperationException();
                            }
                            else
                            {
                                transaction.Commit();
                            }

                        }catch(Exception e)
                        {
                            transaction.Rollback();
                        }

                        return nbrLigne == 1 ? utilisateur : null;

                        

                        
                    }
                }

                
            }
        }
    }
}
