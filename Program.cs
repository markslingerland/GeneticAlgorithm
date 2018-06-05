using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            /* FUNCTIONS TO DEFINE (for each problem):
            Func<Ind> createIndividual;                                 ==> input is nothing, output is a new individual
            Func<Ind,double> computeFitness;                            ==> input is one individual, output is its fitness
            Func<Ind[],double[],Func<Tuple<Ind,Ind>>> selectTwoParents; ==> input is an array of individuals (population) and an array of corresponding fitnesses, output is a function which (without any input) returns a tuple with two individuals (parents)
            Func<Tuple<Ind, Ind>, Tuple<Ind, Ind>> crossover;           ==> input is a tuple with two individuals (parents), output is a tuple with two individuals (offspring/children)
            Func<Ind, double, Ind> mutation;                            ==> input is one individual and mutation rate, output is the mutated individual
            */

            Func<Ind> createIndividual = makeIndividual;
            Func<Ind,double> computeFitness = fitnessComputer;
            Func<Ind[],double[],Func<Tuple<Ind,Ind>>> selectTwoParents = bestParents;
            Func<Tuple<Ind, Ind>, Tuple<Ind, Ind>> crossover = singlePointCrossover;
            Func<Ind, double, Ind> mutation = modification;
            
            var result = modification(makeIndividual(), 0.2);


            GeneticAlgorithm<Ind> fakeProblemGA = new GeneticAlgorithm<Ind>(0.5, 0.1, false, 10, 5); // CHANGE THE GENERIC TYPE (NOW IT'S INT AS AN EXAMPLE) AND THE PARAMETERS VALUES
            var solution = fakeProblemGA.Run(createIndividual, computeFitness, selectTwoParents, crossover, mutation); 
            Console.WriteLine("Solution: ");
            Console.WriteLine(solution.Genetics.Sum());

        }

        private static Ind makeIndividual(){
            var genetics = new List<int>();
            var rnd = new Random();
            for(var i = 0; i < 16; i++){
                genetics.Add(rnd.Next(2));
            }

            return new Ind(genetics);
        }

        private static double fitnessComputer(Ind individual){
            var fitness = 0.0;
            foreach(var genetic in individual.Genetics){
                if(genetic > 0){
                    fitness += 1;
                }
            }
            return fitness;
        }

        private static Func<Tuple<Ind,Ind>> bestParents(Ind[] individuals, double[] fitnesses){
            return () => Selection.Tournament(individuals, fitnesses);
        }

        private static Tuple<Ind,Ind> singlePointCrossover(Tuple<Ind,Ind> parents)
        {
            var rnd = new Random();
            var parent1 = parents.Item1;
            var parent2 = parents.Item2;
            var splitPoint = rnd.Next(0,parent1.Genetics.Count - 1);
            for(var i = splitPoint; i < parent1.Genetics.Count ; i++){
                swap(parent1.Genetics, parent2.Genetics, i);
            }
            return Tuple.Create(parent1, parent2);
        }

        private static void swap<T>(IList<T> list1, IList<T> list2, int index)
        {
            T tmp = list1[index];
            list1[index] = list2[index];
            list2[index] = tmp;
        }

        private static Ind modification(Ind individual, double mutationRate){
            var rnd = new Random();
            for(var i = 0; i < individual.Genetics.Count; i++){
                var probability = rnd.NextDouble();
                if(probability <= mutationRate){
                    if(individual.Genetics[i] == 0){
                        individual.Genetics[i] = 1;
                    } else {
                        individual.Genetics[i] = 0;
                    }
                }
            }
            return individual;
        }

        
    }
}
