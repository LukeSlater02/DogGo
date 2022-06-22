using DogGo.Models;
using System.Collections.Generic;
namespace DogGo.Repositories
{
    public interface IWalkRepository
    {
        List<Walk> GetWalkByWalker(int walkerId);
    }
}
