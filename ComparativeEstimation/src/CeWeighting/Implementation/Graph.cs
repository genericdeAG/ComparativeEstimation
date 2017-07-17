using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeWeighting
{
    public class Graph
    {
        public Graph()
        {
            Nodes = new List<Node>();
        }
        public IList<Node> Nodes { get; set; }
    }
}
