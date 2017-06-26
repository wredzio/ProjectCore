
using GeneticAlgorithmSchedule.Infrastructure.Abstract;
using GeneticAlgorithmSchedule.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GeneticAlgorithmSchedule.Infrastructure.Concrete
{
    public class GeneticAlgorithmSchedule : GeneticAlgorithm<Schedule>
    {
        public List<bool> BestFlags { get; set; }
        public int[] IndexesOfBestChromosomes { get; set; }
        public int CurrentBestSize { get; set; }

        private AlgorithmConfig _algorithmConfig;
        private School _school;
        private Random _rand;

        public GeneticAlgorithmSchedule(AlgorithmConfig algorithmConfig, School school)
        {
            _algorithmConfig = algorithmConfig;
            _school = school;
            _rand = new Random();

            BestFlags = Extensions.RepeatedDefault<bool>(_algorithmConfig.NumberOfChromosomes);
            IndexesOfBestChromosomes = new int[_algorithmConfig.TrackBest];
            CurrentBestSize = 0;
        }

        protected override Schedule GetBestChromosome(IEnumerable<Schedule> population)
        {
            return population.OrderBy(o => o.Fitness).LastOrDefault();
        }

        protected override IEnumerable<Schedule> InitializePopulation()
        {
            List<Schedule> population = new List<Schedule>();
            ClearBest();

            for (int i = 0; i < _algorithmConfig.NumberOfChromosomes; i++)
            {
                var schedule = new Schedule(_school, false, _rand);

                population.Add(schedule);
                AddToBest(i, population);
            }
            return population;
        }

        protected override bool IsEvaluationConditionSufficient(IEnumerable<Schedule> population)
        {
            return GetBestChromosome(population).Fitness >= 1;
        }

        protected override IEnumerable<Parents<Schedule>> SelectParents(IEnumerable<Schedule> population)
        {
            List<Parents<Schedule>> paterns = new List<Parents<Schedule>>();
            for (int i = 0; i < _algorithmConfig.ReplaceByGeneration; i++)
            {
                paterns.Add(new Parents<Schedule>()
                {
                    FirstParent = population.ElementAt(_rand.Next() % population.Count()),
                    SecondParent = population.ElementAt(_rand.Next() % population.Count())
                });
            }
            return paterns;
        }

        protected override IEnumerable<Schedule> Crossover(IEnumerable<Parents<Schedule>> parentsList)
        {
            parentsList = parentsList.ToList();

            List<Schedule> offsrping = new List<Schedule>();

            foreach (Parents<Schedule> parents in parentsList)
            {
                if (_rand.Next() % 100 > _algorithmConfig.CrosoverProbability)
                {
                    offsrping.Add(new Schedule(_school, false, _rand));
                    continue;
                }

                Schedule child = new Schedule(_school, true, _rand);

                int size = _school.CourseClasses.Count();

                List<bool> pointsToCrossover = Extensions.RepeatedDefault<bool>(size);

                for (int i = _algorithmConfig.NumberOfCrossoverPoints; i > 0; i--)
                {
                    while (true)
                    {
                        int crossoverPoint = _rand.Next() % size;
                        if (!pointsToCrossover[crossoverPoint])
                        {
                            pointsToCrossover[crossoverPoint] = true;
                            break;
                        }
                    }
                }

                bool first = _rand.Next() % 2 == 0;
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

                offsrping.Add(child);
            }
            return offsrping;
        }

        protected override IEnumerable<Schedule> Mutation(IEnumerable<Schedule> offsrping)
        {
            List<Schedule> offspringAfterMutation = new List<Schedule>();

            foreach (Schedule child in offsrping)
            {

                if (_rand.Next() % 100 > _algorithmConfig.MutationProbability)
                {
                    offspringAfterMutation.Add(child);
                    continue;
                }

                int numberOfClasses = child.Classes.Count;
                int numberOfSlots = child.Slots.Length;

                for (int i = _algorithmConfig.MutationSize; i > 0; i--)
                {
                    int randomClassIdToMove = _rand.Next() % numberOfClasses;
                    KeyValuePair<CourseClass, int> courseClassToMutation = child.Classes.ToList<KeyValuePair<CourseClass, int>>()[randomClassIdToMove];

                    int numberOfRooms = _school.Rooms.Count();
                    int duration = courseClassToMutation.Key.Duration;
                    int day = _rand.Next() % _school.NumberOfWorkDays;
                    int room = _rand.Next() % numberOfRooms;
                    int time = _rand.Next() % (_school.NumberOfHoursInDay + 1 - duration);
                    int position = day * numberOfRooms * _school.NumberOfWorkDays + room * _school.NumberOfHoursInDay + time;

                    for (int j = duration - 1; j >= 0; j--)
                    {
                        var courseClasses = child.Slots[courseClassToMutation.Value + j];

                        foreach (var courseClass in courseClasses)
                        {
                            if (courseClassToMutation.Value.Equals(courseClass))
                            {
                                courseClasses.Remove(courseClass);
                                break;
                            }
                        }

                        child.Slots[position + j].Add(courseClassToMutation.Key);
                    }
                    child.Classes[courseClassToMutation.Key] = position;
                }
                child.CalculateFitness();

                offspringAfterMutation.Add(child);
            }
            return offspringAfterMutation;
        }

        protected override IEnumerable<Schedule> CreateNewPopulation(IEnumerable<Schedule> offsrping, IEnumerable<Schedule> oldPopulation)
        {
            var newPopulation = oldPopulation.ToArray();
            offsrping = offsrping.ToArray();

            for (int j = 0; j < _algorithmConfig.ReplaceByGeneration; j++)
            {
                int ci;

                do
                {
                    ci = _rand.Next() % oldPopulation.Count();

                } while (IsInBest(ci));

                newPopulation[ci] = offsrping.ElementAt(j);

                AddToBest(ci, newPopulation);
            }

            float sum = 0;
            foreach (var ch in newPopulation)
            {
                sum = sum + ch.Fitness;
            }

            Console.WriteLine("Najlepszy : " + GetBestChromosome(newPopulation).Fitness + " Średnia populacji: " + sum / newPopulation.Count());

            return newPopulation;
        }

        private bool IsInBest(int chromosomeIndex)
        {
            return BestFlags[chromosomeIndex];
        }

        private void AddToBest(int chromosomeIndex, IEnumerable<Schedule> population)
        {
            Schedule[] Chromosomes = population.ToArray();

            if ((CurrentBestSize == IndexesOfBestChromosomes.Length &&
                Chromosomes[IndexesOfBestChromosomes[CurrentBestSize - 1]].Fitness >=
                Chromosomes[chromosomeIndex].Fitness) || BestFlags[chromosomeIndex])
                return;

            int i = CurrentBestSize;
            for (; i > 0; i--)
            {
                if (i < IndexesOfBestChromosomes.Length)
                {
                    if (Chromosomes[IndexesOfBestChromosomes[i - 1]].Fitness >
                        Chromosomes[chromosomeIndex].Fitness)
                        break;

                    IndexesOfBestChromosomes[i] = IndexesOfBestChromosomes[i - 1];
                }
                else
                    BestFlags[IndexesOfBestChromosomes[i - 1]] = false;
            }

            IndexesOfBestChromosomes[i] = chromosomeIndex;
            BestFlags[chromosomeIndex] = true;

            if (CurrentBestSize < IndexesOfBestChromosomes.Length)
                CurrentBestSize++;
        }

        private void ClearBest()
        {
            for (int i = (int)BestFlags.Count - 1; i >= 0; --i)
                BestFlags[i] = false;

            CurrentBestSize = 0;
        }
    }
}
