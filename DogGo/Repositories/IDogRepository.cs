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
        void DeleteDog(int ownerId);
        List<Dog> GetDogsByOwnerId(int ownerId);
    }
}
