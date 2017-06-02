using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndDadAlgorytm
{
    public class Demand
    {
        public int startNode;
        public int endNode;
        public int demandVolume;
        public List<Path> listOfPaths;

        public Demand(int startNode, int endNode, int demandVolume, List<Path> listOfPaths)
        {
            this.startNode = startNode;
            this.endNode = endNode;
            this.demandVolume = demandVolume;
            this.listOfPaths = listOfPaths;
        }

        public Demand(int startNode, int endNode, int demandVolume)
        {
            this.startNode = startNode;
            this.endNode = endNode;
            this.demandVolume = demandVolume;

        }
    }
}
