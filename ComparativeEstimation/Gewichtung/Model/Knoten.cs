using System.Collections.Generic;

namespace Gewichtung.Model
{
    public class Knoten
    {
        public int Index { get; set; }
        public IList<Knoten> Nachfolger { get; set; }
    }
}