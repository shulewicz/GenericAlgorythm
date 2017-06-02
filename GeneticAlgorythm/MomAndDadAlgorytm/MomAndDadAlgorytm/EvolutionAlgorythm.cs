using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndDadAlgorytm
{
    public class EvolutionAlgorythm
    {
        public List<Chromosome> ListOfChromosomes;
        int PopulationSize;
        int NumberOfGenerations;
        double ProbabilityOfMutation;
        double ProbabilityOfCrossing;
        Network Network;
        long seed;


        public EvolutionAlgorythm(List<Chromosome> ListOfChromosome, int PopulationSize, int NumberOfGenerations, double ProbabilityOfMutation, double ProbabilityOfCrossing, long seed, Network Network)
        {
            this.ListOfChromosomes = ListOfChromosome;
            this.PopulationSize = PopulationSize;
            this.NumberOfGenerations = NumberOfGenerations;
            this.ProbabilityOfMutation = ProbabilityOfMutation;
            this.ProbabilityOfCrossing = ProbabilityOfCrossing;
            this.Network = Network;
            this.seed = seed;
        }


        public void startEvolutionDDAP()
        {
            for (int i = 0; i < NumberOfGenerations; i++)
            {
                cross();
                mutate();
                select();
            }
        }

        public void startEvolutionDAP()
        {
            cross();
            mutate();
            select();
        }

        public void cross ()
        {
            int numberOfPairs = ListOfChromosomes.Count()/2;

            for (int i = 0; i< numberOfPairs; i++)
            {
                Random rand = new Random();
                double x = rand.NextDouble();

                if (x > ProbabilityOfCrossing)
                {
                    continue;
                }


                int index1 = rand.Next(ListOfChromosomes.Count() - 1);
                int index2 = index1;

                while (index2 == index1)
                {
                    index2 = rand.Next(ListOfChromosomes.Count() - 1);
                }

                List<Chromosome> Children = new List<Chromosome>();
                Children = crossPairOfChromosomes(ListOfChromosomes[index1].chromosome, ListOfChromosomes[index2].chromosome);
                ListOfChromosomes.AddRange(Children);
            }
        }

        public List<Chromosome> crossPairOfChromosomes (List<List<int>> chromosome1, List<List<int>> chromosome2)
        {

            List<Chromosome> children = new List<Chromosome>();

            List<List<int>> child1 = new List<List<int>>();
            List<List<int>> child2 = new List<List<int>>();

            Random rand = new Random();

            child1 = chromosome1;
            child2 = chromosome2;

            int numberOfChanges = rand.Next(chromosome1.Count()) + 1;
            float cost1 = 0;
            float cost2 = 0;

            for (int i = 0; i < numberOfChanges; i++)
            {
                int index = rand.Next(chromosome1.Count());

                List<int> gene = new List<int>();
                gene = child1[index];
                child1[index] = child2[index];
                child2[index] = gene;

            }

            cost1 = solutionCheck(child1);
            cost2 = solutionCheck(child2);

            children.Add(new Chromosome(child1, cost1));
            children.Add(new Chromosome(child2, cost2));

            return children;
        }

        private void mutate ()
        {
            for (int i = 0; i < ListOfChromosomes.Count(); i++)
            {
                for (int j = 0; j< ListOfChromosomes[i].chromosome.Count(); j++)
                {
                    Random rand = new Random();

                    double x = rand.NextDouble();
                    
                    if (x > ProbabilityOfMutation)
                    {
                        continue;
                    }
                    List<int> Gene = newGene(ListOfChromosomes[i].chromosome[j]);
                    ListOfChromosomes[i].chromosome[j] = Gene;

                }

                float cost1 = ListOfChromosomes[i].cost;
                ListOfChromosomes[i].cost = solutionCheck(ListOfChromosomes[i].chromosome);
                float cost2 = ListOfChromosomes[i].cost;

                if (cost2 != cost1)
                {
                    continue;
                }

            }
        }

        public List<int> newGene(List<int> Gene)
        {
            int sum = 0;

            for (int i = 0; i<Gene.Count(); i++)
            {
                sum += Gene[i];
            }

            for (int i = 0; i < Gene.Count(); i++)
            {
                Random rand = new Random();

                int value = rand.Next(sum);
                sum -= value;
               
            }

            return Gene;
        }

        private void select ()
        {
            var Lista = ListOfChromosomes.OrderBy(x => x.cost).ToList();
            ListOfChromosomes = Lista;

            for (int i = PopulationSize; i < ListOfChromosomes.Count(); i++)
            {
                ListOfChromosomes.Remove(ListOfChromosomes[i]);
                i--;
            }

        }

        private float solutionCheck(List<List<int>> solution)
        {
            //Console.WriteLine("CHECKING SOLUTION....");
            //Console.WriteLine("");
            List<int> lambdasOnLink = new List<int>();
            float Cost = 0;

            for (int x = 0; x < Network.Link.Count(); x++)
            {
                lambdasOnLink.Add(0);
            }

            for (int i = 0; i < solution.Count(); i++)
            {
                for (int j = 0; j < solution[i].Count(); j++)
                {
                    int numberOfLambdas = solution[i][j];
                    var indexesOfLinks = Network.Demand[i].listOfPaths[j].ListOfLinks;

                    for (int k = 0; k < indexesOfLinks.Count(); k++)
                    {
                        int index = indexesOfLinks[k] - 1;
                        lambdasOnLink[index] = lambdasOnLink[index] + numberOfLambdas;
                    }
                }
            }

            for (int i = 0; i < lambdasOnLink.Count(); i++)
            {
                float costOfLink = Count(lambdasOnLink[i], Network.Link[i].numberOfLambdas, Network.Link[i].costOfFibre);
                Cost += costOfLink;
            }

            return Cost;
        }

        private float Count(int input, int numberOfLambdas, float costOfFibre)
        {
            int x = input / numberOfLambdas;
            int y = input % numberOfLambdas;

            if (y != 0 || x == 0)
            {
                x++;
            }

            return x * costOfFibre;
        }

    }
}
