using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public class DogRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public DogRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        public List<Dog> GetAllDogs()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, [Name], Notes, OwnerId, Breed, ImageUrl 
                        FROM Dog
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Dog> owners = new List<Dog>();
                        while (reader.Read())
                        {
                            Dog owner = new Dog
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Notes = reader.GetString(reader.GetOrdinal("Notes")),
                                OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                                ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl"))
                            };

                            owners.Add(owner);
                        }

                        return owners;
                    }
                }
            }
        }

        public Dog GetDogById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, [Name], Notes, Breed, ImageUrl, OwnerId
                        FROM Dog
                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Dog owner = new Dog()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Notes = reader.GetString(reader.GetOrdinal("Notes")),
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                                ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                                OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId"))
                            };

                            return owner;
                        }

                        return null;
                    }
                }
            }
        }

        public Dog GetDogByNotes(string email)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, [Name], Notes, Breed, ImageUrl, OwnerId
                        FROM Dog
                        WHERE Notes = @email";

                    cmd.Parameters.AddWithValue("@email", email);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Dog owner = new Dog()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Notes = reader.GetString(reader.GetOrdinal("Notes")),
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                                ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                                OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId"))
                            };

                            return owner;
                        }

                        return null;
                    }
                }
            }
        }

        public void AddDog(Dog owner)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Dog ([Name], Notes, ImageUrl, Breed, OwnerId)
                    OUTPUT INSERTED.ID
                    VALUES (@name, @email, @phoneNumber, @address, @neighborhoodId);
                ";

                    cmd.Parameters.AddWithValue("@name", owner.Name);
                    cmd.Parameters.AddWithValue("@email", owner.Notes);
                    cmd.Parameters.AddWithValue("@phoneNumber", owner.ImageUrl);
                    cmd.Parameters.AddWithValue("@address", owner.Breed);
                    cmd.Parameters.AddWithValue("@neighborhoodId", owner.OwnerId);

                    int id = (int)cmd.ExecuteScalar();

                    owner.Id = id;
                }
            }
        }

        public void UpdateDog(Dog owner)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Dog
                            SET 
                                [Name] = @name, 
                                Notes = @email, 
                                Breed = @address, 
                                ImageUrl = @phone, 
                                OwnerId = @neighborhoodId
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@name", owner.Name);
                    cmd.Parameters.AddWithValue("@email", owner.Notes);
                    cmd.Parameters.AddWithValue("@address", owner.Breed);
                    cmd.Parameters.AddWithValue("@phone", owner.ImageUrl);
                    cmd.Parameters.AddWithValue("@neighborhoodId", owner.OwnerId);
                    cmd.Parameters.AddWithValue("@id", owner.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        public void DeleteDog(int ownerId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Dog
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", ownerId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

