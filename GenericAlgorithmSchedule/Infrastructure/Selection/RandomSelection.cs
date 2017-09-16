using GeneticAlgorithmSchedule.Models;
using System;
using System.Collections.Generic;
using System.Text;
using GeneticAlgorithmSchedule.Infrastructure.Abstract;
using System.Linq;

namespace GeneticAlgorithmSchedule.Infrastructure.Selection
{
    internal class RandomSelection : GeneticSelection<Schedule>
    {
        public RandomSelection(int replaceByGeneration, Random rand) 
            : base(replaceByGeneration, rand) { }

        public override IEnumerable<Parents<Schedule>> SelectParents(IEnumerable<Schedule> population)
        {
            List<Parents<Schedule>> paterns = new List<Parents<Schedule>>();
            for (int i = 0; i < _replaceByGeneration; i++)
            {
                paterns.Add(new Parents<Schedule>()
                {
                    FirstParent = population.ElementAt(_rand.Next() % population.Count()),
                    SecondParent = population.ElementAt(_rand.Next() % population.Count())
                });
            }
            return paterns;
        }
    }
}
