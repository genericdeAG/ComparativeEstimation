using System;
using System.Collections.Generic;
using Contracts;
using Shouldly;
using Xunit;

namespace Gewichtung.Tests
{
    public class GewichtungTests
    {

        [Fact]
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

            var sut = new global::Gewichtung.Gewichtung();
            sut.Gewichtung_berechne(
                voting,
                paare,
                g => g.StoryIndizes.ShouldBe(new [] {2,0,1}),
                () => { throw new Exception(); }
            );
        }

        [Fact]
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

            var sut = new global::Gewichtung.Gewichtung();
            sut.Gewichtung_berechne(
                voting,
                paare,
                g => throw new Exception("kein Fehler"),
                () => Assert.True(true)
            );
        }


    }
}