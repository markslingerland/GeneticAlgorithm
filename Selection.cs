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
            while(bestParents.Count <= 2){
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

        public static Tuple<Ind,Ind> Ranking(Ind[] individuals, double[] fitnesses){
            var individualList = individuals.ToList();
            var fitnessesList = fitnesses.ToList();
            var newIndividualList = new List<Ind>();
            var newFitnessesList = new List<double>();
            var cumulative = 0.0;
            for(var i = 0; i < fitnesses.Count(); i++){
                cumulative++;
                var lowest = fitnessesList.Min();
                var index = fitnessesList.IndexOf(lowest);
                newIndividualList.Add(individualList[index]);
                newFitnessesList.Add(cumulative);
                individualList.RemoveAt(index);
                fitnessesList.RemoveAt(index);
            }

            return Roulette(newIndividualList.ToArray(), newFitnessesList.ToArray());
        }

        public static Tuple<Ind,Ind> Tournament(Ind[] individuals, double[] fitnesses){
            Random rnd = new Random();
            var individualsList = individuals.ToList();
            var fitnessesList = fitnesses.ToList();
            for(var i = 0; i < 2; i++){
                var random = rnd.Next();
                var randomIndividuals = individualsList.OrderBy(x => random).Take(individualsList.Count() / 4);
                var randomFitnesses = fitnessesList.OrderBy(x => random).Take(fitnessesList.Count() / 4);
                var index = individualsList.IndexOf(randomIndividuals.First());

                
            }

            return Tuple.Create(individuals[0], individuals[1]);
        }



    }
}