﻿using System;
namespace DogGo.Models
{
    public class Walk
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int Duration { get; set; }
        public int WalkerId { get; set; }
        public int DogId { get; set; }
        public string OwnerName { get; set; }

    }
}
