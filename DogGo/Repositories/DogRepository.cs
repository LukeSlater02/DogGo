using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
namespace DogGo.Repositories
{
    public class DogRepository : IDogRepository
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
                        SELECT Id, [Name] as DogName, Notes, OwnerId, Breed, ImageUrl 
                        FROM Dog
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Dog> dogs = new List<Dog>();
                        while (reader.Read())
                        {
                            Dog dog = new Dog
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("DogName")),
                                Notes = !reader.IsDBNull(reader.GetOrdinal("Notes")) ? reader.GetString(reader.GetOrdinal("Notes")) : "",
                                OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                                ImageUrl = !reader.IsDBNull(reader.GetOrdinal("ImageUrl")) ? reader.GetString(reader.GetOrdinal("ImageUrl")) : ""
                            };

                            dogs.Add(dog);
                        }

                        return dogs;
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
                            Dog dog = new Dog()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Notes = reader.GetString(reader.GetOrdinal("Notes")),
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                                ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                                OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId"))
                            };

                            return dog;
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
                            Dog dog = new Dog()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Notes = reader.GetString(reader.GetOrdinal("Notes")),
                                Breed = reader.GetString(reader.GetOrdinal("Breed")),
                                ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                                OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId"))
                            };

                            return dog;
                        }

                        return null;
                    }
                }
            }
        }

        public void AddDog(Dog dog)
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

                    cmd.Parameters.AddWithValue("@name", dog.Name);
                    cmd.Parameters.AddWithValue("@email", dog.Notes);
                    cmd.Parameters.AddWithValue("@phoneNumber", dog.ImageUrl);
                    cmd.Parameters.AddWithValue("@address", dog.Breed);
                    cmd.Parameters.AddWithValue("@neighborhoodId", dog.OwnerId);

                    int id = (int)cmd.ExecuteScalar();

                    dog.Id = id;
                }
            }
        }

        public void UpdateDog(Dog dog)
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

                    cmd.Parameters.AddWithValue("@name", dog.Name);
                    cmd.Parameters.AddWithValue("@email", dog.Notes);
                    cmd.Parameters.AddWithValue("@address", dog.Breed);
                    cmd.Parameters.AddWithValue("@phone", dog.ImageUrl);
                    cmd.Parameters.AddWithValue("@neighborhoodId", dog.OwnerId);
                    cmd.Parameters.AddWithValue("@id", dog.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        public void DeleteDog(int dogId)
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

                    cmd.Parameters.AddWithValue("@id", dogId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

