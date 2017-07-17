using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeWeighting
{
    public class Node
    {
        public Node()
        {
            Successors = new List<Node>();
        }
        public int Index { get; set; }
        public IList<Node> Successors { get; set; }
    }
}
