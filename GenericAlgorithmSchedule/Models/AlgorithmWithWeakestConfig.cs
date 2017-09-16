using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmSchedule.Models
{
    public class AlgorithmWithWeakestConfig : AlgorithmConfig
    {
        public int TrackWorst { get; set; }

        public string ToStringSelectionType()
        {
            return String.Format("SelectionType {0}", SelectionType.ToString());
        }

        public string ToStringNumberOfCrossoverPoints()
        {
            return String.Format("NumberOfCrossoverPoints {0}", NumberOfCrossoverPoints.ToString());
        }

        public string ToStringMutationSize()
        {
            return String.Format("MutationSize {0}", MutationSize.ToString());
        }

        public string ToStringMutationProbability()
        {
            return String.Format("MutationProbability {0}", MutationProbability.ToString());
        }

        public string ToStringNumberOfChromosomes()
        {
            return String.Format("NumberOfChromosomes {0}", NumberOfChromosomes.ToString());
        }

        public string ToStringReplaceByGeneration()
        {
            return String.Format("ReplaceByGeneration {0}", ReplaceByGeneration.ToString());
        }

        public string ToStringTrackBest()
        {
            return String.Format("TrackBest {0}", TrackBest.ToString());
        }

        public string ToStringCrosoverProbability()
        {
            return String.Format("CrosoverProbability {0}", CrosoverProbability.ToString());
        }

        public string ToStringTrackWorst()
        {
            return String.Format("TrackWorst {0}", TrackWorst.ToString());
        }
    }
}
