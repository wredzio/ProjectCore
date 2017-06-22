using GeneticAlgorithmSchedule.Infrastructure.Abstract;
using GeneticAlgorithmSchedule.Models;
using System;

namespace ProjectCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Config2.Init();

            School school = new School();

            school.CourseClasses = Config2.CourseClasses;
            school.Courses = Config2.Courses;
            school.Professors = Config2.Professors;
            school.Rooms = Config2.Rooms;
            school.NumberOfHoursInDay = 12;
            school.NumberOfWorkDays = 5;

            AlgorithmConfig algorithmConfig = new AlgorithmConfig()
            {
                CrosoverProbability = 90,
                MutationProbability = 5,
                MutationSize = 2,
                NumberOfChromosomes = 500,
                NumberOfCrossoverPoints = 2,
                ReplaceByGeneration = 400,
                TrackBest = 300
            };

            GeneticAlgorithm<Schedule> a = new GeneticAlgorithmSchedule.Infrastructure.Concrete.GeneticAlgorithmSchedule(algorithmConfig, school);

            Schedule result = a.Run();

            Config2.CreateSchedule(result, school);

            //Schedule prototype = new Schedule(2, 2, 90, 10);

            //var instance = new Algorithm(100, 20, 5, prototype);

            //instance.Start();
        }
    }
}