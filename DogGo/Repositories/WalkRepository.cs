using System;
using System.Collections.Generic;
using DogGo.Models;
using DogGo.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace DogGo.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public WalkRepository(IConfiguration config)
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

        public List<Walk> GetWalkByWalker(int walkerId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                SELECT Walks.Id, Date, Duration, WalkerId, DogId, Owner.Name OwnerName FROM Walks
                                JOIN Dog on Dog.Id = DogId
                                JOIN Owner on Dog.OwnerId = Owner.Id
                                WHERE Walks.Id = @id";

                    cmd.Parameters.AddWithValue("@id", walkerId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Walk> walks = new List<Walk>();
                        while (reader.Read())
                        {
                            Walk walk = new Walk()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")).ToShortDateString(),
                                Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                                WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                                DogId = reader.GetInt32(reader.GetOrdinal("DogId")),
                                OwnerName = reader.GetString(reader.GetOrdinal("OwnerName"))
                            };

                            walks.Add(walk);
                        }

                        return walks;
                    }
                }
            }
        }
    }
}
