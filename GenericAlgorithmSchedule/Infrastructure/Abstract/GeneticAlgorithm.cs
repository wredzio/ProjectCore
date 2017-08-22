using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmSchedule.Infrastructure.Abstract
{
    public abstract class GeneticAlgorithm<T> where T : IChromosome
    {
        public int CurrentGeneration { get; protected set; }

        private IEnumerable<T> _population;
        private IEnumerable<Parents<T>> _parentsList;
        private IEnumerable<T> _offsrping;

        protected abstract IEnumerable<T> InitializePopulation();
        protected abstract bool IsEvaluationConditionSufficient(IEnumerable<T> population);
        protected abstract IEnumerable<Parents<T>> SelectParents(IEnumerable<T> population);
        protected abstract IEnumerable<T> Crossover(IEnumerable<Parents<T>> parentsList);
        protected abstract IEnumerable<T> Mutation(IEnumerable<T> offsrping);
        protected abstract IEnumerable<T> CreateNewPopulation(IEnumerable<T> offsrping, IEnumerable<T> oldPopulation);
        protected abstract T GetBestChromosome(IEnumerable<T> population);

        public virtual T Run()
        {
            _population = InitializePopulation();

            CurrentGeneration = 0;

            while (!IsEvaluationConditionSufficient(_population))
            {
                _parentsList = SelectParents(_population);

                _offsrping = Crossover(_parentsList);
                _offsrping = Mutation(_offsrping);
                _population = CreateNewPopulation(_offsrping, _population);

                CurrentGeneration = CurrentGeneration + 1; ;
            }

            return GetBestChromosome(_population);
        }
    }

    public class Parents<T>
    {
        public T FirstParent { get; set; }
        public T SecondParent { get; set; }
    }
}
