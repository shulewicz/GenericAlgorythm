using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndDadAlgorytm
{
    public class DDAP
    {
        public float TheBestCost = 0;
        float Cost = 0;

        public List<float> listOfCosts = new List<float>();
        public List<int> theBestLambdasOnLink = new List<int>();
        public List<int> lambdasOnLink = new List<int>();
        public List<List<int>> solution = new List<List<int>>();
        public List<List<int>>theBestSolution = new List<List<int>>();
        public List<List<List<int>>> listOfSolutions = new List<List<List<int>>>();



        public DDAP (Network network)
        {
        }

        public void startBruteForce (Network network, int NumberOfRounds)
        {
            for (int k = 0; k < NumberOfRounds; k++)
            {
                BruteForce(network);
            }
        }


        private void BruteForce(Network Network)
        {
            solution = new List<List<int>>();
            lambdasOnLink = new List<int>();

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

                solution.Add(DemandList);
            }

            solutionCheck(Network);
            listOfSolutions.Add(solution);
                listOfCosts.Add(Cost);
            
                if (Cost < TheBestCost || TheBestCost == 0)
                {
                    TheBestCost = Cost;
                    theBestSolution = solution;
                    theBestLambdasOnLink = lambdasOnLink;
                }
                Cost = 0;
            

        }

        private bool solutionCheck(Network Network)
        {
            //Console.WriteLine("CHECKING SOLUTION....");
            //Console.WriteLine("");

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

            return true;
        }

        private float Count (int input, int numberOfLambdas, float costOfFibre)
        {
            int x = input / numberOfLambdas;
            int y = input % numberOfLambdas;

            if (y!=0)
            {
                x ++;
            }

            return x*costOfFibre;
        }

    }
}
