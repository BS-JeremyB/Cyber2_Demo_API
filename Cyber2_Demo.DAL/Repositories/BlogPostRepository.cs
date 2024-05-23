using Cyber2_Demo.DAL.Interfaces;
using Cyber2_Demo.Domain.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Cyber2_Demo.DAL.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {

        private readonly string _connectionString =
           @"Server=(localdb)\BStormLocalDB;Database=DEMO_API;Trusted_Connection=True;";

        public BlogPost? Create(BlogPost blogPost)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using(SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO BlogPost OUTPUT INSERTED.Id VALUES (@Titre, @Contenu, @Utilisateur_Id)";
                        command.CommandType = CommandType.Text;

                        command.Parameters.AddWithValue("@Titre" , blogPost.Titre);
                        command.Parameters.AddWithValue("@Contenu" , blogPost.Contenu);
                        command.Parameters.AddWithValue("@Utilisateur_Id" , blogPost.Auteur.Id);

                        connection.Open();
                        blogPost.Id = (int)command.ExecuteScalar();

                        return blogPost;
                        
                    }

                }

            }catch(Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite lors de la création du post : {ex.Message}");
                return null;
            }
        }

        public BlogPost Update(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }

        public bool Delete(BlogPost blogPost)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "DELETE FROM BlogPost WHERE Id = @Id";
                        command.CommandType = CommandType.Text;

                        command.Parameters.AddWithValue("@Id", blogPost.Id);

                        connection.Open();
                        int nbrLigne = (int)command.ExecuteNonQuery();

                        return nbrLigne > 0;
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public BlogPost? GetById(int id)
        {
            try
            {
                using(SqlConnection connection=new SqlConnection(_connectionString))
                {
                    using(SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT B.*, U.Username, U.Nom, U.Prenom, U.Email FROM BlogPost B JOIN Utilisateur U ON B.Utilisateur_Id = U.Id WHERE B.Id = @Id";
                        command.CommandType = CommandType.Text;

                        command.Parameters.AddWithValue("@Id", id);

                        connection.Open();
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new BlogPost
                                {
                                    Id = (int)reader["Id"],
                                    Titre = reader["Titre"].ToString(),
                                    Contenu = reader["Contenu"].ToString(),
                                    Auteur = new Utilisateur
                                    {
                                        Id = Convert.ToInt32(reader["Utilisateur_Id"]),
                                        Username = Convert.ToString(reader["Username"]),
                                        Nom = Convert.ToString(reader["Nom"]),
                                        Prenom = Convert.ToString(reader["Prenom"]),
                                        Email = Convert.ToString(reader["Email"]),
                                    }

                                };
                            }
                            return null;
                        }
                    }
                }
            }catch (Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite lors de la création du post : {ex.Message}");
                return null;
            }
        }

        public IEnumerable<BlogPost> GetAll()
        {
            List<BlogPost> posts = new List<BlogPost>();
            try
            {
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using(SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT B.*, U.Username, U.Nom, U.Prenom, U.Email FROM BlogPost B JOIN Utilisateur U ON B.Utilisateur_Id = U.Id";
                        command.CommandType = CommandType.Text;

                        connection.Open();
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                posts.Add(new BlogPost
                                {
                                    Id = (int)reader["Id"],
                                    Titre = reader["Titre"].ToString(),
                                    Contenu = reader["Contenu"].ToString(),
                                    Auteur = new Utilisateur
                                    {
                                        Id = Convert.ToInt32(reader["Utilisateur_Id"]),
                                        Username = Convert.ToString(reader["Username"]),
                                        Nom = Convert.ToString(reader["Nom"]),
                                        Prenom = Convert.ToString(reader["Prenom"]),
                                        Email = Convert.ToString(reader["Email"]),
                                    }
                                });
                            }

                            return posts;
                        }
                    }
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return posts;
            }
        }
    }
}
