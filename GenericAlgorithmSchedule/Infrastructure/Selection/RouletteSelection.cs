using GeneticAlgorithmSchedule.Infrastructure.Abstract;
using GeneticAlgorithmSchedule.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithmSchedule.Infrastructure.Selection
{
    internal class RouletteSelection : GeneticSelection<Schedule>
    {
        public RouletteSelection(int replaceByGeneration, Random rand) : base(replaceByGeneration, rand) { }

        public override IEnumerable<Parents<Schedule>> SelectParents(IEnumerable<Schedule> population)
        {
            var sum = population.Sum(o => o.Fitness);

            var ruoletteWheelElements = population.Select((o,i) => new { Distribution = o.Fitness * 100 / sum, Index = i });

            List<Parents<Schedule>> paterns = new List<Parents<Schedule>>();
            for (int i = 0; i < _replaceByGeneration; i++)
            {
                List<Schedule> rouletteWiners = new List<Schedule>();

                for (int j = 0; j < 2; j++)
                {
                    float randomNumber = _rand.Next() % sum / 100;
                    float sumElement = 0;
                    foreach (var ruoletteWheelElement in ruoletteWheelElements)
                    {
                        sumElement = sumElement + ruoletteWheelElement.Distribution;
                        if (randomNumber - sumElement < 0)
                        {
                            rouletteWiners.Add(population.ElementAt(ruoletteWheelElement.Index));
                            break;
                        }

                    }
                }

                paterns.Add(new Parents<Schedule>()
                {
                    FirstParent = rouletteWiners[0],
                    SecondParent = rouletteWiners[1]
                });
            }
            return paterns;
        }
    }
}
