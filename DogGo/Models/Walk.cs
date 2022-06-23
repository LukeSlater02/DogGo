using System;
using System.ComponentModel;

namespace DogGo.Models
{
    public class Walk
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int Duration { get; set; }

        [DisplayName("Walker")]
        public int WalkerId { get; set; }

        [DisplayName("Dog")]
        public int DogId { get; set; }
        public string OwnerName { get; set; }

    }
}
