using System;
using System.Collections.Generic;
using Contracts;
using Gewichtung.Model;

namespace Gewichtung
{
    public class Gewichtung : IGewichtung
    {
        public void Gewichtung_berechne(IEnumerable<GewichtetesVergleichspaarDto> voting, IEnumerable<Vergleichspaar> vergleichspaare, Action<Contracts.Gewichtung> ok, Action fehler)
        {
            var relationen = this.Relationen_erstellen(voting, vergleichspaare);
            var graph = GraphGenerator.Graph_erstellen(relationen);
            GraphGenerator.Sortieren(graph, ok, fehler);
        }

        private IEnumerable<IndexTupel> Relationen_erstellen(IEnumerable<GewichtetesVergleichspaarDto> voting, IEnumerable<Vergleichspaar> vergleichspaare)
        {
            throw new NotImplementedException();
        }

        public Contracts.Gewichtung Gesamtgewichtung_berechne(IEnumerable<Contracts.Gewichtung> gewichtungen)
        {
            throw new NotImplementedException();
        }
    }
}