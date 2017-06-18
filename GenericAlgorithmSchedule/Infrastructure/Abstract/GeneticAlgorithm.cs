using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmSchedule.Infrastructure.Abstract
{
    public abstract class GeneticAlgorithm<T>
    {
        public int CurrentGeneration { get; protected set; }

        protected IEnumerable<T> _population;
        protected IEnumerable<Parents<T>> _parentsList;
        protected IEnumerable<T> _offsrping;

        protected abstract IEnumerable<T> InitializePopulation();
        protected abstract bool IsEvaluationConditionSufficient();
        protected abstract IEnumerable<Parents<T>> SelectParents(IEnumerable<T> population);
        protected abstract IEnumerable<T> Crossover(IEnumerable<Parents<T>> parentsList);
        protected abstract IEnumerable<T> Mutation(IEnumerable<T> offsrping);
        protected abstract IEnumerable<T> CreateNewPopulation(IEnumerable<T> offsrping, IEnumerable<T> oldPopulation);

        public void Start()
        {
            _population = InitializePopulation();

            CurrentGeneration = 0;

            while (!IsEvaluationConditionSufficient())
            {
                _parentsList = SelectParents(_population);

                _offsrping = Crossover(_parentsList);
                _offsrping = Mutation(_offsrping);
                _population = CreateNewPopulation(_offsrping, _population);

                CurrentGeneration = CurrentGeneration + 1; ;
            }
        }
    }

    public class Parents<T>
    {
        public T FirstParent { get; set; }
        public T SecondParent { get; set; }
    }
}
