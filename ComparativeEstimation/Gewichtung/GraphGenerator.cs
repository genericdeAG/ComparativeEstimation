using System;
using System.Collections.Generic;
using System.Linq;
using Gewichtung.Model;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;

namespace Gewichtung
{
    public static class GraphGenerator
    {
        public static Graph Graph_erstellen(IEnumerable<IndexTupel> relationen)
        {
            throw new NotImplementedException();
        }

        public static void Sortieren(Graph graph, Action<Contracts.Gewichtung> ok, Action fehler)
        {
            var dict = Anzahl_Vorgänger_berechnen(graph);
            TopologischSortieren(graph, dict, ok, fehler);
        }

        internal static Dictionary<int, int> Anzahl_Vorgänger_berechnen(Graph graph)
        {
            if (graph?.Knoten == null)
            {
                return new Dictionary<int, int>();
            }

            return graph.Knoten.Select(k => new {Index = k.Index, AnzahlVorgänger = graph.Knoten.Count(v => v.Nachfolger.Select(m => m.Index).Contains(k.Index))}).ToDictionary(k => k.Index, v => v.AnzahlVorgänger);
        }

        private static void TopologischSortieren(Graph graph, Dictionary<int, int> vorgaenger, Action<Contracts.Gewichtung> ok, Action fehler)
        {
            throw new NotImplementedException();
        }
    }
}