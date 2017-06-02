using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndDadAlgorytm
{
    public class Link
    {
        public int ID { get; set; }
        public int startNode { get; set; }
        public int endNode { get; set; }
        public int numberOfFibres { get; set; }
        public float costOfFibre { get; set; }
        public int numberOfLambdas { get; set; }

        public Link(int ID, int startNode, int endNode, int numberOfFibres, float costOfFibre, int numberOfLambdas)
        {
            this.ID = ID;
            this.startNode = startNode;
            this.endNode = endNode;
            this.numberOfFibres = numberOfFibres;
            this.costOfFibre = costOfFibre;
            this.numberOfLambdas = numberOfLambdas;
        }
    }
}
