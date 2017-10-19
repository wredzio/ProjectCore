
using GeneticAlgorithmSchedule.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmSchedule.Database.Models.Schools
{
    public class Room : BaseEntity
    {
        public string Name { get; set; }
        public bool Lab { get; set; }
        public int NumberOfSeats { get; set; }
    }
}

