using GeneticAlgorithmSchedule.Infrastructure.Abstract;
using GeneticAlgorithmSchedule.Infrastructure.Selection;
using GeneticAlgorithmSchedule.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProjectCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Config.Init();

            School school = new School();

            school.CourseClasses = Config.CourseClasses;
            school.Courses = Config.Courses;
            school.Teachers = Config.Professors;
            school.Rooms = Config.Rooms;
            school.StudentsGroup = Config.StudentsGroups;
            school.NumberOfHoursInDay = 12;
            school.NumberOfWorkDays = 5;

            AlgorithmConfig algorithmConfig = new AlgorithmConfig()
            {
                CrosoverProbability = 90,
                MutationProbability = 5,
                MutationSize = 2,
                NumberOfChromosomes = 1000,
                NumberOfCrossoverPoints = 2,
                ReplaceByGeneration = 1000,
                TrackBest = 300,
                SelectionType = SelectionType.Random
            };

            AlgorithmWithWeakestConfig algorithmExtendedConfig1 = new AlgorithmWithWeakestConfig()
            {
                CrosoverProbability = 100,
                MutationProbability = 5,
                MutationSize = 1,
                NumberOfChromosomes = 1000,
                NumberOfCrossoverPoints = 2,
                ReplaceByGeneration = 800,
                TrackBest = 400,
                TrackWorst = 200,
                SoftConditionSufficient = 0.0,
                SelectionType = SelectionType.Tournament
            };

            AlgorithmWithWeakestConfig algorithmExtendedConfig2 = new AlgorithmWithWeakestConfig()
            {
                CrosoverProbability = 100,
                MutationProbability = 5,
                MutationSize = 1,
                NumberOfChromosomes = 600,
                NumberOfCrossoverPoints = 2,
                ReplaceByGeneration = 480,
                TrackBest = 120,
                TrackWorst = 120,
                SoftConditionSufficient = 0.6,
                SelectionType = SelectionType.Tournament
            };

            AlgorithmWithWeakestConfig algorithmExtendedConfig3 = new AlgorithmWithWeakestConfig()
            {
                CrosoverProbability = 100,
                MutationProbability = 5,
                MutationSize = 1,
                NumberOfChromosomes = 600,
                NumberOfCrossoverPoints = 2,
                ReplaceByGeneration = 480,
                TrackBest = 240,
                TrackWorst = 240,
                SoftConditionSufficient = 0.6,
                SelectionType = SelectionType.Ranking
            };

            GeneticAlgorithm<Schedule> scheduleGeneticAlgorithm;
            var algorithmExtendedConfig = algorithmExtendedConfig1;
            Schedule.WithSoft = algorithmExtendedConfig.IsSoftOn;

            if (algorithmExtendedConfig.TrackWorst != 1)
            {
                scheduleGeneticAlgorithm = new GeneticAlgorithmSchedule.Infrastructure.Concrete.GeneticAlgorithmScheduleWithWeakest(algorithmExtendedConfig, school);
            }
            else
            {
                scheduleGeneticAlgorithm = new GeneticAlgorithmSchedule.Infrastructure.Concrete.GeneticAlgorithmSchedule(algorithmExtendedConfig, school);
            }


            var s = scheduleGeneticAlgorithm.Run();
            var a = scheduleGeneticAlgorithm.CurrentGeneration;

            int score = 0;
            foreach (var group in school.StudentsGroup)
            {
                Dictionary<int, Dictionary<CourseClass, int>> ass = new Dictionary<int, Dictionary<CourseClass, int>>();

                int numberOfRooms = school.Rooms.Count();
                int daySize = school.NumberOfHoursInDay * numberOfRooms;

                for (int i = 0; i < school.NumberOfWorkDays; i++)
                {
                    ass.Add(i, new Dictionary<CourseClass, int>());
                }

                foreach (var groupClasses in s.Classes.Where(o => o.Key.StudentsGroups.Any(sg => sg.Equals(group))))
                {
                    ass.FirstOrDefault(o => o.Key == groupClasses.Value / daySize).Value.Add(groupClasses.Key, groupClasses.Value);
                }

                foreach (var aa in ass)
                {
                    if (aa.Value.Count == 0)
                    {
                        score = score + 2;
                    }

                    else
                    {
                        if (aa.Value.All(o => o.Value % daySize % school.NumberOfHoursInDay <= 6))
                        {
                            score = score + 1;
                        }

                        foreach(var kkk in aa.Value)
                        {
                            foreach (var kkk2 in aa.Value)
                            {
                                if(kkk.Value != kkk2.Value)
                                {
                                    int timeKkk = kkk.Value % daySize % school.NumberOfHoursInDay;
                                    int timeKkk2 = kkk2.Value % daySize % school.NumberOfHoursInDay;
                                    if( timeKkk - timeKkk2 == 1)
                                    {
                                        score = score + 1;
                                    }

                                    if (timeKkk - timeKkk2 == 7)
                                    {
                                        score = score + 1;
                                    }

                                }
                            }

                        }
                    }
                }
            }

            Console.WriteLine(score + " gen : " + scheduleGeneticAlgorithm.CurrentGeneration);

            Config.CreateSchedule(s, school);

            Console.ReadKey();

            //Config.AddResult(scheduleGeneticAlgorithm.FitnessBest, scheduleGeneticAlgorithm.FitnessWorst, scheduleGeneticAlgorithm.FitnessA);

            //Schedule result = scheduleGeneticAlgorithm.Run();

            //Config.CreateSchedule(result, school);

            //Schedule prototype = new Schedule(2, 2, 90, 10);

            //var instance = new Algorithm(100, 20, 5, prototype);

            //instance.Start();



        }
    }
}