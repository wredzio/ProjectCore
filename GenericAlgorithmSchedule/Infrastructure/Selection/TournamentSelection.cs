using GeneticAlgorithmSchedule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmSchedule.Infrastructure.Abstract;

namespace GeneticAlgorithmSchedule.Infrastructure.Selection
{
    internal class TournamentSelection : GeneticSelection<Schedule>
    {
        public static int TournamentCount { get; set; }

        public TournamentSelection(int replaceByGeneration, Random rand) : base(replaceByGeneration, rand) { TournamentCount = 2; }

        public override IEnumerable<Parents<Schedule>> SelectParents(IEnumerable<Schedule> population)
        {
            List<Parents<Schedule>> paterns = new List<Parents<Schedule>>();
            for (int i = 0; i < _replaceByGeneration; i++)
            {
                List<Schedule> tournamentWiners = new List<Schedule>();
                for (int j = 0; j < TournamentCount; j++)
                {
                    var firstToTournament = population.ElementAt(_rand.Next() % population.Count());
                    var secondToTournament = population.ElementAt(_rand.Next() % population.Count());

                    tournamentWiners.Add(firstToTournament.Fitness > secondToTournament.Fitness ? firstToTournament : secondToTournament);
                }

                paterns.Add(new Parents<Schedule>()
                {
                    FirstParent = tournamentWiners[0],
                    SecondParent = tournamentWiners[1]
                });
            }
            return paterns;
        }
    }
}
