using DogGo.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public interface IDogRepository
    {
        List<Dog> GetAllDogs();

        Dog GetDogById(int id);
        void AddDog(Dog owner);
        void UpdateDog(Dog owner);
        Dog GetDogByEmail(string email);
        void DeleteDog(int ownerId);
    }
}
