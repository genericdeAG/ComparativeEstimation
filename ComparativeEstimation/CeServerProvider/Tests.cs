using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using FluentAssertions;
using Xunit;

namespace CeServerProvider
{
    public class Tests
    {
        private RestProvider CreateValidRestProvider()
        {
            return new RestProvider("http://localhost:5000/api/");
        }


        [Fact]
        public void Âmelde_MitEinerId_ReturnsWithoutError()
        {
            //ARRANGE
            var restProvider = CreateValidRestProvider();

            //ACT
            restProvider.Âmelde("irgenbdwas");
        }

        [Fact]
        public void Gewichtung_Regischrtriere_SchönesWetter_Funktioniert()
        {
            //ARRANGE
            bool warOk = false;
            var provider = CreateValidRestProvider();
            var voting = new List<GewichtetesVergleichspaarDto>()
            {
                new GewichtetesVergleichspaarDto()
                {
                    Id = "id1",
                    Selektion = Selektion.A
                },

                new GewichtetesVergleichspaarDto()
                {
                    Id = "id2",
                    Selektion = Selektion.A
                },

                new GewichtetesVergleichspaarDto()
                {
                    Id = "id3",
                    Selektion = Selektion.B
                }
            };

            //ACT   
            provider.Gewichtung_regischtriere(voting, () => { warOk = true; }, () => { warOk = false; });

            //ASSERT
            warOk.Should().BeTrue();
        }

        [Fact]
        public void Gesamtgewichtung_SchönesWetter_LiefertObjektZurück()
        {
            //ARRANGE
            var provider = CreateValidRestProvider();

            //ACT
            var result = provider.Gesamtgewichtung;

            //ASSERT
            result.Should().NotBeNull();

        }

        [Fact]
        public void Vergleichspaare_SchönesWetter_LiefertObjektZurück()
        {
            //ARRANGE
            var provider = CreateValidRestProvider();

            //ACT
            var result = provider.Vergleichspaare;

            //ASSERT
            result.Should().NotBeNull();
        }


        [Fact]
        public void Sprint_âlege_MitEinerId_ReturnsWithoutError()
        {
            //ARRANGE
            var restProvider = CreateValidRestProvider();
            var stories = new List<string>
            {
                "a",
                "b",
                "c"
            };

            //ACT
            restProvider.Sprint_âlege(stories);
        }


        [Fact]
        public void Sprint_lösche_ReturnsWithoutError()
        {
            //ARRANGE
            var restProvider = CreateValidRestProvider();

            //ACT
            restProvider.Sprint_lösche();
        }

    }
}
