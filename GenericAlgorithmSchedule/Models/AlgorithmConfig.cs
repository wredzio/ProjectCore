
using GeneticAlgorithmSchedule.Infrastructure.Selection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmSchedule.Models
{
    public class AlgorithmConfig : BaseEntity
    {
        public int NumberOfCrossoverPoints { get; set; }
        public int MutationSize { get; set; }
        public int CrosoverProbability { get; set; }
        public int MutationProbability { get; set; }
        public int NumberOfChromosomes { get; set; }
        public int ReplaceByGeneration { get; set; }
        public int TrackBest { get; set; }
        public SelectionType SelectionType { get; set; }
        public double SoftConditionSufficient { get; set; }
        public bool IsSoftOn { get => SoftConditionSufficient > 0; private set { } }
    }
}
