using Cyber2_Demo.DAL.Interfaces;
using Cyber2_Demo.Domain.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber2_Demo.DAL.Repositories
{
    public class UtilisateurADORepository : IUtilisateurRepository
    {
        private readonly string _connectionString =
           @"Server=(localdb)\BStormLocalDB;Database=DEMO_API;Trusted_Connection=True;";

        public Utilisateur Create(Utilisateur utilisateur)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "CreationUtilisateur";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Username", utilisateur.Username);
                    command.Parameters.AddWithValue("@Nom", utilisateur.Nom);
                    command.Parameters.AddWithValue("@Prenom", utilisateur.Prenom);
                    command.Parameters.AddWithValue("@Email", utilisateur.Email);
                    command.Parameters.AddWithValue("@Password", utilisateur.Password);

                    connection.Open();
                    var result = command.ExecuteScalar();

                    if (result is int id && id > 0)
                    {
                        utilisateur.Id = id;
                        return utilisateur;
                    }
                    else
                    {
                        return null; // Indicates failure to insert the user
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error occurred: {ex.Message}");
                return null;
            }
        }

        public bool Delete(Utilisateur utilisateur)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "DELETE FROM Utilisateur WHERE Id = @Id";
                        command.CommandType = CommandType.Text;

                        command.Parameters.AddWithValue("@Id", utilisateur.Id);

                        connection.Open();
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                return false;
            }
        }

        public IEnumerable<Utilisateur> GetAll()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM Utilisateur";
                        command.CommandType = CommandType.Text;

                        List<Utilisateur> utilisateurs = new List<Utilisateur>();
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
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
                                });
                            }
                        }
                        return utilisateurs;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while retrieving users: {ex.Message}");
                return new List<Utilisateur>(); // Return an empty list on error
            }
        }

        public Utilisateur? GetById(int id)
        {
            try
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
                                    Nom = Convert.ToString(reader["Nom"]),
                                    Prenom = Convert.ToString(reader["Prenom"]),
                                    Email = Convert.ToString(reader["Email"]),
                                };
                            }
                        }
                        return utilisateur;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while retrieving user with ID {id}: {ex.Message}");
                return null;
            }
        }

        public Utilisateur Update(Utilisateur utilisateur)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.CommandText = "UPDATE Utilisateur SET Nom = @Nom, Prenom = @Prenom, Email = @Email, Username = @Username WHERE Id = @Id";
                            command.CommandType = CommandType.Text;
                            command.Transaction = transaction;

                            command.Parameters.AddWithValue("@Nom", utilisateur.Nom);
                            command.Parameters.AddWithValue("@Prenom", utilisateur.Prenom);
                            command.Parameters.AddWithValue("@Email", utilisateur.Email);
                            command.Parameters.AddWithValue("@Username", utilisateur.Username);
                            command.Parameters.AddWithValue("@Id", utilisateur.Id);

                            if (command.ExecuteNonQuery() == 1)
                            {
                                transaction.Commit();
                                return utilisateur;
                            }
                            else
                            {
                                transaction.Rollback();
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while updating user: {ex.Message}");
                return null;
            }
        }

        public bool Exist(Utilisateur utilisateur)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT COUNT(Id) FROM Utilisateur WHERE (Username LIKE @Username OR Email LIKE @Email) AND Id NOT LIKE @Id";
                        command.CommandType = CommandType.Text;

                        command.Parameters.AddWithValue("@Id", utilisateur.Id);
                        command.Parameters.AddWithValue("@Username", utilisateur.Username);
                        command.Parameters.AddWithValue("@Email", utilisateur.Email);

                        connection.Open();
                        int nbrOccurrence = (int)command.ExecuteScalar();

                        return nbrOccurrence > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while checking existence: {ex.Message}");
                return false;
            }
        }
    }
}
