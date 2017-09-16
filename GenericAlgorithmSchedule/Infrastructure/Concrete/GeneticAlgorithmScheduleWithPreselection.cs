using System;
using System.Collections.Generic;
using System.Text;
using GeneticAlgorithmSchedule.Models;
using System.Linq;
using GeneticAlgorithmSchedule.Infrastructure.Abstract;
using GeneticAlgorithmSchedule.Utils;

namespace GeneticAlgorithmSchedule.Infrastructure.Concrete
{
    public class GeneticAlgorithmScheduleWithWeakest : GeneticAlgorithmSchedule
    {
        private List<Schedule> _worstChromosomes;
        private readonly AlgorithmWithWeakestConfig _algorithmExtendedConfig;
        private int _replaceByGeneration;

        public GeneticAlgorithmScheduleWithWeakest(AlgorithmWithWeakestConfig algorithmExtendedConfig, School school)
            : base(algorithmExtendedConfig, school)
        {
            _worstChromosomes = new List<Schedule>();
            _algorithmExtendedConfig = algorithmExtendedConfig;
            _replaceByGeneration = _algorithmExtendedConfig.ReplaceByGeneration;
        }

        protected override IEnumerable<Schedule> InitializePopulation()
        {
            var population = base.InitializePopulation();

            AddToWorst(population);

            return population;

        }

        protected override IEnumerable<Schedule> Crossover(IEnumerable<Parents<Schedule>> parentsList)
        {
            parentsList = parentsList.ToList();

            List<Schedule> offsrping = new List<Schedule>();

            foreach (Parents<Schedule> parents in parentsList)
            {
                if (Rand.Next() % 100 > _algorithmExtendedConfig.CrosoverProbability)
                {
                    offsrping.Add(new Schedule(School, false, Rand));
                    continue;
                }

                Schedule child = new Schedule(School, true, Rand);

                int size = School.CourseClasses.Count();

                List<bool> pointsToCrossover = Extensions.RepeatedDefault<bool>(size);

                for (int i = _algorithmExtendedConfig.NumberOfCrossoverPoints; i > 0; i--)
                {
                    while (true)
                    {
                        int crossoverPoint = Rand.Next() % size;
                        if (!pointsToCrossover[crossoverPoint])
                        {
                            pointsToCrossover[crossoverPoint] = true;
                            break;
                        }
                    }
                }

                bool first = Rand.Next() % 2 == 0;
                for (int i = 0; i < size; i++)
                {
                    if (first)
                    {
                        var firstParentCourseClass = parents.FirstParent.Classes.ElementAt(i);

                        child.Classes.Add(firstParentCourseClass.Key, firstParentCourseClass.Value);

                        for (int j = firstParentCourseClass.Key.Duration - 1; j >= 0; j--)
                        {
                            child.Slots[firstParentCourseClass.Value + j].Add(firstParentCourseClass.Key);
                        }
                    }
                    else
                    {
                        var secondParentCourseClass = parents.SecondParent.Classes.ElementAt(i);

                        child.Classes.Add(secondParentCourseClass.Key, secondParentCourseClass.Value);

                        for (int j = secondParentCourseClass.Key.Duration - 1; j >= 0; j--)
                        {
                            child.Slots[secondParentCourseClass.Value + j].Add(secondParentCourseClass.Key);
                        }
                    }

                    if (pointsToCrossover[i])
                    {
                        first = !first;
                    }
                }
                child.CalculateFitness();

                if (_worstChromosomes != null && child.Fitness < _worstChromosomes.FirstOrDefault().Fitness)
                {
                    _replaceByGeneration--;
                    _worstChromosomes.Add(child);
                    continue;
                }

                offsrping.Add(child);
            }
            return offsrping;
        }

        protected override IEnumerable<Schedule> CreateNewPopulation(IEnumerable<Schedule> offsrping, IEnumerable<Schedule> oldPopulation)
        {
            var newPopulation = oldPopulation.ToArray();
            offsrping = offsrping.ToArray();

            for (int j = 0; j < _replaceByGeneration; j++)
            {
                int ci;

                do
                {
                    ci = Rand.Next() % oldPopulation.Count();

                } while (IsInBest(ci));

                Schedule child = offsrping.ElementAt(j);

                newPopulation[ci] = child;

                AddToBest(ci, newPopulation);
            }

            float sum = 0;
            foreach (var ch in newPopulation)
            {
                sum = sum + ch.Fitness;
            }

            Console.WriteLine("Najlepszy : " + GetBestChromosome(newPopulation).Fitness + " Średnia populacji: " + sum / newPopulation.Count());
            Console.WriteLine("Najlepszy soft : " + GetBestChromosome(newPopulation).FitnessSoft + " Generation :" + CurrentGeneration);
            FitnessA.Add(sum / newPopulation.Count());
            FitnessBest.Add(GetBestChromosome(newPopulation).Fitness);
            FitnessWorst.Add(newPopulation.OrderBy(o => o.Fitness).FirstOrDefault().Fitness);

            AddToWorst(newPopulation);

            _replaceByGeneration = _algorithmExtendedConfig.ReplaceByGeneration;

            return newPopulation;
        }

        private void AddToWorst(IEnumerable<Schedule> population)
        {
            var worsts = population
                            .OrderBy(o => o.Fitness)
                            .Take(_algorithmExtendedConfig.TrackWorst);

            if (_worstChromosomes.Count == 0)
            {
                _worstChromosomes.AddRange(worsts);
            }

            foreach (var worst in worsts)
            {
                if (_worstChromosomes.All(o => !o.Equals(worst)))
                {
                    _worstChromosomes.Add(worst);
                }
            }

            _worstChromosomes = _worstChromosomes
                                    .OrderBy(o => o.Fitness)
                                    .Reverse()
                                    .Take(_algorithmExtendedConfig.TrackWorst)
                                    .ToList();
        }
    }
}
