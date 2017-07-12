using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using NSubstitute;
using Shouldly;
using Xunit;

namespace EClient.Tests
{
    public class VergleichspaarViewModelTests
    {
        private ICes ces;

        public VergleichspaarViewModelTests()
        {
            this.ces = Substitute.For<ICes>();
            var vergleichspaare = new List<VergleichspaarDto>
            {
                new VergleichspaarDto { Id = "0", A = "Story1", B = "Story2"},
                new VergleichspaarDto { Id = "1", A = "Story1", B = "Story3"},
            };
            this.ces.Vergleichspaare.Returns(vergleichspaare);
        }

        [Fact]
        public void VergleichspaarViewModel_läd_bei_Aufruf_die_Vergleichspaare()
        {
            var sut = new VergleichspaarViewModel(this.ces);

            sut.Vergleichspaare.Count.ShouldBe(2);
            sut.Vergleichspaare.First().B.ShouldBe("Story2");
            sut.Vergleichspaare.First().Id.ShouldBe("0");
            sut.Vergleichspaare.Last().Id.ShouldBe("1");
            sut.Vergleichspaare.Last().B.ShouldBe("Story3");
        }

        [Fact]
        public void Vergleichspaare_werden_bei_Klick_auf_Ok_gespeichert()
        {
            var sut = new VergleichspaarViewModel(this.ces);

            sut.Vergleichspaare.First().Selektion = Selektion.B;
            sut.Vergleichspaare.Last().Selektion = Selektion.A;

            sut.OkCommand.Execute(null);

            this.ces.Received(1).Gewichtung_regischtriere(
                Arg.Do<IEnumerable<GewichtetesVergleichspaarDto>>(dtos =>
                {
                    dtos.First().Selektion.ShouldBe(Selektion.B);
                    dtos.First().Id.ShouldBe("0");
                    dtos.Last().Id.ShouldBe("1");
                    dtos.Last().Selektion.ShouldBe(Selektion.A);
                }), 
                Arg.Any<Action>(), 
                Arg.Any<Action>());
        }
    }
}
