using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndDadAlgorytm
{
    public class Path
    {
        public int ID;
        public List<int> ListOfLinks;

        public Path(int ID, List<int> ListOfLinks)
        {
            this.ID = ID;
            this.ListOfLinks = ListOfLinks;
        }
    }
}
