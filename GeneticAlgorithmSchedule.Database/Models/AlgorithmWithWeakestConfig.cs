using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmSchedule.Database.Models
{
    public class AlgorithmWithWeakestConfig : AlgorithmConfig
    {
        public int TrackWorst { get; set; }
    }
}
