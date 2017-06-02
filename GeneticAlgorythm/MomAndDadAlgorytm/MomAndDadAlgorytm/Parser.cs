using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndDadAlgorytm
{
    public class Parser
    {
        private string[] networkFile = null;
        private int numberOfLinks;
        private int numberOfDemands;
        List<string> LinkPart = new List<string>();
        List<string> DemandPart = new List<string>();

        public Parser (string path)
        {
            networkFile = File.ReadAllLines(path);
            ParseNetwork();
            ParseLinkPart();
        }

        public void ParseNetwork ()
        {
            bool isDemandSection = false;
            numberOfLinks = int.Parse(networkFile[0]);

            for (int i = 1; i < networkFile.Length; i++)
            {
                if (i > numberOfLinks && networkFile[i] == "-1")
                {
                    i = i + 2;
                    numberOfDemands = int.Parse(networkFile[i]);
                    i = i + 1;
                    isDemandSection = true;
                    continue;
                }
                else if (i <= numberOfLinks)
                {
                    LinkPart.Add(networkFile[i]);
                }
                else if (isDemandSection)
                {
                    DemandPart.Add(networkFile[i]);
                }
            }
        }

        public List<Link> ParseLinkPart()
        {
            List<Link> Link = new List<Link>();
            string[] linkPart = LinkPart.ToArray();

            for (int i = 0; i<linkPart.Length; i++)
            {
                string[] numbers = linkPart[i].Split(' ');
                Link.Add(new Link(i, int.Parse(numbers[0]), int.Parse(numbers[1]), int.Parse(numbers[2]), float.Parse(numbers[3]), int.Parse(numbers[4])));
            }
            return Link;
        }

        public List<Demand> ParseDemandPart()
        {
            List<Demand> Demand = new List<Demand>();
            List<Path> Paths = null;

            string[] demandPart = DemandPart.ToArray();

            int lineIndex = 0;

            //int numberOfPaths = int.Parse(demandPart[0]);

            for (int i = 0; i<numberOfDemands; i++)
            {
                Paths = new List<Path>();
                string[] numbers = demandPart[lineIndex].Split(' ');

                Demand.Add(new Demand(int.Parse(numbers[0]), int.Parse(numbers[1]), int.Parse(numbers[2]), new List<Path>()));
                lineIndex++;

                numbers = demandPart[lineIndex].Split(' ');
                int numberOfPaths = int.Parse(numbers[0]);
                lineIndex++;

                for (int j = 0; j< numberOfPaths; j++)
                {
                    numbers = demandPart[lineIndex].Split(' ');

                    if (numbers[0] != "" && numbers != null)
                    {
                        List<int> listOfLinks = new List<int>();
                        int index = 1;

                        while (numbers[index] != null && numbers[index] != "")
                        {
                            listOfLinks.Add(int.Parse(numbers[index]));
                            index++;
                        }

                        numbers = demandPart[lineIndex].Split(' ');
                        lineIndex++;
                        Paths.Add(new Path(j + 1, listOfLinks));
                    }
                }
                Demand[i].listOfPaths = Paths;
                lineIndex++;

            }

            return Demand;

        }
    }
}
