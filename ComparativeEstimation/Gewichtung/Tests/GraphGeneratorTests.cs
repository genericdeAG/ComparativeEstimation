using System.Collections.Generic;
using Gewichtung.Model;
using Shouldly;
using Xunit;

namespace Gewichtung.Tests
{
    public class GraphGeneratorTests
    {

        [Fact]
        public void AnzahlVorgaengerBerechnen_LeererGraph_leeresDictionary ()
        {
            var graph = new Graph();

            var dictionary = GraphGenerator.Anzahl_Vorgänger_berechnen(graph);
            dictionary.ShouldBeEmpty();
        }


        [Fact]
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

            k0.Nachfolger = new List<Knoten>{k1,k2, k3};
            k1.Nachfolger = new List<Knoten>{k2};
             
            var graph = new Graph
            {
                Knoten = new List<Knoten> { k0,k1,k2,k3 }
            };

            var dict = GraphGenerator.Anzahl_Vorgänger_berechnen(graph);

            dict[0].ShouldBe(0);
            dict[1].ShouldBe(1);
            dict[2].ShouldBe(2);
            dict[3].ShouldBe(1);
        }


    }
}