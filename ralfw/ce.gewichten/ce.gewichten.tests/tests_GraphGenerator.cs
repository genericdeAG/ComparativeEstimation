using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ce.gewichten.model;
using ce.gewichten;

namespace Gewichtung.Tests
{
    [TestFixture]
    public class GraphGeneratorTests
    {

        [Test]
        public void AnzahlVorgaengerBerechnen_LeererGraph_leeresDictionary()
        {
            var graph = new Graph();

            var dictionary = GraphGenerator.Anzahl_Vorgänger_berechnen(graph);
            Assert.AreEqual(0, dictionary.Count());
        }


        [Test]
        public void AnzahlVorgaengerBerechnen_EinGraph_richtigGefülltestDictionary()
        {
            var k0 = new Knoten
            {
                Index = 0,
            };

            var k1 = new Knoten
            {
                Index = 1
            };

            var k2 = new Knoten
            {
                Index = 2
            };

            var k3 = new Knoten()
            {
                Index = 3
            };

            k0.Nachfolger = new List<Knoten> { k1, k2, k3 };
            k1.Nachfolger = new List<Knoten> { k2 };

            var graph1 = new Graph
            {
                Knoten = new List<Knoten> { k0, k1, k2, k3 }
            };
            var graph = graph1;

            var dict = GraphGenerator.Anzahl_Vorgänger_berechnen(graph);

            Assert.AreEqual(0, dict[0]);
            Assert.AreEqual(1, dict[1]);
            Assert.AreEqual(2, dict[2]);
            Assert.AreEqual(1, dict[3]);
        }

        [Test]
        public void Sortieren()
        {
            var k0 = new Knoten { Index = 0 };
            var k1 = new Knoten { Index = 1 };
            var k2 = new Knoten { Index = 2 };

            k0.Nachfolger.Add(k1);
            k2.Nachfolger.Add(k0);
            k2.Nachfolger.Add(k1);

            var graph = new Graph()
            {
                Knoten = new List<Knoten> { k0, k1, k2 }
            };

            var dict = new Dictionary<int, int>()
            {
                [0] = 1,
                [1] = 2,
                [2] = 0
            };

            var result = new int[0];
            GraphGenerator.TopologischSortieren(
                graph,
                dict,
                g => { result = g.StoryIndizes.ToArray(); },
                () => Assert.False(true, "Unerwarteter Fehler"));

            Assert.AreEqual(new[] { 2, 0, 1 }, result);
        }

        [Test]
        public void GraphErstellen()
        {
            var tuples = new List<IndexTupel>
            {
                new IndexTupel
                {
                    Wichtiger = 0,
                    Weniger = 1,
                },
                new IndexTupel
                {
                    Wichtiger = 2,
                    Weniger = 0,
                },
                new IndexTupel
                {
                    Wichtiger = 2,
                    Weniger = 1,
                },
            };
            var graph = GraphGenerator.Graph_erstellen(tuples);

            Assert.AreEqual(3, graph.Knoten.Count);
            Assert.AreEqual(new[]{1}, graph.Knoten[0].Nachfolger.Select(n => n.Index));
            Assert.AreEqual(0, graph.Knoten[1].Nachfolger.Select(n => n.Index).Count());
            Assert.AreEqual(new[]{0,1}, graph.Knoten[2].Nachfolger.Select(n => n.Index));
        }
    }
}