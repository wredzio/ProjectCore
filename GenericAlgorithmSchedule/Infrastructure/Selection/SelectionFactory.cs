using GeneticAlgorithmSchedule.Infrastructure.Abstract;
using GeneticAlgorithmSchedule.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmSchedule.Infrastructure.Selection
{
    public enum SelectionType
    {
        Random,
        Tournament,
        Ranking,
        Roulette
    }

    public abstract class GeneticSelection<T>
    {
        protected int _replaceByGeneration;
        protected Random _rand;

        public GeneticSelection(int replaceByGeneration, Random rand)
        {
            _rand = rand;
            _replaceByGeneration = replaceByGeneration;
        }

        public abstract IEnumerable<Parents<T>> SelectParents(IEnumerable<T> population);
    }

    public class SelectionFactory
    {
        protected int _replaceByGeneration;
        protected Random _rand;

        public SelectionFactory(int replaceByGeneration, Random rand)
        {
            _rand = rand;
            _replaceByGeneration = replaceByGeneration;
        }

        public GeneticSelection<Schedule> CreateSelector(SelectionType selectionType)
        {
            switch (selectionType)
            {
                case SelectionType.Random:
                    return new RandomSelection(_replaceByGeneration, _rand);
                case SelectionType.Ranking:
                    return new RankingSelection(_replaceByGeneration, _rand);
                case SelectionType.Roulette:
                    return new RouletteSelection(_replaceByGeneration, _rand);
                case SelectionType.Tournament:
                    return new TournamentSelection(_replaceByGeneration, _rand);
                default:
                    throw new Exception();
            }
        }
    }
}
