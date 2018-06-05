using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm
{
    public static class Selection
    {
        public static Tuple<Ind,Ind> Roulette(Ind[] individuals, double[] fitnesses){
            var individualList = individuals.ToList();
            var fitnessesList = fitnesses.ToList();
            Random rnd = new Random();
            var bestParents = new List<Ind>();
            while(bestParents.Count >= 2){
                var totalFitness = fitnessesList.Sum();
                var cumulativeProbability = 0.0;
                var cumulativeProbabilityList = new List<double>();

                for(var i = 0; i < individualList.Count(); i++){
                    cumulativeProbability += fitnessesList[i] / totalFitness;
                    cumulativeProbabilityList.Add(cumulativeProbability);
                }

                var randomDouble = rnd.NextDouble();
                for(var j = 0; j < individualList.Count(); j++)
                {
                    if(cumulativeProbabilityList[j] >= randomDouble)
                    {
                        bestParents.Add(individualList[j]);
                        individualList.RemoveAt(j);
                        fitnessesList.RemoveAt(j);
                        break;
                    }
                }
            }
            

            return Tuple.Create(bestParents[0], bestParents[1]);
        }
    }
}