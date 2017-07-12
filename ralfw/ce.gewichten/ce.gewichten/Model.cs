using System;
using System.Collections.Generic;

namespace ce.gewichten.model
{
    public class Knoten
    {
        public Knoten()
        {
            Nachfolger = new List<Knoten>();
        }
        public int Index { get; set; }
        public IList<Knoten> Nachfolger { get; set; }
    }


    public class Graph
    {
        public Graph()
        {
            Knoten = new List<Knoten>();
        }
        public IList<Knoten> Knoten { get; set; }
    }


    public class IndexTupel
    {
        public int Wichtiger { get; set; }
        public int Weniger { get; set; }
    }
}
