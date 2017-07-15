using System;
using Xunit;

namespace CeDomain
{
    public class test_RequestHandler
    {
        [Fact]
        public void Anmelden()
        {
            var state = new RequestHandler.State();
            var sut = new RequestHandler(null, state);

            sut.Âmelde("1");
            Assert.Equal(1, state.Anmeldungen.Count);

            sut.Âmelde("2");
            Assert.Equal(2, state.Anmeldungen.Count);
        }

        [Fact]
        public void Anmelden_idempotent()
        {
            var state = new RequestHandler.State();
            var sut = new RequestHandler(null, state);

            sut.Âmelde("1");
            Assert.Equal(1, state.Anmeldungen.Count);

            sut.Âmelde("1");
            Assert.Equal(1, state.Anmeldungen.Count);
        }
    }
}
