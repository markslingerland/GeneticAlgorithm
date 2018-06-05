using System.Collections.Generic;

namespace GeneticAlgorithm
{
    public class Ind
    {
        public Ind(List<int> genetics){
            Genetics = genetics;
        }
        public List<int> Genetics { get; set; }
    }
}