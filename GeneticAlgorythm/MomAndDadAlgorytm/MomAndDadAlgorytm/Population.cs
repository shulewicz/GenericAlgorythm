using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndDadAlgorytm
{
    public class Population
    {
        public List<Chromosome> PopulationList = new List<Chromosome>();

        Network Network;
        int sizeOfPopulation;
        long Seed;
        float Cost = 0;

        public Population (Network network, long seed, int sizeOfPopulation)
        {
            Network = network;
            Seed = seed;
            this.sizeOfPopulation = sizeOfPopulation;
            MakePopulation();
        }

        private void MakePopulation()
        {
            for (int k = 0; k < sizeOfPopulation; k++)
            {
                GenerateOneChromosome();
            }
        }

        private void GenerateOneChromosome()
        {
            List<List<int>> chromosome = new List<List<int>>();
            float cost = 0;
            List<int> lambdasOnLink = new List<int>();

            for (int x = 0; x < Network.Link.Count(); x++)
            {
                lambdasOnLink.Add(0);
            }

            for (int i = 0; i < Network.Demand.Count(); i++)
            {

                List<Path> ListOfConnections = Network.Demand[i].listOfPaths;
                List<int> DemandList = new List<int>();

                int max = Network.Demand[i].demandVolume;

                Random rand = new Random();

                for (int j = 0; j < ListOfConnections.Count(); j++)
                {
                    int LambdasOnPath = rand.Next(0, max + 1);

                    if (j + 1 == ListOfConnections.Count())
                    {
                        LambdasOnPath = max;
                    }

                    DemandList.Add(LambdasOnPath);
                    max = max - LambdasOnPath;
                }

                chromosome.Add(DemandList);
            }

            solutionCheck(Network, chromosome, lambdasOnLink);

            Chromosome Chromosome = new Chromosome(chromosome,Cost);
            PopulationList.Add(Chromosome);
            Cost = 0;

        }

        private bool solutionCheck(Network Network, List<List<int>> chromosome, List<int> lambdasOnLink)
        {
            //Console.WriteLine("CHECKING SOLUTION....");
            //Console.WriteLine("");

            for (int i = 0; i < chromosome.Count(); i++)
            {
                for (int j = 0; j < chromosome[i].Count(); j++)
                {
                    int numberOfLambdas = chromosome[i][j];
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

                if (lambdasOnLink[i] > Network.Link[i].numberOfLambdas * Network.Link[i].numberOfFibres)
                {
                    //return false;
                }
            }
            //Cost = 0;
            return true;
        }

        private float Count(int input, int numberOfLambdas, float costOfFibre)
        {
            int x = input / numberOfLambdas;
            int y = input % numberOfLambdas;

            if (y != 0)
            {
                x++;
            }

            return x * costOfFibre;
        }


    }
}
