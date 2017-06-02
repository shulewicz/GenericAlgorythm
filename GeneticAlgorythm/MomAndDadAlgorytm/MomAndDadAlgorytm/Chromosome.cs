using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndDadAlgorytm
{
    public class Chromosome
    {
        public List<List<int>> chromosome = new List<List<int>>();
        public float cost = 0;

        public Chromosome (List<List<int>> Chromosome, float Cost)
        {
            chromosome = Chromosome;
            cost = Cost;
        }

    }
}
