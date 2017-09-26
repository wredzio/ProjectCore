
using GeneticAlgorithmSchedule.Infrastructure.Abstract;
using GeneticAlgorithmSchedule.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GeneticAlgorithmSchedule.Infrastructure.Selection;

namespace GeneticAlgorithmSchedule.Infrastructure.Concrete
{
    public class GeneticAlgorithmSchedule : GeneticAlgorithm<Schedule>
    {
        public List<bool> BestFlags { get; set; }
        public int[] IndexesOfBestChromosomes { get; set; }
        public int CurrentBestSize { get; set; }

        private readonly AlgorithmConfig _algorithmConfig;
        protected School School;
        protected Random Rand;

        public GeneticAlgorithmSchedule(AlgorithmConfig algorithmConfig, School school)
        {
            _algorithmConfig = algorithmConfig;
            School = school;
            Rand = new Random();

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
                var schedule = new Schedule(School, false, Rand);

                population.Add(schedule);
                AddToBest(i, population);
            }
            return population;
        }

        protected override bool IsEvaluationConditionSufficient(IEnumerable<Schedule> population)
        {
            if (CurrentGeneration > 2500)
                return true;


            return GetBestChromosome(population).Fitness >= 1 
                && (_algorithmConfig.IsSoftOn 
                        ? GetBestChromosome(population).FitnessSoft >= _algorithmConfig.SoftConditionSufficient : true);
        }

        protected override IEnumerable<Parents<Schedule>> SelectParents(IEnumerable<Schedule> population)
        {
            SelectionFactory selectionFactory = new SelectionFactory(_algorithmConfig.ReplaceByGeneration, Rand);
            GeneticSelection<Schedule> geneticSelection = selectionFactory.CreateSelector(_algorithmConfig.SelectionType);
            
            return geneticSelection.SelectParents(population);
        }

        protected override IEnumerable<Schedule> Crossover(IEnumerable<Parents<Schedule>> parentsList)
        {
            parentsList = parentsList.ToList();

            List<Schedule> offsrping = new List<Schedule>();

            foreach (Parents<Schedule> parents in parentsList)
            {
                if (Rand.Next() % 100 > _algorithmConfig.CrosoverProbability)
                {
                    offsrping.Add(new Schedule(School, false, Rand));
                    continue;
                }

                Schedule child = new Schedule(School, true, Rand);

                int size = School.CourseClasses.Count();

                List<bool> pointsToCrossover = Extensions.RepeatedDefault<bool>(size);

                for (int i = _algorithmConfig.NumberOfCrossoverPoints; i > 0; i--)
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

                offsrping.Add(child);
            }
            return offsrping;
        }

        protected override IEnumerable<Schedule> Mutation(IEnumerable<Schedule> offsrping)
        {
            List<Schedule> offspringAfterMutation = new List<Schedule>();

            foreach (Schedule child in offsrping)
            {

                if (Rand.Next() % 100 > _algorithmConfig.MutationProbability)
                {
                    offspringAfterMutation.Add(child);
                    continue;
                }

                int numberOfClasses = child.Classes.Count;
                int numberOfSlots = child.Slots.Length;

                for (int i = _algorithmConfig.MutationSize; i > 0; i--)
                {
                    int randomClassIdToMove = Rand.Next() % numberOfClasses;
                    KeyValuePair<CourseClass, int> courseClassToMutation = child.Classes.ToList<KeyValuePair<CourseClass, int>>()[randomClassIdToMove];

                    int numberOfRooms = School.Rooms.Count();
                    int duration = courseClassToMutation.Key.Duration;
                    int day = Rand.Next() % School.NumberOfWorkDays;
                    int room = Rand.Next() % numberOfRooms;
                    int time = Rand.Next() % (School.NumberOfHoursInDay + 1 - duration);
                    int position = day * numberOfRooms * School.NumberOfWorkDays + room * School.NumberOfHoursInDay + time;

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
                    ci = Rand.Next() % _algorithmConfig.NumberOfChromosomes;

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

            FitnessA.Add(sum / newPopulation.Count());
            FitnessBest.Add(GetBestChromosome(newPopulation).Fitness);
            FitnessWorst.Add(newPopulation.OrderBy(o => o.Fitness).FirstOrDefault().Fitness);

            return newPopulation;
        }

        protected bool IsInBest(int chromosomeIndex)
        {
            return BestFlags[chromosomeIndex];
        }

        protected void AddToBest(int chromosomeIndex, IEnumerable<Schedule> population)
        {
            Schedule[] chromosomes = population.ToArray();

            if ((CurrentBestSize == IndexesOfBestChromosomes.Length &&
                chromosomes[IndexesOfBestChromosomes[CurrentBestSize - 1]].Fitness >=
                chromosomes[chromosomeIndex].Fitness) && (!_algorithmConfig.IsSoftOn || chromosomes[IndexesOfBestChromosomes[CurrentBestSize - 1]].FitnessSoft >=
                                                          chromosomes[chromosomeIndex].FitnessSoft) || BestFlags[chromosomeIndex])
                return;

            int i = CurrentBestSize;
            for (; i > 0; i--)
            {
                if (i < IndexesOfBestChromosomes.Length)
                {
                    if (chromosomes[IndexesOfBestChromosomes[i - 1]].Fitness > chromosomes[chromosomeIndex].Fitness || chromosomes[IndexesOfBestChromosomes[i - 1]].FitnessSoft > chromosomes[chromosomeIndex].FitnessSoft)
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
