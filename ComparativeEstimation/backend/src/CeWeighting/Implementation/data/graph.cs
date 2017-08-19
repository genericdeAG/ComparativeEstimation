using System.Collections.Generic;

namespace CeWeighting.data
{
    public class Graph
    {
        public Graph() {
            Nodes = new List<Node>();
        }
        public IList<Node> Nodes { get; set; }
    }


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
