using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmSchedule.Infrastructure.Abstract
{
    public interface IChromosome
    {
        float Fitness { get; set; }
        void CalculateFitness();
    }
}
