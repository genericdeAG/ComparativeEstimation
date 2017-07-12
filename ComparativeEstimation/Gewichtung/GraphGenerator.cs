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

            return graph.Knoten.Select(k => new { Index = k.Index, AnzahlVorgänger = graph.Knoten.Count(v => v.Nachfolger.Select(m => m.Index).Contains(k.Index)) }).ToDictionary(k => k.Index, v => v.AnzahlVorgänger);
        }

        internal static void TopologischSortieren(Graph graph, Dictionary<int, int> vorgaenger, Action<Contracts.Gewichtung> ok, Action fehler)
        {
            var storyIndizes = new List<int>();

            while (vorgaenger.Any())
            {
                // finde elemente Ohne Vorgänger
                var keinVorgängerKnoten = vorgaenger.Where(v => v.Value == 0).ToList();
                if (!keinVorgängerKnoten.Any())
                {
                    fehler();
                    return;
                }

                foreach (var pair in keinVorgängerKnoten)
                {
                    storyIndizes.Add(pair.Key);
                    foreach (var knoten in graph.Knoten.First(k => k.Index == pair.Key).Nachfolger)
                    {
                        vorgaenger[knoten.Index]--;
                    }

                    vorgaenger.Remove(pair.Key);
                }
            }

            ok(new Contracts.Gewichtung{StoryIndizes = storyIndizes});
        }
    }
}