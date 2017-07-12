using System.Collections.Generic;

namespace Gewichtung.Model
{
    public class Graph
    {
        public Graph()
        {
            Knoten = new List<Knoten>();
        }
        public IList<Knoten> Knoten { get; set; }
    }
}