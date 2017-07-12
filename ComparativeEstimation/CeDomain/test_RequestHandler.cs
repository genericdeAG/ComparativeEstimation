using System;
using NUnit.Framework;

namespace CeDomain
{
    [TestFixture]
    public class test_RequestHandler
    {
        [Test]
        public void Anmelden()
        {
            var state = new RequestHandlerState();
            var sut = new RequestHandler(null, state);

            sut.Âmelde("1");
            Assert.AreEqual(1, state.anmeldungen.Count);

            sut.Âmelde("2");
            Assert.AreEqual(2, state.anmeldungen.Count);
        }

        [Test]
        public void Anmelden_idempotent()
        {
            var state = new RequestHandlerState();
            var sut = new RequestHandler(null, state);

            sut.Âmelde("1");
            Assert.AreEqual(1, state.anmeldungen.Count);

            sut.Âmelde("1");
            Assert.AreEqual(1, state.anmeldungen.Count);
        }
    }
}
