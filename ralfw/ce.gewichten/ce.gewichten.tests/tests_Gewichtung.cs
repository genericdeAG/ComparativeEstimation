using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ce.contracts;
using ce.gewichten.model;
using ce.gewichten;

namespace Gewichtung.Tests
{
    [TestFixture]
    public class GewichtungTests
    {

        [Test]
        public void Gewichtung_berechnen()
        {
            var voting = new List<GewichtetesVergleichspaarDto>()
            {
                new GewichtetesVergleichspaarDto {Id = "1", Selektion = Selektion.A},
                new GewichtetesVergleichspaarDto {Id = "2", Selektion = Selektion.B},
                new GewichtetesVergleichspaarDto {Id = "3", Selektion = Selektion.B},
            };

            var paare = new List<Vergleichspaar>()
            {
                new Vergleichspaar {Id = "1", IndexA = 0, IndexB = 1},
                new Vergleichspaar {Id = "2", IndexA = 0, IndexB = 2},
                new Vergleichspaar {Id = "3", IndexA = 1, IndexB = 2}
            };

            var result = new int[0];
            var sut = new Gewichten();
            sut.Gewichtung_berechne(
                voting,
                paare,
                g => result = g.StoryIndizes.ToArray(),
                () => { throw new Exception(); }
            );

            Assert.AreEqual(new[] { 2, 0, 1 }, result);
        }

        [Test]
        public void GesamtgewichtungBerechnen_nochKeineVotings_leereListe()
        {
            var sut = new Gewichten();

            var result = sut.Gesamtgewichtung_berechne(new List<ce.contracts.Gewichtung>());
            Assert.AreEqual(0, result.StoryIndizes.Count());

            result = sut.Gesamtgewichtung_berechne(null);
            Assert.AreEqual(0, result.StoryIndizes.Count());
        }

        [Test]
        public void Gewichtung_berechnen_FehlerTrittAuf()
        {
            var voting = new List<GewichtetesVergleichspaarDto>()
            {
                new GewichtetesVergleichspaarDto {Id = "1", Selektion = Selektion.A},
                new GewichtetesVergleichspaarDto {Id = "2", Selektion = Selektion.B},
                new GewichtetesVergleichspaarDto {Id = "3", Selektion = Selektion.A},
            };

            var paare = new List<Vergleichspaar>()
            {
                new Vergleichspaar {Id = "1", IndexA = 0, IndexB = 1},
                new Vergleichspaar {Id = "2", IndexA = 0, IndexB = 2},
                new Vergleichspaar {Id = "3", IndexA = 1, IndexB = 2}
            };

            var sut = new Gewichten();
            sut.Gewichtung_berechne(
                voting,
                paare,
                g => { throw new Exception("kein Fehler"); },
                () => Assert.True(true)
            );
        }


        [Test]
        public void Relationen()
        {
            IEnumerable<GewichtetesVergleichspaarDto> voting = new List<GewichtetesVergleichspaarDto>
                {
                    new GewichtetesVergleichspaarDto
                    {
                        Id = "0",
                        Selektion = Selektion.A
                    },
                    new GewichtetesVergleichspaarDto
                    {
                        Id = "1",
                        Selektion = Selektion.B
                    },
                    new GewichtetesVergleichspaarDto
                    {
                        Id = "2",
                        Selektion = Selektion.B
                    },
                };

            IEnumerable<Vergleichspaar> vergleichspaare = new List<Vergleichspaar>
            {
                new Vergleichspaar
                {
                    Id = "0",
                    IndexA = 0,
                    IndexB = 1,
                },
                new Vergleichspaar
                {
                    Id = "1",
                    IndexA = 0,
                    IndexB = 2,
                },
                new Vergleichspaar
                {
                    Id = "2",
                    IndexA = 1,
                    IndexB = 2,
                },
            };
            var result = Gewichten.Relationen_erstellen(voting, vergleichspaare).ToList();
            AssertIndize(result[0], 0, 1);
            AssertIndize(result[1], 2, 0);
            AssertIndize(result[2], 2, 1);
        }

        private void AssertIndize(IndexTupel tuple, int wichtiger, int weniger)
        {
            Assert.AreEqual(weniger, tuple.Weniger);
            Assert.AreEqual(wichtiger, tuple.Wichtiger);
        }

        [Test]
        public void Gesamtgewichtung_berechne_richtig()
        {
            var gewichtungen = new List<ce.contracts.Gewichtung>()
            {
                new ce.contracts.Gewichtung() {StoryIndizes = new List<int> {2, 0, 1}},
                new ce.contracts.Gewichtung() {StoryIndizes = new List<int> {1, 2, 0}},
                new ce.contracts.Gewichtung() {StoryIndizes = new List<int> {2, 1, 0}},
            };

            var sut = new Gewichten();

            var result = sut.Gesamtgewichtung_berechne(gewichtungen);

            Assert.AreEqual(new[] { 2, 1, 0 }, result.StoryIndizes);
        }
    }
}