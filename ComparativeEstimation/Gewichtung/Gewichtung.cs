using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Gewichtung.Model;

namespace Gewichtung
{
    public class Gewichtung : IGewichtung
    {
        public void Gewichtung_berechne(IEnumerable<GewichtetesVergleichspaarDto> voting, IEnumerable<Vergleichspaar> vergleichspaare, Action<Contracts.Gewichtung> ok, Action fehler)
        {
            var relationen = Relationen_erstellen(voting, vergleichspaare);
            var graph = GraphGenerator.Graph_erstellen(relationen);
            GraphGenerator.Sortieren(graph, ok, fehler);
        }

        internal static IEnumerable<IndexTupel> Relationen_erstellen(IEnumerable<GewichtetesVergleichspaarDto> voting, IEnumerable<Vergleichspaar> vergleichspaare)
        {
            var paare = vergleichspaare.ToList();
            return voting.Select(v => ErstelleTupel(v, paare.First(p => p.Id == v.Id)));
        }

        private static IndexTupel ErstelleTupel(GewichtetesVergleichspaarDto v, Vergleichspaar paar)
        {
            return new IndexTupel
            {
                Wichtiger = v.Selektion == Selektion.A ? paar.IndexA : paar.IndexB,
                Weniger = v.Selektion == Selektion.A ? paar.IndexB : paar.IndexA
            };
        }

        public Contracts.Gewichtung Gesamtgewichtung_berechne(IEnumerable<Contracts.Gewichtung> gewichtungen)
        {
            throw new NotImplementedException();
        }
    }
}