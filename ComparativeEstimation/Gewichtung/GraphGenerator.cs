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
            var graph = new Graph();

            foreach (var relation in relationen)
            {
                var wichtiger = HoleOderErstelleKnoten(graph, relation.Wichtiger);
                var weniger = HoleOderErstelleKnoten(graph, relation.Weniger);

                wichtiger.Nachfolger.Add(weniger);
            }

            return graph;
        }



        private static Knoten HoleOderErstelleKnoten(Graph graph, int index)
        {
            var knoten = graph.Knoten.FirstOrDefault(k => k.Index == index);

            if (knoten == null)
            {
                knoten = new Knoten{Index = index};
                graph.Knoten.Add(knoten);
            }

            return knoten;
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

            ok(new Contracts.Gewichtung { StoryIndizes = storyIndizes });
        }
    }
}