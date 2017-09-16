using GeneticAlgorithmSchedule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmSchedule.Infrastructure.Abstract;

namespace GeneticAlgorithmSchedule.Infrastructure.Selection
{
    internal class RankingSelection : GeneticSelection<Schedule>
    {
        public RankingSelection(int replaceByGeneration, Random rand) : base(replaceByGeneration, rand) { }

        public override IEnumerable<Parents<Schedule>> SelectParents(IEnumerable<Schedule> population)
        {
            var rankPopulation = population.OrderBy(o => o.Fitness).Reverse().Take(population.Count() / 2);

            List<Parents<Schedule>> paterns = new List<Parents<Schedule>>();
            for (int i = 0; i < _replaceByGeneration; i++)
            {
                paterns.Add(new Parents<Schedule>()
                {
                    FirstParent = rankPopulation.ElementAt(_rand.Next() % rankPopulation.Count()),
                    SecondParent = rankPopulation.ElementAt(_rand.Next() % rankPopulation.Count())
                });
            }
            return paterns;
        }
    }
}
