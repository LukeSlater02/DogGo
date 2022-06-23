using System.Collections.Generic;
namespace DogGo.Models.ViewModels
{
    public class CreateWalkViewModel
    {
        public Walk Walk { get; set; }
        public List<Dog> DogList { get; set; }
        public List<Walker> WalkerList { get; set; }
    }
}
