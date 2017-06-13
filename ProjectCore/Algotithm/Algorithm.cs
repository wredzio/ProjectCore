using System.Collections.Generic;
using System.Linq;
using System;
using System.Diagnostics;

namespace ProjectCore.Algotithm
{
    public class Algorithm
    {
        private Random rand = new Random();
        public Schedule[] Chromosomes { get; set; }
        public List<bool> BestFlags { get; set; }
        public int[] BestChromosomes { get; set; }
        public int CurrentBestSize { get; set; }
        public Schedule Prototype { get; set; }
        public int CurrentGeneration { get; set; }
        public int ReplaceByGeneration { get; set; }

        public Algorithm(int numberOfChromosomes, int replaceByGeneration, int trackBest, Schedule prototype)
        {
            ReplaceByGeneration = replaceByGeneration;
            CurrentBestSize = 0;
            Prototype = prototype;
            CurrentGeneration = 0;

            if (numberOfChromosomes < 2)
                numberOfChromosomes = 2;

            if (trackBest < 1)
                trackBest = 1;

            Chromosomes = new Schedule[numberOfChromosomes];
            BestFlags = Extensions.RepeatedDefault<bool>(numberOfChromosomes);

            BestChromosomes = new int[trackBest];

        }

        public void Start()
        {
            if (Prototype == null)
                return;

            ClearBest();


            for (int i = 0; i < Chromosomes.Length; i++)
            {
                Chromosomes[i] = Prototype.MakeNewFromPrototype();
                AddToBest(i);
            }

            CurrentGeneration = 0;

            while (true)
            {

                Schedule bestChromosome = GetBestChromosome();               

                if (bestChromosome.Fitness >= 1)
                {
                  
                    Config.CreateSchedule(bestChromosome);
                    break;
                }

                Schedule[] offspring = new Schedule[ReplaceByGeneration];
                for (int j = 0; j < ReplaceByGeneration; j++)
                {
                    Schedule p1 = Chromosomes[rand.Next() % Chromosomes.Length];
                    Schedule p2 = Chromosomes[rand.Next() % Chromosomes.Length];

                    offspring[j] = p1.Crossover(p2);
                    offspring[j].Mutation();
                }

                for (int j = 0; j < ReplaceByGeneration; j++)
                {
                    int ci;
                    
                    do
                    {
                        ci = rand.Next() % Chromosomes.Length;

                    } while (IsInBest(ci));

                    Chromosomes[ci] = offspring[j];

                    AddToBest(ci);
                }

                float sum = 0;
                foreach (var ch in Chromosomes)
                {
                    sum = sum + ch.Fitness;
                }

                Debug.WriteLine("Œrednia populacji: " + sum / Chromosomes.Length + " Najlepszy : " + bestChromosome.Fitness);
                Console.WriteLine("Œrednia populacji: " + sum / Chromosomes.Length + " Najlepszy : " + bestChromosome.Fitness);
                CurrentGeneration++;
            }

        }

        Schedule GetBestChromosome()
        {
            return Chromosomes[BestChromosomes[0]];
        }

        bool IsInBest(int chromosomeIndex)
        {
            return BestFlags[chromosomeIndex];
        }

        void AddToBest(int chromosomeIndex)
        {
            if ((CurrentBestSize == BestChromosomes.Length &&
                Chromosomes[BestChromosomes[CurrentBestSize - 1]].Fitness >=
                Chromosomes[chromosomeIndex].Fitness) || BestFlags[chromosomeIndex])
                return;

            int i = CurrentBestSize;
            for (; i > 0; i--)
            {
                if (i < BestChromosomes.Length)
                {
                    if (Chromosomes[ BestChromosomes[i - 1]].Fitness >
                        Chromosomes[chromosomeIndex].Fitness)
                        break;

                    BestChromosomes[i] = BestChromosomes[i - 1];
                }
                else
                    BestFlags[BestChromosomes[i - 1]] = false;
            }

            BestChromosomes[i] = chromosomeIndex;
            BestFlags[chromosomeIndex] = true;

            if (CurrentBestSize < BestChromosomes.Length)
                CurrentBestSize++;
        }

        public void ClearBest()
        {
            for (int i = (int)BestFlags.Count - 1; i >= 0; --i)
                BestFlags[i] = false;

            CurrentBestSize = 0;
        }
    }
}